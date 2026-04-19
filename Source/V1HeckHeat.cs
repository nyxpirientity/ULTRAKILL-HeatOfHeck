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
    public class V1HeckHeat : MonoBehaviour
    {
        public static HeatOfHeck Instance = null;

        public NewMovement player { get; private set; } = null;
        public StyleHUD Shud { get; private set; } = null;

        public static int MonoRegistrarIdx { get; private set; }
        
        internal static void Initialize()
        {
            MonoRegistrarIdx = PlayerComponents.MonoRegistrar.Register<V1HeckHeat>();
        }

        protected void Start()
        {
            player = NewMovement.Instance;
            Shud = StyleHUD.Instance;
            LastContactDamageReset.UpdateToNow();
        }

        int PuppetContactDamageStyleThisTick = 0;
        int NormalContactDamageStyleThisTick = 0;
        int MiniBossContactDamageStyleThisTick = 0;
        int BossContactDamageStyleThisTick = 0;
        int UltraBossContactDamageStyleThisTick = 0;
        HashSet<GameObject> SafeFromContactDamage = new HashSet<GameObject>(128);
        SceneTimeStamp LastContactDamageReset;
        SceneTimeStamp LastContactDamageStyleReset;

        protected void OnCollisionStay(Collision collision)
        {
            if (NyxLib.Cheats.IsCheatDisabled(Cheats.HeatOfHeck))
            {
                return;
            }

            EnemyIdentifier eid = collision.gameObject.GetComponent<EnemyIdentifier>() ?? collision.gameObject.GetComponentInChildren<EnemyIdentifier>() ?? collision.gameObject.GetComponentInParent<EnemyIdentifier>();
            
            if (eid == null)
            {
                return;
            }

            if (!((HeatOfHeck.Instance.OurHeatResistance?.isActiveAndEnabled).GetValueOrDefault(false)))
            {
                return;
            }

            if (SafeFromContactDamage.Contains(eid.gameObject))
            {
                return;
            }

            if (eid.Dead)
            {
                return;
            }

            if (HeatOfHeck.Instance.CurrentHeatResistance <= 35.0f)
            {
                float burnStrength = NyxMath.InverseNormalizeToRange(HeatOfHeck.Instance.CurrentHeatResistance, -100.0f, 40.0f);
                eid.ApplyDamage(Vector3.Normalize(eid.transform.position - player.transform.position) * burnStrength * 10.0f, eid.transform.position, burnStrength * Options.ContactDamageMaxDamage.Value, 0.0f, null, false);
                SafeFromContactDamage.Add(eid.gameObject);
                if (eid.Dead)
                {
                    if (eid.puppet)
                    {
                        PuppetContactDamageStyleThisTick += 1;
                    }
                    else
                    {
                        switch (EnemyUtils.GetEnemyGameplayRank(eid))
                        {
                            case EnemyGameplayRank.Normal:
                                NormalContactDamageStyleThisTick += 1;
                                break;
                            case EnemyGameplayRank.Miniboss:
                                MiniBossContactDamageStyleThisTick += 1;
                                break;
                            case EnemyGameplayRank.Boss:
                                BossContactDamageStyleThisTick += 1;
                                break;
                            case EnemyGameplayRank.Ultraboss:
                                UltraBossContactDamageStyleThisTick += 1;
                                break;
                        }
                    }
                }
            }
        }

        protected void FixedUpdate()
        {
            if (!NyxLib.Cheats.IsCheatEnabled(Cheats.HeatOfHeck))
            {
                return;
            }
            
            if (LastContactDamageReset.TimeSince > Options.ContactDamageResetTime.Value)
            {
                SafeFromContactDamage.Clear();
                LastContactDamageReset.UpdateToNow();
            }

            if (LastContactDamageStyleReset.TimeSince > 2.0)
            {
                if (PuppetContactDamageStyleThisTick > 0) Shud.AddPoints(5 * PuppetContactDamageStyleThisTick, $"<color=#00ff44>SUPER HEATED</color>", gameObject, null, PuppetContactDamageStyleThisTick);
                if (NormalContactDamageStyleThisTick > 0) Shud.AddPoints(75 * NormalContactDamageStyleThisTick, $"<color=#00ff44>CONTACT DAMAGE</color>", gameObject, null, NormalContactDamageStyleThisTick);
                if (MiniBossContactDamageStyleThisTick > 0) Shud.AddPoints(250 * MiniBossContactDamageStyleThisTick, $"<color=#00fff7>BRANDED</color>", gameObject, null, MiniBossContactDamageStyleThisTick);
                if (BossContactDamageStyleThisTick > 0) Shud.AddPoints(500 * BossContactDamageStyleThisTick, $"<color=#a1f3ff>BRANDING STEEL</color>", gameObject, null, BossContactDamageStyleThisTick);
                if (UltraBossContactDamageStyleThisTick > 0) Shud.AddPoints(5000 * UltraBossContactDamageStyleThisTick, $"<color=#ffb700>NEW EXHIBIT</color>", gameObject, null, UltraBossContactDamageStyleThisTick);
                LastContactDamageStyleReset.UpdateToNow();
                NormalContactDamageStyleThisTick = 0;
                MiniBossContactDamageStyleThisTick = 0;
                BossContactDamageStyleThisTick = 0;
                UltraBossContactDamageStyleThisTick = 0;
                PuppetContactDamageStyleThisTick = 0;
            }
        }
    }
}