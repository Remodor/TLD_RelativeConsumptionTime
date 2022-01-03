using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;

namespace RelativeConsumptionTime
{
    using Settings = RCT_Settings;

    public class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Debug.Log($"[{Info.Name}] version {Info.Version} loaded!");
            RCT_Settings.OnLoad();
        }
        internal static float HoursToRealtime(float hours)
        {
            float realTimeSeconds = hours * Settings.Get().waiting_time_for_one_hour_in_seconds;
            return Mathf.Clamp(realTimeSeconds, Settings.Get().minimum_waiting_time, Settings.Get().maximum_waiting_time);
        }
        internal static float MinutesToRealtime(float minutes)
        {
            float realTimeSeconds = (minutes / 60f) * Settings.Get().waiting_time_for_one_hour_in_seconds;
            return Mathf.Clamp(realTimeSeconds, Settings.Get().minimum_waiting_time, Settings.Get().maximum_waiting_time);
        }
    }
}
