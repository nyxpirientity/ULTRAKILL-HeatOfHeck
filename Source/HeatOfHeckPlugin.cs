using UnityEngine;
using BepInEx;

namespace Nyxpiri.ULTRAKILL.HeatOfHeck
{
    [BepInPlugin("com.nyxpiri.bepinex.plugins.ultrakill.heat-of-heck", "Heat of Heck", "0.0.0.1")]
    [BepInProcess("ULTRAKILL.exe")]
    public class HeatOfHeckPlugin : BaseUnityPlugin
    {
        protected void Awake()
        {
            Log.Initialize(Logger);
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
