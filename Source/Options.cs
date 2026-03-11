using System.Collections.Generic;
using BepInEx.Configuration;

namespace Nyxpiri.ULTRAKILL.ModName
{
    public static class Options
    {
        public static ConfigEntry<float> DestructiveHeatResDrainEntry { get; private set; } = null;
        public static ConfigEntry<float> DestructiveHeatResRecoveryEntry { get; private set; } = null;
        public static ConfigEntry<float> DestructiveHeatResExplosiveSizeBase { get; private set; } = null;
        public static ConfigEntry<float> DestructiveHeatResExplosiveSizeNormMin { get; private set; } = null;
        public static ConfigEntry<float> DestructiveHeatResExplosiveSizeNormMax { get; private set; } = null;
        public static ConfigEntry<float> DestructiveHeatResExplosiveDmgScalar { get; private set; } = null;
        public static ConfigEntry<bool> DestructiveHeatResExplosiveDmgPlayer { get; private set; } = null;

        public static ConfigEntry<float> ChaoticHeatResDrainEntry { get; private set; } = null;
        public static ConfigEntry<float> ChaoticHeatResRecoveryEntry { get; private set; } = null;
        public static ConfigEntry<float> ChaoticHeatResExplosiveSizeBase { get; private set; } = null;
        public static ConfigEntry<float> ChaoticHeatResExplosiveSizeNormMin { get; private set; } = null;
        public static ConfigEntry<float> ChaoticHeatResExplosiveSizeNormMax { get; private set; } = null;
        public static ConfigEntry<float> ChaoticHeatResExplosiveDmgScalar { get; private set; } = null;
        public static ConfigEntry<bool> ChaoticHeatResExplosiveDmgPlayer { get; private set; } = null;

        public static ConfigEntry<float> BrutalHeatResRecoveryEntry { get; private set; } = null;
        public static ConfigEntry<float> BrutalHeatResDrainEntry { get; private set; } = null;
        public static ConfigEntry<float> BrutalHeatResExplosiveSizeBase { get; private set; } = null;
        public static ConfigEntry<float> BrutalHeatResExplosiveSizeNormMin { get; private set; } = null;
        public static ConfigEntry<float> BrutalHeatResExplosiveSizeNormMax { get; private set; } = null;
        public static ConfigEntry<float> BrutalHeatResExplosiveDmgScalar { get; private set; } = null;
        public static ConfigEntry<bool> BrutalHeatResExplosiveDmgPlayer { get; private set; } = null;

        public static ConfigEntry<float> AnarchicHeatResDrainEntry { get; private set; } = null;
        public static ConfigEntry<float> AnarchicHeatResRecoveryEntry { get; private set; } = null;
        public static ConfigEntry<float> AnarchicHeatResExplosiveSizeBase { get; private set; } = null;
        public static ConfigEntry<float> AnarchicHeatResExplosiveSizeNormMin { get; private set; } = null;
        public static ConfigEntry<float> AnarchicHeatResExplosiveSizeNormMax { get; private set; } = null;
        public static ConfigEntry<float> AnarchicHeatResExplosiveDmgScalar { get; private set; } = null;
        public static ConfigEntry<bool> AnarchicHeatResExplosiveDmgPlayer { get; private set; } = null;

        public static ConfigEntry<float> SupremeHeatResDrainEntry { get; private set; } = null;
        public static ConfigEntry<float> SupremeHeatResRecoveryEntry { get; private set; } = null;
        public static ConfigEntry<float> SupremeHeatResExplosiveSizeBase { get; private set; } = null;
        public static ConfigEntry<float> SupremeHeatResExplosiveSizeNormMin { get; private set; } = null;
        public static ConfigEntry<float> SupremeHeatResExplosiveSizeNormMax { get; private set; } = null;
        public static ConfigEntry<float> SupremeHeatResExplosiveDmgScalar { get; private set; } = null;
        public static ConfigEntry<bool> SupremeHeatResExplosiveDmgPlayer { get; private set; } = null;

        public static ConfigEntry<float> SSadisticHeatResDrainEntry { get; private set; } = null;
        public static ConfigEntry<float> SSadisticHeatResRecoveryEntry { get; private set; } = null;
        public static ConfigEntry<float> SSadisticHeatResExplosiveSizeBase { get; private set; } = null;
        public static ConfigEntry<float> SSadisticHeatResExplosiveSizeNormMin { get; private set; } = null;
        public static ConfigEntry<float> SSadisticHeatResExplosiveSizeNormMax { get; private set; } = null;
        public static ConfigEntry<float> SSadisticHeatResExplosiveDmgScalar { get; private set; } = null;
        public static ConfigEntry<bool> SSadisticHeatResExplosiveDmgPlayer { get; private set; } = null;

