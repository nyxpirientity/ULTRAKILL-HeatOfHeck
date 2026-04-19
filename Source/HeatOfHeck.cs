using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Nyxpiri.ULTRAKILL.NyxLib;

namespace Nyxpiri.ULTRAKILL.HeatOfHeck
{
    [HarmonyPatch(typeof(Revolver), "Update")]
    static class PlayerRevolverPatch
    {
        private static FieldInfo twirlLevelFi = typeof(Revolver).GetField("twirlLevel", BindingFlags.Instance | BindingFlags.NonPublic);
        public static void Prefix(Revolver __instance)
        {
            HeatOfHeck.RevolverTwirlThisUpdate = Mathf.Max(HeatOfHeck.RevolverTwirlThisUpdate, (float)(twirlLevelFi.GetValue(__instance)));
        }

        public static void Postfix(Revolver __instance)
        {
        }
    }

    public static class HeatOfHeckHeckExtension
    {
        public static HeatOfHeck GetHeatOfHeck(this Heck heck)
        {
            return heck.GetMonoByIndex<HeatOfHeck>(HeatOfHeck.MonoRegistrarIdx);
        }
    }

    public class HeatOfHeck : MonoBehaviour
    {
        public static HeatOfHeck Instance = null;
        public static float RevolverTwirlThisUpdate = 0.0f;
        public NewMovement player { get; private set; } = null;
        public StyleHUD Shud { get; private set; } = null;

        public TextMeshProUGUI OurHeatResistanceFlavourText { get; private set; }
        public TextMeshProUGUI HeatResLabel { get; private set; }
        public TextMeshProUGUI HeatResPercentage { get; private set; }
        public TextMeshProUGUI HeatResFlashingText { get; private set; }
        public Image ScreenShatterImage { get; private set; }
        public float DefaultHurtingSoundPitch { get; private set; }
        public Vector2 BasePosition { get; private set; }
        public Vector3 BaseScale { get; private set; }
        public static HeatResistance OurHeatResistanceStatic = null;
        public static HeatResistance LastEnabledHeatRes { get; set; } = null;
        public static int MonoRegistrarIdx { get; private set; }
        
        internal static void Initialize()
        {
            MonoRegistrarIdx = Heck.MonoRegistrar.Register<HeatOfHeck>();
        }

        private void EnemyPostHurt(EventMethodCancelInfo cancelInfo, EnemyComponents enemy, GameObject target, Vector3 force, Vector3? hitPoint, float multiplier, bool tryForExplode, float critMultiplier, GameObject sourceWeapon, bool ignoreTotalDamageTakenMultiplier, bool fromExplosion)
        {
            if (!NyxLib.Cheats.IsCheatEnabled(Cheats.HeatOfHeck))
            {
                return;
            }

            if (sourceWeapon == null)
            {
                return;
            }
            
            var eid = enemy.Eid;

            if (eid.Dead)
            {
                return;
            }

            if (OurHeatResistance != null)
            {
                FieldPublisher<HeatResistance, float> heatResistance = new FieldPublisher<HeatResistance, float>(OurHeatResistance, "heatResistance");

                var options = Options.GetStyleRankOptions(Shud.GetStyleRank());

                float heatResExplosionLimit = options.ExplosiveAttacksHeatResThreshold.Value;

                if (heatResistance.Value <= heatResExplosionLimit && !fromExplosion)
                {
                    HeatResExplosion(multiplier + (multiplier * critMultiplier), hitPoint.GetValueOrDefault(eid.transform.position), false, out _);
                }

                if (heatResistance.Value <= 60.0f)
                {
                    eid.StartBurning(3.0f);
                }
            }
        }

