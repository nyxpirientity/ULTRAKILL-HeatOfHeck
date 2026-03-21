using Nyxpiri.ULTRAKILL.NyxLib;
using UnityEngine;

namespace Nyxpiri.ULTRAKILL.HeatOfHeck
{
    public static class Assets
    {
        public static GameObject HeatResistancePrefab { get; private set; } = null;
        public static GameObject HeatResHurtSound { get; private set; }

        internal static void Initialize()
        {
            NyxLib.Assets.AddAssetPicker<HeatResistance>((hr) =>
            {
                HeatResistancePrefab = UnityEngine.Object.Instantiate(hr.gameObject.transform.parent.gameObject, null, false);
                HeatResistancePrefab.SetActive(false);
                UnityEngine.Object.DontDestroyOnLoad(HeatResistancePrefab);

                var heatRes = HeatResistancePrefab.gameObject.GetComponentInChildren<HeatResistance>(includeInactive: true);
                FieldPublisher<HeatResistance, GameObject> hurtingSound = new FieldPublisher<HeatResistance, GameObject>(heatRes, "hurtingSound");
                HeatResHurtSound = GameObject.Instantiate(hurtingSound.Value); 
                UnityEngine.Object.DontDestroyOnLoad(HeatResHurtSound);

                return true;
            });
        }
    }
}