        public static ConfigEntry<float> SSSensoredStormHeatResDrainEntry { get; private set; } = null;
        public static ConfigEntry<float> SSSensoredStormHeatResRecoveryEntry { get; private set; } = null;
        public static ConfigEntry<float> SSSensoredStormHeatResExplosiveSizeBase { get; private set; } = null;
        public static ConfigEntry<float> SSSensoredStormHeatResExplosiveSizeNormMin { get; private set; } = null;
        public static ConfigEntry<float> SSSensoredStormHeatResExplosiveSizeNormMax { get; private set; } = null;
        public static ConfigEntry<float> SSSensoredStormHeatResExplosiveDmgScalar { get; private set; } = null;
        public static ConfigEntry<bool> SSSensoredStormHeatResExplosiveDmgPlayer { get; private set; } = null;

        public static ConfigEntry<float> ULTRAKILLHeatResDrainEntry { get; private set; } = null;
        public static ConfigEntry<float> ULTRAKILLHeatResRecoveryEntry { get; private set; } = null;
        public static ConfigEntry<float> ULTRAKILLHeatResExplosiveSizeBase { get; private set; } = null;
        public static ConfigEntry<float> ULTRAKILLHeatResExplosiveSizeNormMin { get; private set; } = null;
        public static ConfigEntry<float> ULTRAKILLHeatResExplosiveSizeNormMax { get; private set; } = null;
        public static ConfigEntry<float> ULTRAKILLHeatResExplosiveDmgScalar { get; private set; } = null;
        public static ConfigEntry<bool> ULTRAKILLHeatResExplosiveDmgPlayer { get; private set; } = null;