        private bool HeatResExplosion(float multiplier, Vector3 hitPoint, bool forceDontHitPlayer, out float explosiveSize)
        {
            StyleRanks styleRank = Shud.GetStyleRank();
            var options = Options.GetStyleRankOptions(styleRank);

            bool superExplosive = CurrentHeatResistance < options.SuperExplosiveAttacksHeatResThreshold.Value;
            float explosiveSizeBase = options.HeatResExplosiveSizeBase.Value;
            float explosiveSizeNormMin = superExplosive ? options.SuperExplosiveAttacksHeatResExplosiveSizeNormMin.Value : options.HeatResExplosiveSizeNormMin.Value;
            float explosiveSizeNormMax = options.HeatResExplosiveSizeNormMax.Value;
            float explosiveDmgScalar = options.HeatResExplosiveDmgScalar.Value;
            bool explosiveDamagePlayer = options.HeatResExplosiveDmgPlayer.Value;

            explosiveSize = explosiveSizeBase * Mathf.Max(0.0f, explosiveSizeNormMin <= explosiveSizeNormMax ? NyxMath.NormalizeToRange(multiplier, explosiveSizeNormMin, explosiveSizeNormMax) : NyxMath.InverseNormalizeToRange(multiplier, explosiveSizeNormMin, explosiveSizeNormMax));

            if (explosiveSize < 0.01f)
            {
                return false;
            }

            if (NyxLib.Assets.ExplosionPrefab == null)
            {
                return false;
            }

            var explosionGo = UnityEngine.Object.Instantiate(NyxLib.Assets.ExplosionPrefab);
            explosionGo.transform.position = hitPoint;
            var explosion = explosionGo.GetComponentInChildren<Explosion>();

            explosion.damage = (int)(multiplier * explosiveDmgScalar);
            explosion.enemy = false;
            explosion.harmless = explosiveDmgScalar <= 0.0f;
            explosion.lowQuality = false;
            explosion.maxSize = explosiveSize;
            explosion.speed = Mathf.Pow(explosion.maxSize * 0.5f, 1.2f) * 0.095f;
            explosion.enemyDamageMultiplier = 1.0f;
            explosion.playerDamageOverride = -1;
            explosion.ignite = true;
            explosion.friendlyFire = false;
            explosion.isFup = false;
            explosion.hitterWeapon = "";
            explosion.halved = false;
            explosion.canHit = explosiveDamagePlayer && !forceDontHitPlayer ? AffectedSubjects.All : AffectedSubjects.EnemiesOnly;
            explosion.originEnemy = null;
            explosion.rocketExplosion = false;
            explosion.toIgnore = new System.Collections.Generic.List<EnemyType>();
            explosion.ultrabooster = false;
            explosion.unblockable = false;
            explosion.electric = false;

            explosionGo.SetActive(true);
            return true;
        }

        protected void Start()
        {
            player = NewMovement.Instance;
            Shud = StyleHUD.Instance;
            Instance = this;
        }

        protected void OnEnable()
        {
            PlayerEvents.PostHurt += PlayerPostHurt;
            EnemyComponents.PostAnyEnemyHurt += EnemyPostHurt;
            TextMeshProUGUIEvents.PostEnable += TextMeshProEnabled;
            TextMeshProUGUIEvents.PostDisable += TextMeshProDisabled;
        }

        protected void OnDisable()
        {
            PlayerEvents.PostHurt -= PlayerPostHurt;
            EnemyComponents.PostAnyEnemyHurt -= EnemyPostHurt;
            TextMeshProUGUIEvents.PostEnable -= TextMeshProEnabled;
            TextMeshProUGUIEvents.PostDisable -= TextMeshProDisabled;
        }

        private void TextMeshProEnabled(TextMeshProUGUI mesh)
        {
            var text = mesh.text;
            if (text.Contains("CRITICAL FAILURE"))
            {
                EarthMoverCritFailureSpawned = true;
            }

            if (text.Contains("INTRUDER DETECTED"))
            {
                EarthMoverFlushWarningSpawned = true;
            }
        }

        private void TextMeshProDisabled(TextMeshProUGUI mesh)
        {
            var text = mesh.text;
            if (text.Contains("CRITICAL FAILURE"))
            {
                EarthMoverCritFailureSpawned = false;
            }

            if (text.Contains("INTRUDER DETECTED"))
            {
                EarthMoverFlushWarningSpawned = false;
            }
        }

        public FieldInfo hrRechargingFI = AccessTools.Field(typeof(HeatResistance), "recharging");
        public bool Cooling => ((LastEnabledHeatRes != null) && (bool)(hrRechargingFI.GetValue(LastEnabledHeatRes)));

        public float CurrentHeatResistance = 0.0f;
        float HeatResistanceVel = 0.0f;
        float HeatResistanceDrain = 0.0f;
        float HeatResHurtTimer = -1.0f;
        float TimeSinceLastHeatResDeactivation = 0.0f;
        float TimeSinceLastHeatResActivation = 10.0f;
        float HeatResRankDescensionTimer = 4.0f;
        float HeatResRankDescensionTimerMax = 4.0f;

