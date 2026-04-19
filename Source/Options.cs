using System;
using System.Collections.Generic;
using System.ComponentModel;
using BepInEx.Configuration;
using Nyxpiri.ULTRAKILL.NyxLib;

namespace Nyxpiri.ULTRAKILL.HeatOfHeck
{
    public static class Options
    {
        public static StyleRankOptions DestructiveOptions { get; private set; } = null;
        public static StyleRankOptions ChaoticOptions { get; private set; } = null;
        public static StyleRankOptions BrutalOptions { get; private set; } = null;
        public static StyleRankOptions AnarchicOptions { get; private set; } = null;
        public static StyleRankOptions SupremeOptions { get; private set; } = null;
        public static StyleRankOptions SSadisticOptions { get; private set; } = null;
        public static StyleRankOptions SSSensoredStormOptions { get; private set; } = null;
        public static StyleRankOptions ULTRAKILLOptions { get; private set; } = null;

        public static ConfigEntry<float> ContactDamageMaxDamage { get; private set; } = null;
        public static ConfigEntry<float> ContactDamageResetTime { get; private set; } = null;
        public static ConfigEntry<float> WaterHeatingScalar { get; private set; } = null;
        public static ConfigEntry<float> CoolingChamberCoolingRate { get; private set; } = null;
        public static HeatResistanceStageOptions StageOptions2 { get; private set; }
        public static HeatResistanceStageOptions StageOptions3 { get; private set; }
        public static HeatResistanceStageOptions StageOptions4 { get; private set; }
        public static HeatResistanceStageOptions StageOptions0 { get; private set; }
        public static HeatResistanceStageOptions StageOptions1 { get; private set; }

        public static StyleRankOptions GetStyleRankOptions(StyleRanks rank, StyleRanks nullRank = StyleRanks.Destructive)
        {
            switch (rank)
            {
                case StyleRanks.Null:
                if (nullRank == StyleRanks.Null)
                {
                    throw new InvalidEnumArgumentException("Null style rank has no options, and for some reason caller requested null style rank options");
                }
                else
                {
                    return GetStyleRankOptions(nullRank);
                }
                case StyleRanks.Destructive:
                    return DestructiveOptions;
                case StyleRanks.Chaotic:
                    return ChaoticOptions;
                case StyleRanks.Brutal:
                    return BrutalOptions;
                case StyleRanks.Anarchic:
                    return AnarchicOptions;
                case StyleRanks.Supreme:
                    return SupremeOptions;
                case StyleRanks.SSadistic:
                    return SSadisticOptions;
                case StyleRanks.SSSensoredStorm:
                    return SSSensoredStormOptions;
                case StyleRanks.ULTRAKILL:
                    return ULTRAKILLOptions;
                default:
                    throw new NotImplementedException();
            }
        }

