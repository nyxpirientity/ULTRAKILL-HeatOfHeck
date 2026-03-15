using UnityEngine;
using BepInEx;
using System;
using System.IO;
using HarmonyLib;

namespace Nyxpiri.ULTRAKILL.HeatOfHeck
{
    public static class Cheats
    {
        public const string HeatOfHeck = "nyxpiri.heat-of-heck";
    }
    
    [BepInPlugin("com.nyxpiri.bepinex.plugins.ultrakill.heat-of-heck", "Heat of Heck", "0.0.0.1")]
    [BepInProcess("ULTRAKILL.exe")]
    public class HeatOfHeckPlugin : BaseUnityPlugin
    {
        protected void Awake()
        {
            Log.Initialize(Logger);
            HeatOfHeck.Initialize();
            NyxLib.Cheats.ReadyForCheatRegistration += RegisterCheats;
            Options.Config = Config;
            Options.Initialize();
            Harmony.CreateAndPatchAll(GetType().Assembly);
            if (!File.Exists(Config.ConfigFilePath))
            {
                Config.Save();
            }
        }

        private void RegisterCheats(CheatsManager cheatsManager)
        {
            cheatsManager.RegisterCheat(new ToggleCheat(
                "Heat of Heck", 
                Cheats.HeatOfHeck,
                onDisable: (cheat) =>
                {
                },
                onEnable: (cheat, manager) =>
                {
                }
            ), "HELL'S IMPACT");
        }

        protected void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                Config.Reload();
            }
        }
        
        protected void Start()
        {
        }

        protected void Update()
        {

        }

        protected void LateUpdate()
        {

        }
    }
}