        bool EarthMoverCritFailureSpawned = false;
        bool EarthMoverFlushWarningSpawned = false;

        public void ResetHeatRestRankDescensionTimer()
        {
            HeatResRankDescensionTimer = HeatResRankDescensionTimerMax;
        }

        protected void FixedUpdate()
        {
            if (!NyxLib.Cheats.IsCheatEnabled(Cheats.HeatOfHeck))
            {
                if ((OurHeatResistance?.isActiveAndEnabled).GetValueOrDefault(false))
                {
                    OurHeatResistance.gameObject.SetActive(false);
                }

                return;
            }

            if (OurHeatResistance != null)
            {
                FieldPublisher<HeatResistance, float> heatResistance = new FieldPublisher<HeatResistance, float>(OurHeatResistance, "heatResistance");
                if (heatResistance.Value <= 50.0f && OurHeatResistance.isActiveAndEnabled)
                {
                    StainVoxelManager.Instance.TryIgniteAt(player.rb.transform.position);
                }
            }
            
            //if (OurHeatResistance != null && HeatResistance.Instance != null && HeatResistance.Instance != OurHeatResistance)
            //{
                //UnityEngine.Object.Destroy(OurHeatResistanceRootGo);
                //OurHeatResistance = null;
            //}
            if (OurHeatResistance == null)// && HeatResistance.Instance == null)
            {
                OurHeatResistanceRootGo = UnityEngine.Object.Instantiate(Assets.HeatResistancePrefab, CanvasController.Instance.gameObject.transform);
                OurHeatResistance = OurHeatResistanceRootGo.GetComponentInChildren<HeatResistance>(true);
                OurHeatResistanceStatic = OurHeatResistance;
                OurHeatResistance.enabled = true;
                OurHeatResistance.gameObject.SetActive(false);
                OurHeatResistanceRootGo.SetActive(true);
                OurHeatResistanceRootGo.transform.SetAsFirstSibling();
                
                //OurHeatResistance.gameObject.DebugPrintChildren();
                OurHeatResistanceFlavourText = OurHeatResistance.gameObject.transform.Find("Flavor Text").gameObject.GetComponent<TextMeshProUGUI>();
                HeatResLabel = OurHeatResistance.gameObject.transform.Find("Meter/Label").gameObject.GetComponent<TextMeshProUGUI>();
                HeatResPercentage = OurHeatResistance.gameObject.transform.Find("Meter/Fill Area/Fill/Percentage").gameObject.GetComponent<TextMeshProUGUI>();
                HeatResFlashingText = OurHeatResistance.gameObject.transform.Find("Warning").gameObject.GetComponent<TextMeshProUGUI>();

                OurHeatResistanceFlavourText.text = "YOU THINK YOU'RE SO GOOD? WELL YOU'D BETTER KEEP MOVING, BLOOD BUCKET";
                FieldPublisher<HeatResistance, GameObject> hurtingSound = new FieldPublisher<HeatResistance, GameObject>(OurHeatResistance, "hurtingSound");
                FieldPublisher<HeatResistance, Image> screenShatter = new FieldPublisher<HeatResistance, Image>(OurHeatResistance, "screenShatter");
                ScreenShatterImage = screenShatter.Value;
                ScreenShatterImage.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1.0f);
                ScreenShatterImage.GetComponent<RectTransform>().anchorMin = new Vector2(1.0f, 1.0f);
                ScreenShatterImage.GetComponent<RectTransform>().anchoredPosition += (Vector2.left * 350.0f) + (Vector2.down * 75.0f);
                var screenShatterEuler = ScreenShatterImage.GetComponent<RectTransform>().eulerAngles;
                screenShatterEuler.z = screenShatterEuler.z + 30;
                ScreenShatterImage.GetComponent<RectTransform>().eulerAngles = screenShatterEuler;
                DefaultHurtingSoundPitch = hurtingSound.Value.GetComponent<AudioSource>().pitch;
                BasePosition = OurHeatResistance.GetComponent<RectTransform>().anchoredPosition;
                //BaseScale = OurHeatResistance.transform.localScale; WRONG because it does a scale effect when it enables lol
                
                //FieldInfo heatResInstanceFI = typeof(MonoSingleton<HeatResistance>).GetField("Instance", BindingFlags.Static | BindingFlags.NonPublic);
                //heatResInstanceFI.SetValue(null, null);
            }
            

            
            var styleRankOptions = Options.GetStyleRankOptions(Shud.GetStyleRank());
            float revolverCoolingScalar = 1.0f;
            revolverCoolingScalar = Mathf.Lerp(1.0f, 0.4f, Mathf.Clamp(NyxMath.NormalizeToRange(RevolverTwirlThisUpdate * (GunControl.Instance.dualWieldCount + 1) * styleRankOptions.RevolverCoolingScalar.Value, 0.1f, 3.0f), 0.0f, 1.0f));
            RevolverTwirlThisUpdate = 0.0f;