        internal static void Initialize()
        {
            ContactDamageMaxDamage = Config.Bind("Balance", "ContactDamageMaxDamage", 5.0f);
            ContactDamageResetTime = Config.Bind("Balance", "ContactDamageResetTime", 0.3f);

            DestructiveOptions = new StyleRankOptions(StyleRanks.Destructive, Config, 
                heatResDrain: -1.0f,
                heatResRecovery: 100.0f,
                heatResExplosiveSizeBase: -1.0f,
                heatResExplosiveSizeNormMin: -1.0f,
                heatResExplosiveSizeNormMax: -1.0f,
                heatResExplosiveDmgScalar: -1.0f,
                heatResExplosiveDmgPlayer: false,
                explosiveAttacksHeatResThreshold: -40.0f,
                superExplosiveAttacksHeatResThreshold: -150.0f,
                revolverTwirlCoolingScalar: 0.3f,
                superExplosiveAttacksHeatResExplosiveSizeNormMin: 0.0f
            );

            ChaoticOptions = new StyleRankOptions(StyleRanks.Chaotic, Config, 
                heatResDrain: -1.0f,
                heatResRecovery: 100.0f,
                heatResExplosiveSizeBase: -1.0f,
                heatResExplosiveSizeNormMin: -1.0f,
                heatResExplosiveSizeNormMax: -1.0f,
                heatResExplosiveDmgScalar: -1.0f,
                heatResExplosiveDmgPlayer: false,
                explosiveAttacksHeatResThreshold: -40.0f,
                superExplosiveAttacksHeatResThreshold: -150.0f,
                revolverTwirlCoolingScalar: 0.3f,
                superExplosiveAttacksHeatResExplosiveSizeNormMin: 0.0f
            );

            BrutalOptions = new StyleRankOptions(StyleRanks.Brutal, Config, 
                heatResDrain: -1.0f,
                heatResRecovery: 100.0f,
                heatResExplosiveSizeBase: -1.0f,
                heatResExplosiveSizeNormMin: -1.0f,
                heatResExplosiveSizeNormMax: -1.0f,
                heatResExplosiveDmgScalar: -1.0f,
                heatResExplosiveDmgPlayer: false,
                explosiveAttacksHeatResThreshold: -40.0f,
                superExplosiveAttacksHeatResThreshold: -150.0f,
                revolverTwirlCoolingScalar: 0.3f,
                superExplosiveAttacksHeatResExplosiveSizeNormMin: 0.0f
            );

            AnarchicOptions = new StyleRankOptions(StyleRanks.Anarchic, Config, 
                heatResDrain: 30.0f,
                heatResRecovery: 1.7f,
                heatResExplosiveSizeBase: -1.0f,
                heatResExplosiveSizeNormMin: -1.0f,
                heatResExplosiveSizeNormMax: -1.0f,
                heatResExplosiveDmgScalar: -1.0f,
                heatResExplosiveDmgPlayer: false,
                explosiveAttacksHeatResThreshold: -40.0f,
                superExplosiveAttacksHeatResThreshold: -150.0f,
                revolverTwirlCoolingScalar: 0.3f,
                superExplosiveAttacksHeatResExplosiveSizeNormMin: 0.0f
            );

            SupremeOptions = new StyleRankOptions(StyleRanks.Supreme, Config, 
                heatResDrain: 60.0f,
                heatResRecovery: 2.0f,
                heatResExplosiveSizeBase: -1.0f,
                heatResExplosiveSizeNormMin: -1.0f,
                heatResExplosiveSizeNormMax: -1.0f,
                heatResExplosiveDmgScalar: -1.0f,
                heatResExplosiveDmgPlayer: false,
                explosiveAttacksHeatResThreshold: -40.0f,
                superExplosiveAttacksHeatResThreshold: -100.0f,
                revolverTwirlCoolingScalar: 0.3f,
                superExplosiveAttacksHeatResExplosiveSizeNormMin: 0.0f
            );

            SSadisticOptions = new StyleRankOptions(StyleRanks.SSadistic, Config, 
                heatResDrain: 70.0f,
                heatResRecovery: 2.0f,
                heatResExplosiveSizeBase: 12.0f,
                heatResExplosiveSizeNormMin: 1.5f,
                heatResExplosiveSizeNormMax: 7.0f,
                heatResExplosiveDmgScalar: 0.35f,
                heatResExplosiveDmgPlayer: true,
                explosiveAttacksHeatResThreshold: -5.0f,
                superExplosiveAttacksHeatResThreshold: -50.0f,
                revolverTwirlCoolingScalar: 0.3f,
                superExplosiveAttacksHeatResExplosiveSizeNormMin: -1.0f
            );

            SSSensoredStormOptions = new StyleRankOptions(StyleRanks.SSSensoredStorm, Config, 
                heatResDrain: 75.0f,
                heatResRecovery: 2.1f,
                heatResExplosiveSizeBase: 14.0f,
                heatResExplosiveSizeNormMin: 0.01f,
                heatResExplosiveSizeNormMax: 8.5f,
                heatResExplosiveDmgScalar: 0.5f,
                heatResExplosiveDmgPlayer: true,
                explosiveAttacksHeatResThreshold: 5.0f,
                superExplosiveAttacksHeatResThreshold: -30.0f,
                revolverTwirlCoolingScalar: 0.25f,
                superExplosiveAttacksHeatResExplosiveSizeNormMin: -1.0f
            );

            ULTRAKILLOptions = new StyleRankOptions(StyleRanks.ULTRAKILL, Config, 
                heatResDrain: 100.0f,
                heatResRecovery: 2.4f,
                heatResExplosiveSizeBase: 20.0f,
                heatResExplosiveSizeNormMin: 0.01f,
                heatResExplosiveSizeNormMax: 6.5f,
                heatResExplosiveDmgScalar: 0.8f,
                heatResExplosiveDmgPlayer: true,
                explosiveAttacksHeatResThreshold: 10.0f,
                superExplosiveAttacksHeatResThreshold: -10.0f,
                revolverTwirlCoolingScalar: 0.3f,
                superExplosiveAttacksHeatResExplosiveSizeNormMin: -1.0f
            );

            CoolingChamberCoolingRate = Config.Bind("Balance.General", "CoolingChamberCoolingRate", 30.0f);
            WaterHeatingScalar = Config.Bind("Balance.General", "WaterHeatingScalar", 0.7f);
        
            StageOptions4 = new HeatResistanceStageOptions(4, Config, additionalAntiHPGain: 50.0f, threshold: -95.0f, rankDescensionTimerChange: -2.0f, vanillaHeatResSpeedup: 2.0f);
            StageOptions3 = new HeatResistanceStageOptions(3, Config, additionalAntiHPGain: 35.0f, threshold: -50.0f, rankDescensionTimerChange: -1.25f, vanillaHeatResSpeedup: 1.5f);
            StageOptions2 = new HeatResistanceStageOptions(2, Config, additionalAntiHPGain: 10.0f, threshold: 0.0f, rankDescensionTimerChange: -0.5f, vanillaHeatResSpeedup: 1.0f);
            StageOptions1 = new HeatResistanceStageOptions(1, Config, additionalAntiHPGain: 0.0f, threshold: 50.0f, rankDescensionTimerChange: 1.25f, vanillaHeatResSpeedup: null);
            StageOptions0 = new HeatResistanceStageOptions(0, Config, additionalAntiHPGain: 0.0f, threshold: null, rankDescensionTimerChange: 2.0f, vanillaHeatResSpeedup: null);
        }
        
