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
            DestructiveOptions = new StyleRankOptions(StyleRanks.Destructive, Config, 
                heatResDrain: -1.0f,
                heatResRecovery: 100.0f,
                heatResExplosiveSizeBase: -1.0f,
                heatResExplosiveSizeNormMin: -1.0f,
                heatResExplosiveSizeNormMax: -1.0f,
                heatResExplosiveDmgScalar: -1.0f,
                heatResExplosiveDmgPlayer: false,
                explosiveAttacksHeatResThreshold: 0.0f,
                revolverTwirlCoolingScalar: 0.3f
            );

            ChaoticOptions = new StyleRankOptions(StyleRanks.Chaotic, Config, 
                heatResDrain: -1.0f,
                heatResRecovery: 100.0f,
                heatResExplosiveSizeBase: -1.0f,
                heatResExplosiveSizeNormMin: -1.0f,
                heatResExplosiveSizeNormMax: -1.0f,
                heatResExplosiveDmgScalar: -1.0f,
                heatResExplosiveDmgPlayer: false,
                explosiveAttacksHeatResThreshold: 0.0f,
                revolverTwirlCoolingScalar: 0.3f
            );

            BrutalOptions = new StyleRankOptions(StyleRanks.Brutal, Config, 
                heatResDrain: -1.0f,
                heatResRecovery: 100.0f,
                heatResExplosiveSizeBase: -1.0f,
                heatResExplosiveSizeNormMin: -1.0f,
                heatResExplosiveSizeNormMax: -1.0f,
                heatResExplosiveDmgScalar: -1.0f,
                heatResExplosiveDmgPlayer: false,
                explosiveAttacksHeatResThreshold: 10.0f,
                revolverTwirlCoolingScalar: 0.3f
            );

            AnarchicOptions = new StyleRankOptions(StyleRanks.Anarchic, Config, 
                heatResDrain: 30.0f,
                heatResRecovery: 1.7f,
                heatResExplosiveSizeBase: -1.0f,
                heatResExplosiveSizeNormMin: -1.0f,
                heatResExplosiveSizeNormMax: -1.0f,
                heatResExplosiveDmgScalar: -1.0f,
                heatResExplosiveDmgPlayer: false,
                explosiveAttacksHeatResThreshold: 10.0f,
                revolverTwirlCoolingScalar: 0.3f
            );

            SupremeOptions = new StyleRankOptions(StyleRanks.Supreme, Config, 
                heatResDrain: 60.0f,
                heatResRecovery: 2.0f,
                heatResExplosiveSizeBase: -1.0f,
                heatResExplosiveSizeNormMin: -1.0f,
                heatResExplosiveSizeNormMax: -1.0f,
                heatResExplosiveDmgScalar: -1.0f,
                heatResExplosiveDmgPlayer: false,
                explosiveAttacksHeatResThreshold: 15.0f,
                revolverTwirlCoolingScalar: 0.3f
            );

            SSadisticOptions = new StyleRankOptions(StyleRanks.SSadistic, Config, 
                heatResDrain: 70.0f,
                heatResRecovery: 2.0f,
                heatResExplosiveSizeBase: 12.0f,
                heatResExplosiveSizeNormMin: 2.5f,
                heatResExplosiveSizeNormMax: -7.0f,
                heatResExplosiveDmgScalar: 0.35f,
                heatResExplosiveDmgPlayer: true,
                explosiveAttacksHeatResThreshold: 15.0f,
                revolverTwirlCoolingScalar: 0.3f
            );

            SSSensoredStormOptions = new StyleRankOptions(StyleRanks.SSSensoredStorm, Config, 
                heatResDrain: 75.0f,
                heatResRecovery: 2.1f,
                heatResExplosiveSizeBase: 14.0f,
                heatResExplosiveSizeNormMin: 0.15f,
                heatResExplosiveSizeNormMax: 8.5f,
                heatResExplosiveDmgScalar: 0.5f,
                heatResExplosiveDmgPlayer: true,
                explosiveAttacksHeatResThreshold: 17.5f,
                revolverTwirlCoolingScalar: 0.25f
            );

            ULTRAKILLOptions = new StyleRankOptions(StyleRanks.ULTRAKILL, Config, 
                heatResDrain: 100.0f,
                heatResRecovery: 2.4f,
                heatResExplosiveSizeBase: 20.0f,
                heatResExplosiveSizeNormMin: 0.15f,
                heatResExplosiveSizeNormMax: 6.5f,
                heatResExplosiveDmgScalar: 0.8f,
                heatResExplosiveDmgPlayer: true,
                explosiveAttacksHeatResThreshold: 20.0f,
                revolverTwirlCoolingScalar: 0.3f
            );
        }
        
        internal static ConfigFile Config = null;
    }

    public class StyleRankOptions
    {
        public StyleRankOptions(StyleRanks styleRank, ConfigFile config, float heatResDrain, float heatResRecovery, float heatResExplosiveSizeBase, float heatResExplosiveSizeNormMin, float heatResExplosiveSizeNormMax, float heatResExplosiveDmgScalar, bool heatResExplosiveDmgPlayer, float explosiveAttacksHeatResThreshold, float revolverTwirlCoolingScalar)
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
        }

        public ConfigEntry<float> HeatResDrain { get; private set; } = null;
        public ConfigEntry<float> HeatResRecovery { get; private set; } = null;
        public ConfigEntry<float> HeatResExplosiveSizeBase { get; private set; } = null;
        public ConfigEntry<float> HeatResExplosiveSizeNormMin { get; private set; } = null;
        public ConfigEntry<float> HeatResExplosiveSizeNormMax { get; private set; } = null;
        public ConfigEntry<float> HeatResExplosiveDmgScalar { get; private set; } = null;
        public ConfigEntry<bool> HeatResExplosiveDmgPlayer { get; private set; } = null;

        public ConfigEntry<float> ExplosiveAttacksHeatResThreshold { get; private set; } = null;
        public ConfigEntry<float> RevolverCoolingScalar { get; private set; } = null;
    }

    public class HeatResistanceStageOptions
    {
        public ConfigEntry<float> Threshold { get; private set; } = null;
        public ConfigEntry<float> AdditionalAntiHPGain { get; private set; } = null;
        public ConfigEntry<float> RankDescensionTimerChange { get; private set; } = null;
    }
}