            TimeSinceLastHeatResActivation += Time.fixedDeltaTime;
            TimeSinceLastHeatResDeactivation += Time.fixedDeltaTime;

            if (OurHeatResistance != null)
            {
                FieldPublisher<BossBarManager, Dictionary<int, BossHealthBarTemplate>> bossBars = new FieldPublisher<BossBarManager, Dictionary<int, BossHealthBarTemplate>>(BossBarManager.Instance, "bossBars");
                
                float pushDownFactor = 0.0f;

                if (bossBars.Value.Count == 1)
                {
                    pushDownFactor += 90.0f;
                }
                else if (bossBars.Value.Count == 2)
                {
                    pushDownFactor += 170.0f;
                }
                else if (bossBars.Value.Count == 3)
                {
                    pushDownFactor += bossBars.Value.Count * 55;
                }
                else if (bossBars.Value.Count == 4)
                {
                    pushDownFactor += bossBars.Value.Count * 45;
                }
                else if (bossBars.Value.Count == 5)
                {
                    pushDownFactor += bossBars.Value.Count * 35;
                }
                else if (bossBars.Value.Count >= 6)
                {
                    pushDownFactor += bossBars.Value.Count * 27.5f;
                }

                bool hasHeatResistanceThatsNotUs = false;

                if (OurHeatResistance != null && LastEnabledHeatRes != null && LastEnabledHeatRes != OurHeatResistance && (LastEnabledHeatRes.NullInvalid()?.isActiveAndEnabled).GetValueOrDefault(false))
                {
                    pushDownFactor += 140.0f;
                    hasHeatResistanceThatsNotUs = true;
                }

                if (EarthMoverCritFailureSpawned)
                {
                    pushDownFactor += 225.0f;
                }

                if (EarthMoverFlushWarningSpawned)
                {
                    pushDownFactor = Mathf.Max(pushDownFactor, 110.0f);
                }
                
                pushDownFactor *= 0.7f;
                
                var rectTransform = OurHeatResistance.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = NyxMath.EaseInterpTo(rectTransform.anchoredPosition, BasePosition + Vector2.down * pushDownFactor, 10.0f, Time.fixedDeltaTime);
                
                float heatResistanceRecovery = player.rb.velocity.magnitude;

                StyleRanks styleRank = Shud.GetStyleRank();
                var options = Options.GetStyleRankOptions(styleRank);
                OurHeatResistance.speed = 0.0f;
                heatResistanceRecovery *= options.HeatResRecovery.Value;
                HeatResistanceDrain = options.HeatResDrain.Value;

                HeatResistanceDrain = HeatResistanceDrain * ((player.touchingWaters.Count > 0) ? Options.WaterHeatingScalar.Value : 1.0f);
                HeatResistanceDrain *= player.fakeFallRequests > 0 ? 0.85f : 1.0f;

                if (OurHeatResistance.isActiveAndEnabled && HeatResistanceDrain <= 0.0f)
                {
                    OurHeatResistance.gameObject.SetActive(false);
                    TimeSinceLastHeatResDeactivation = 0.0f;
                }
                else if (!OurHeatResistance.isActiveAndEnabled && HeatResistanceDrain > 0.0f)
                {
                    OurHeatResistance.gameObject.SetActive(true);
                    HeatResHurtTimer = 0.75f;

                    if (TimeSinceLastHeatResActivation > 5.0f)
                    {
                        Shud.AddPoints(75, "<color=#ff9b31>ON FIRE</color>", null, null, -1, "", "");
                    }

                    TimeSinceLastHeatResActivation = 0.0f;
                    CurrentHeatResistance = 100.0f;
                }

                if (TimeSinceLastHeatResActivation > 5.0f && TimeSinceLastHeatResActivation % 15.0f < Time.fixedDeltaTime * 0.75f && OurHeatResistance.isActiveAndEnabled)
                {
                    Shud.AddPoints(75, "<color=#ffc131>HEATED AND UNBOTHERED</color>");
                }

                int heatResHurtDmg = 6;
                heatResHurtDmg = Mathf.RoundToInt((float)heatResHurtDmg * NyxMath.InverseNormalizeToRange(CurrentHeatResistance, 0.0f, 100.0f));
                if (HeatResistanceDrain > 0.0f && player.hp > 25 && CurrentHeatResistance < 25.0f && HeatResHurtTimer <= 0.0f)
                {
                    player.GetHurt(Math.Min(Math.Max((player.hp - (20 + heatResHurtDmg)), 0), heatResHurtDmg), false, 0.0f, false, false, 0.0f, false);
                    HeatResHurtTimer = 2.0f;
                }

                HeatResHurtTimer -= Time.fixedDeltaTime;

                FieldPublisher<HeatResistance, float> appliedHeatResistance = new FieldPublisher<HeatResistance, float>(OurHeatResistance, "heatResistance");
                FieldPublisher<HeatResistance, GameObject> hurtingSound = new FieldPublisher<HeatResistance, GameObject>(OurHeatResistance, "hurtingSound");
                
                float scaledHeatResistanceDrain = HeatResistanceDrain;
                
                if (CurrentHeatResistance < -95.0f)
                {
                    scaledHeatResistanceDrain *= 0.7f;
                }
                else if (CurrentHeatResistance < -50.0f)
                {
                    scaledHeatResistanceDrain *= 0.8f;
                }
                else if (CurrentHeatResistance <= 0.0f)
                {
                    scaledHeatResistanceDrain *= 0.9f;
                }                
                else if (CurrentHeatResistance <= 35.0f)
                {
                    scaledHeatResistanceDrain *= 1.0f;
                }
                else if (CurrentHeatResistance <= 60.0f)
                {
                    scaledHeatResistanceDrain *= 1.1f;
                }
                else if (CurrentHeatResistance <= 80.0f)
                {
                    scaledHeatResistanceDrain *= 1.2f;
                }             
                else if (CurrentHeatResistance <= 100.0f)
                {
                    scaledHeatResistanceDrain *= 1.3f;
                }

                scaledHeatResistanceDrain *= revolverCoolingScalar;

                float targetVel = Cooling ? heatResistanceRecovery + Options.CoolingChamberCoolingRate.Value : (heatResistanceRecovery - scaledHeatResistanceDrain);
                HeatResistanceVel = Mathf.MoveTowards(HeatResistanceVel, targetVel, Time.fixedDeltaTime * (HeatResistanceDrain * 6.0f) * (targetVel > HeatResistanceVel ? 1.0f : 0.35f));
                HeatResistanceVel = Mathf.Clamp(HeatResistanceVel, -HeatResistanceDrain, HeatResistanceDrain * 3.0f);

                CurrentHeatResistance = Mathf.Clamp(CurrentHeatResistance + (HeatResistanceVel * Time.fixedDeltaTime), -100.0f, 100.0f);

                //CurrentHeatResistance = Mathf.MoveTowards(CurrentHeatResistance, 100.0f, (Time.fixedDeltaTime * heatResistanceRecovery));

                appliedHeatResistance.Value = Mathf.Max(CurrentHeatResistance, 0.0f);
                
                bool otherHeatResEnabled = hasHeatResistanceThatsNotUs && (LastEnabledHeatRes.NullInvalid()?.isActiveAndEnabled).GetValueOrDefault(false);

                if (CurrentHeatResistance < Options.StageOptions4.Threshold.Value)
                {
                    player.ForceAntiHP((float)Options.StageOptions4.AdditionalAntiHPGain.Value * Time.fixedDeltaTime, silent: true, dontOverwriteHp: false, addToCooldown: true, stopInstaHeal: true);
                    hurtingSound.Value.GetComponent<AudioSource>().pitch = DefaultHurtingSoundPitch + 0.4f + UnityEngine.Random.Range(0.0f, 0.1f);
                    
                    switch (UnityEngine.Random.Range(0, 4))
                    {
                        case 0:
                        HeatResFlashingText.text = $"S{(char)UnityEngine.Random.Range(33, 96)}{(char)UnityEngine.Random.Range(33, 96)}T{(char)UnityEngine.Random.Range(33, 96)}O{(char)UnityEngine.Random.Range(33, 96)}{(char)UnityEngine.Random.Range(33, 96)}P{(char)UnityEngine.Random.Range(33, 96)}I{(char)UnityEngine.Random.Range(33, 96)}T";
                        break;
                        case 1:
                        HeatResFlashingText.text = $"I{(char)UnityEngine.Random.Range(33, 96)}T{(char)UnityEngine.Random.Range(33, 96)}{(char)UnityEngine.Random.Range(33, 96)}B{(char)UnityEngine.Random.Range(33, 96)}{(char)UnityEngine.Random.Range(33, 96)}U{(char)UnityEngine.Random.Range(33, 96)}{(char)UnityEngine.Random.Range(33, 96)}RN{(char)UnityEngine.Random.Range(33, 96)}S";
                        break;
                        case 2:
                        HeatResFlashingText.text = $"AAAAAAA";
                        break;
                        case 3:
                        default:
                        HeatResFlashingText.text = $"E{(char)UnityEngine.Random.Range(33, 96)}{(char)UnityEngine.Random.Range(33, 96)}{(char)UnityEngine.Random.Range(33, 96)}R{(char)UnityEngine.Random.Range(33, 96)}{(char)UnityEngine.Random.Range(33, 96)}R{(char)UnityEngine.Random.Range(33, 96)}{(char)UnityEngine.Random.Range(33, 96)}O{(char)UnityEngine.Random.Range(33, 96)}{(char)UnityEngine.Random.Range(33, 96)}R{(char)UnityEngine.Random.Range(33, 96)}";
                            break;
                    }

                    HeatResRankDescensionTimer += Time.fixedDeltaTime * Options.StageOptions4.RankDescensionTimerChange.Value;
                    
                    if (otherHeatResEnabled)
                    {
                        var otherHeatRes = LastEnabledHeatRes;
                        FieldPublisher<HeatResistance, float> otherHeatResistance = new FieldPublisher<HeatResistance, float>(otherHeatRes, "heatResistance");
                        otherHeatResistance.Value = Mathf.MoveTowards(otherHeatResistance.Value, 0.0f, Time.fixedDeltaTime * otherHeatRes.speed * Options.StageOptions4.HeatResSpeedup.Value);
                    }
                }
                else if (CurrentHeatResistance < Options.StageOptions3.Threshold.Value)
                {
                    player.ForceAntiHP((float)Options.StageOptions3.AdditionalAntiHPGain.Value * Time.fixedDeltaTime, silent: true, dontOverwriteHp: false, addToCooldown: true, stopInstaHeal: true);
                    hurtingSound.Value.GetComponent<AudioSource>().pitch = DefaultHurtingSoundPitch + 0.1f;
                    HeatResFlashingText.text = "CRITICAL";
                    HeatResRankDescensionTimer += Time.fixedDeltaTime * Options.StageOptions3.RankDescensionTimerChange.Value;
                    
                    if (otherHeatResEnabled)
                    {
                        var otherHeatRes = LastEnabledHeatRes;
                        FieldPublisher<HeatResistance, float> otherHeatResistance = new FieldPublisher<HeatResistance, float>(otherHeatRes, "heatResistance");
                        otherHeatResistance.Value = Mathf.MoveTowards(otherHeatResistance.Value, 0.0f, Time.fixedDeltaTime * otherHeatRes.speed * Options.StageOptions3.HeatResSpeedup.Value);
                    }
                }
                else if (CurrentHeatResistance <= Options.StageOptions2.Threshold.Value)
                {
                    hurtingSound.Value.GetComponent<AudioSource>().pitch = DefaultHurtingSoundPitch;
                    HeatResFlashingText.text = "WARNING:";
                    HeatResRankDescensionTimer += Time.fixedDeltaTime * Options.StageOptions2.RankDescensionTimerChange.Value;
                    player.ForceAntiHP((float)Options.StageOptions2.AdditionalAntiHPGain.Value * Time.fixedDeltaTime, silent: true, dontOverwriteHp: false, addToCooldown: true, stopInstaHeal: true);

                    if (otherHeatResEnabled)
                    {
                        var otherHeatRes = HeatResistance.Instance;
                        FieldPublisher<HeatResistance, float> otherHeatResistance = new FieldPublisher<HeatResistance, float>(otherHeatRes, "heatResistance");
                        otherHeatResistance.Value = Mathf.MoveTowards(otherHeatResistance.Value, 0.0f, Time.fixedDeltaTime * otherHeatRes.speed * Options.StageOptions2.HeatResSpeedup.Value);
                    }
                }
                else if (CurrentHeatResistance <= Options.StageOptions1.Threshold.Value)
                {
                    hurtingSound.Value.GetComponent<AudioSource>().pitch = DefaultHurtingSoundPitch;
                    HeatResFlashingText.text = "WARNING:";
                    HeatResRankDescensionTimer += Time.fixedDeltaTime * Options.StageOptions1.RankDescensionTimerChange.Value;
                }
                else
                {
                    hurtingSound.Value.GetComponent<AudioSource>().pitch = DefaultHurtingSoundPitch;
                    HeatResFlashingText.text = "WARNING:";
                    HeatResRankDescensionTimer += Time.fixedDeltaTime * Options.StageOptions0.RankDescensionTimerChange.Value;
                }

                HeatResRankDescensionTimer = Mathf.Min(HeatResRankDescensionTimer, HeatResRankDescensionTimerMax);

                if (HeatResRankDescensionTimer <= 0.0f)
                {
                    Shud.DescendRank();
                    ResetHeatRestRankDescensionTimer();
                    CurrentHeatResistance = Mathf.Max(CurrentHeatResistance, -10.0f);
                }
            }
        }