        internal static ConfigFile Config = null;
    }

    public class StyleRankOptions
    {
        public StyleRankOptions(StyleRanks styleRank, ConfigFile config, float heatResDrain, float heatResRecovery, float heatResExplosiveSizeBase, float heatResExplosiveSizeNormMin, float heatResExplosiveSizeNormMax, float heatResExplosiveDmgScalar, bool heatResExplosiveDmgPlayer, float explosiveAttacksHeatResThreshold, float superExplosiveAttacksHeatResThreshold, float superExplosiveAttacksHeatResExplosiveSizeNormMin, float revolverTwirlCoolingScalar)
        {
            string category = $"{styleRank}";
            HeatResDrain = config.Bind(category, "HeatResDrain", heatResDrain);
            HeatResRecovery = config.Bind(category, "HeatResRecovery", heatResRecovery);
            HeatResExplosiveSizeBase = config.Bind(category, "HeatResExplosiveSizeBase", heatResExplosiveSizeBase);
            HeatResExplosiveSizeNormMin = config.Bind(category, "HeatResExplosiveSizeNormMin", heatResExplosiveSizeNormMin);
            HeatResExplosiveSizeNormMax = config.Bind(category, "HeatResExplosiveSizeNormMax", heatResExplosiveSizeNormMax);
            HeatResExplosiveDmgScalar = config.Bind(category, "HeatResExplosiveDmgScalar", heatResExplosiveDmgScalar);
            HeatResExplosiveDmgPlayer = config.Bind(category, "HeatResExplosiveDmgPlayer", heatResExplosiveDmgPlayer);
            RevolverCoolingScalar = config.Bind(category, "RevolverTwirlCoolingScalar", revolverTwirlCoolingScalar);
            
            ExplosiveAttacksHeatResThreshold = config.Bind(category, "ExplosiveAttacksHeatResThreshold", explosiveAttacksHeatResThreshold);
            SuperExplosiveAttacksHeatResThreshold = config.Bind(category, "SuperExplosiveAttacksHeatResThreshold", superExplosiveAttacksHeatResThreshold);
            SuperExplosiveAttacksHeatResExplosiveSizeNormMin = config.Bind(category, "SuperExplosiveAttacksHeatResExplosiveSizeNormMin", superExplosiveAttacksHeatResExplosiveSizeNormMin);
        }

        public ConfigEntry<float> HeatResDrain { get; private set; } = null;
        public ConfigEntry<float> HeatResRecovery { get; private set; } = null;
        public ConfigEntry<float> HeatResExplosiveSizeBase { get; private set; } = null;
        public ConfigEntry<float> HeatResExplosiveSizeNormMin { get; private set; } = null;
        public ConfigEntry<float> HeatResExplosiveSizeNormMax { get; private set; } = null;
        public ConfigEntry<float> HeatResExplosiveDmgScalar { get; private set; } = null;
        public ConfigEntry<bool> HeatResExplosiveDmgPlayer { get; private set; } = null;

        public ConfigEntry<float> ExplosiveAttacksHeatResThreshold { get; private set; } = null;
        public ConfigEntry<float> SuperExplosiveAttacksHeatResExplosiveSizeNormMin { get; private set; } = null;
        public ConfigEntry<float> SuperExplosiveAttacksHeatResThreshold { get; private set; } = null;
        public ConfigEntry<float> RevolverCoolingScalar { get; private set; } = null;
    }

    public class HeatResistanceStageOptions
    {
        public HeatResistanceStageOptions(int stageNum, ConfigFile config, float? additionalAntiHPGain, float? threshold, float? vanillaHeatResSpeedup, float rankDescensionTimerChange)
        {
            string category = $"HeatResStage{stageNum}";
            
            if (threshold.HasValue)
            {
                Threshold = config.Bind(category, "StageThreshold", threshold.Value);
            }

            if (additionalAntiHPGain.HasValue)
            {
                AdditionalAntiHPGain = config.Bind(category, "AdditionalAntiHPGain", additionalAntiHPGain.Value);
            }

            if (vanillaHeatResSpeedup.HasValue)
            {
                HeatResSpeedup = config.Bind(category, "VanillaHeatResSpeedup", vanillaHeatResSpeedup.Value);
            }

            RankDescensionTimerChange = config.Bind(category, "RankDescensionTimerChange", rankDescensionTimerChange);
        }

        public ConfigEntry<float> Threshold { get; private set; } = null;
        public ConfigEntry<float> AdditionalAntiHPGain { get; private set; } = null;
        public ConfigEntry<float> HeatResSpeedup { get; private set; } = null;
        public ConfigEntry<float> RankDescensionTimerChange { get; private set; } = null;
    }
}