        public static void Initialize()
        {
            DestructiveHeatResDrainEntry = Config.Bind($"{""}Destructive", "HeatResDrain", -1.0f);
            DestructiveHeatResRecoveryEntry = Config.Bind($"{""}Destructive", "HeatResRecovery", 100.0f);
            DestructiveHeatResExplosiveSizeBase = Config.Bind($"{""}Destructive", "HeatResExplosiveSizeBase", -1.0f);
            DestructiveHeatResExplosiveSizeNormMin = Config.Bind($"{""}Destructive", "HeatResExplosiveSizeNormMin", -1.0f);
            DestructiveHeatResExplosiveSizeNormMax = Config.Bind($"{""}Destructive", "HeatResExplosiveSizeNormMax", -1.0f);
            DestructiveHeatResExplosiveDmgScalar = Config.Bind($"{""}Destructive", "HeatResExplosiveDmgScalar", -1.0f);
            DestructiveHeatResExplosiveDmgPlayer = Config.Bind($"{""}Destructive", "HeatResExplosiveDmgPlayer", false);

            ChaoticHeatResDrainEntry = Config.Bind($"{""}Chaotic", "HeatResDrain", -1.0f);
            ChaoticHeatResRecoveryEntry = Config.Bind($"{""}Chaotic", "HeatResRecovery", 100.0f);
            ChaoticHeatResExplosiveSizeBase = Config.Bind($"{""}Chaotic", "HeatResExplosiveSizeBase", -1.0f);
            ChaoticHeatResExplosiveSizeNormMin = Config.Bind($"{""}Chaotic", "HeatResExplosiveSizeNormMin", -1.0f);
            ChaoticHeatResExplosiveSizeNormMax = Config.Bind($"{""}Chaotic", "HeatResExplosiveSizeNormMax", -1.0f);
            ChaoticHeatResExplosiveDmgScalar = Config.Bind($"{""}Chaotic", "HeatResExplosiveDmgScalar", -1.0f);
            ChaoticHeatResExplosiveDmgPlayer = Config.Bind($"{""}Chaotic", "HeatResExplosiveDmgPlayer", false);

            BrutalHeatResDrainEntry = Config.Bind($"{""}Brutal", "HeatResDrain", -1.0f);
            BrutalHeatResRecoveryEntry = Config.Bind($"{""}Brutal", "HeatResRecovery", 100.0f);
            BrutalHeatResExplosiveSizeBase = Config.Bind($"{""}Brutal", "HeatResExplosiveSizeBase", -1.0f);
            BrutalHeatResExplosiveSizeNormMin = Config.Bind($"{""}Brutal", "HeatResExplosiveSizeNormMin", -1.0f);
            BrutalHeatResExplosiveSizeNormMax = Config.Bind($"{""}Brutal", "HeatResExplosiveSizeNormMax", -1.0f);
            BrutalHeatResExplosiveDmgScalar = Config.Bind($"{""}Brutal", "HeatResExplosiveDmgScalar", -1.0f);
            BrutalHeatResExplosiveDmgPlayer = Config.Bind($"{""}Brutal", "HeatResExplosiveDmgPlayer", false);

            AnarchicHeatResDrainEntry = Config.Bind($"{""}Anarchic", "HeatResDrain", 30.0f);
            AnarchicHeatResRecoveryEntry = Config.Bind($"{""}Anarchic", "HeatResRecovery", 1.7f);
            AnarchicHeatResExplosiveSizeBase = Config.Bind($"{""}Anarchic", "HeatResExplosiveSizeBase", -1.0f);
            AnarchicHeatResExplosiveSizeNormMin = Config.Bind($"{""}Anarchic", "HeatResExplosiveSizeNormMin", -1.0f);
            AnarchicHeatResExplosiveSizeNormMax = Config.Bind($"{""}Anarchic", "HeatResExplosiveSizeNormMax", -1.0f);
            AnarchicHeatResExplosiveDmgScalar = Config.Bind($"{""}Anarchic", "HeatResExplosiveDmgScalar", -1.0f);
            AnarchicHeatResExplosiveDmgPlayer = Config.Bind($"{""}Anarchic", "HeatResExplosiveDmgPlayer", false);

            SupremeHeatResDrainEntry = Config.Bind($"{""}Supreme", "HeatResDrain", 60.0f);
            SupremeHeatResRecoveryEntry = Config.Bind($"{""}Supreme", "HeatResRecovery", 2.0f);
            SupremeHeatResExplosiveSizeBase = Config.Bind($"{""}Supreme", "HeatResExplosiveSizeBase", -1.0f);
            SupremeHeatResExplosiveSizeNormMin = Config.Bind($"{""}Supreme", "HeatResExplosiveSizeNormMin", -1.0f);
            SupremeHeatResExplosiveSizeNormMax = Config.Bind($"{""}Supreme", "HeatResExplosiveSizeNormMax", -1.0f);
            SupremeHeatResExplosiveDmgScalar = Config.Bind($"{""}Supreme", "HeatResExplosiveDmgScalar", -1.0f);
            SupremeHeatResExplosiveDmgPlayer = Config.Bind($"{""}Supreme", "HeatResExplosiveDmgPlayer", false);

            SSadisticHeatResDrainEntry = Config.Bind($"{""}SSadistic", "HeatResDrain", 70.0f);
            SSadisticHeatResRecoveryEntry = Config.Bind($"{""}SSadistic", "HeatResRecovery", 2.0f);
            SSadisticHeatResExplosiveSizeBase = Config.Bind($"{""}SSadistic", "HeatResExplosiveSizeBase", 12.0f);
            SSadisticHeatResExplosiveSizeNormMin = Config.Bind($"{""}SSadistic", "HeatResExplosiveSizeNormMin", 2.5f);
            SSadisticHeatResExplosiveSizeNormMax = Config.Bind($"{""}SSadistic", "HeatResExplosiveSizeNormMax", 7.0f);
            SSadisticHeatResExplosiveDmgScalar = Config.Bind($"{""}SSadistic", "HeatResExplosiveDmgScalar", 0.35f);
            SSadisticHeatResExplosiveDmgPlayer = Config.Bind($"{""}SSadistic", "HeatResExplosiveDmgPlayer", true);

            SSSensoredStormHeatResDrainEntry = Config.Bind($"{""}SSSensoredStorm", "HeatStormResDrain", 85.0f);
            SSSensoredStormHeatResRecoveryEntry = Config.Bind($"{""}SSSensoredStorm", "HeatStormResRecovery", 1.9f);
            SSSensoredStormHeatResExplosiveSizeBase = Config.Bind($"{""}SSSensoredStorm", "HeatResExplosiveSizeBase", 14.0f);
            SSSensoredStormHeatResExplosiveSizeNormMin = Config.Bind($"{""}SSSensoredStorm", "HeatResExplosiveSizeNormMin", 0.15f);
            SSSensoredStormHeatResExplosiveSizeNormMax = Config.Bind($"{""}SSSensoredStorm", "HeatResExplosiveSizeNormMax", 8.5f);
            SSSensoredStormHeatResExplosiveDmgScalar = Config.Bind($"{""}SSSensoredStorm", "HeatResExplosiveDmgScalar", 0.5f);
            SSSensoredStormHeatResExplosiveDmgPlayer = Config.Bind($"{""}SSSensoredStorm", "HeatResExplosiveDmgPlayer", true);

            ULTRAKILLHeatResRecoveryEntry = Config.Bind($"{""}ULTRAKILL", "HeatResRecovery", 2.7f);
            ULTRAKILLHeatResDrainEntry = Config.Bind($"{""}ULTRAKILL", "HeatResDrain", 100.0f);
            ULTRAKILLHeatResExplosiveSizeBase = Config.Bind($"{""}ULTRAKILL", "HeatResExplosiveSizeBase", 20.0f);
            ULTRAKILLHeatResExplosiveSizeNormMin = Config.Bind($"{""}ULTRAKILL", "HeatResExplosiveSizeNormMin", 0.15f);
            ULTRAKILLHeatResExplosiveSizeNormMax = Config.Bind($"{""}ULTRAKILL", "HeatResExplosiveSizeNormMax", 6.5f);
            ULTRAKILLHeatResExplosiveDmgScalar = Config.Bind($"{""}ULTRAKILL", "HeatResExplosiveDmgScalar", 0.8f);
            ULTRAKILLHeatResExplosiveDmgPlayer = Config.Bind($"{""}ULTRAKILL", "HeatResExplosiveDmgPlayer", true);
        }
        
        internal static ConfigFile Config = null;
    }
}