        protected void LateUpdate()
        {
            if (!NyxLib.Cheats.IsCheatEnabled(Cheats.HeatOfHeck))
            {
                return;
            }

            if (OurHeatResistance == null)
            {
                return;
            }

            float temperature = Mathf.Lerp(60.0f, 140.0f, NyxMath.InverseNormalizeToRange(CurrentHeatResistance, -100.0f, 100.0f));
            HeatResLabel.text = $"INTERNAL TEMPERATURE - {temperature:F1}C";
            HeatResPercentage.text = $"{temperature:F2}C";
        }

        private void PlayerPostHurt(EventMethodCancelInfo cancelInfo, PlayerComponents player, int unprocessedDamage, int processedDamage, bool invincible, float scoreLossMultiplier, bool explosion, bool instablack, float hardDamageMultiplier, bool ignoreInvincibility)
        {
            if (!NyxLib.Cheats.IsCheatEnabled(Cheats.HeatOfHeck))
            {
                return;
            }

            if (processedDamage <= 8)
            {
                return;
            }

            var newMovement = player.NewMovement;

            if (OurHeatResistance != null && !explosion && scoreLossMultiplier > 0.0f)
            {
                if (CurrentHeatResistance <= -50.0f)
                {
                    HeatResExplosion(processedDamage * 0.25f, newMovement.rb.transform.position, true, out float explosiveSize);
                    newMovement.GetHurt(Mathf.RoundToInt(processedDamage * 0.3f), false, 0.0f, true, false, 0.35f, true);
                    newMovement.Launch(Vector3.up, explosiveSize * 3.0f, true);
                }
                else if (CurrentHeatResistance <= -10.0f)
                {
                    HeatResExplosion(processedDamage * 0.25f, newMovement.rb.transform.position, true, out float explosiveSize);
                    newMovement.GetHurt(Mathf.RoundToInt(processedDamage * 0.2f), false, 0.0f, true, false, 0.35f, true);
                    newMovement.Launch(Vector3.up, explosiveSize * 2.0f, true);
                }
            }
        }

        GameObject OurHeatResistanceRootGo = null;
        public HeatResistance OurHeatResistance = null;
    }

    [HarmonyPatch(typeof(HeatResistance), "OnEnable")]
    static class HeatResistanceEnablePatch
    {
        public static void Prefix(HeatResistance __instance)
        {
    
        }

        public static void Postfix(HeatResistance __instance)
        {
            if (__instance == HeatOfHeck.OurHeatResistanceStatic)
            {
                return;
            }

            HeatOfHeck.LastEnabledHeatRes = __instance;
        }
    }
}