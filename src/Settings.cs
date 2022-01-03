using ModSettings;

namespace RelativeConsumptionTime
{
    internal class RelativeConsumptionTime_Settings : JsonModSettings
    {
        [Section("Relative Consumption Time Multiplier")]

        [Name("Drinking Duration Multiplier")]
        [Description("The relative consumption duration of water is multiplied by this value.\n(Default = 1, Recommended = 1.2)")]
        [Slider(0.2f, 5f, 49)]
        public float drinkTimeMultiplier = 1f;

        [Name("Food Duration Multiplier")]
        [Description("The relative consumption duration of food items is multiplied by this value.\n(Default = 1, Recommended = 1.2)")]
        [Slider(0.2f, 5f, 49)]
        public float foodTimeMultiplier = 1f;

        [Section("Absolute Minimum")]

        [Name("Drinking Minimum Duration")]
        [Description("The absolute minimum drinking duration in seconds.\n(Default = 1s)")]
        [Slider(0.2f, 5f, 49)]
        public float drinkMinDuration = 1f;

        [Name("Food Minimum Duration")]
        [Description("The absolute minimum food item duration in seconds.\n(Default = 1s)")]
        [Slider(0.2f, 5f, 49)]
        public float foodMinDuration = 1f;

        [Section("Minimum Threshold to Apply Multiplier")]
        [Name("Minimum Calories")]
        [Description("The minimum amount of total calories for the multiplier to apply.\n(Recommended > 150 [Cat tail stalk])")]
        [Slider(0f, 1000f, 101)]
        public float foodMinTreshold = 0f;

        //---Waiting Time
        [Section("Relative Waiting Time")]
        [Name("Enable")]
        [Description("Real waiting times are rather generic in comparison to the spent ingame time. E.g. You can pass a few hours in a few seconds sometimes. Especially harmful outside.\nThis changes the waiting time to be relative to the spent game time. Following actions are covered:\nSurveying, carcass harvesting, breaking down, crafting, fishing, inventory actions (clean, harvest, etc.), snow shelter, passing time (including cooking) and rope placing.\n(Vanilla = false)")]
        public bool relative_waiting_time = false;

        [Name("One Hour Waiting Time")]
        [Description("Specify the waiting time for one ingame hour in seconds. E.g. if set to 1 second, you will wait 1 second for every ingame hour passed.\n(Recommended = 6-9s)")]
        [Slider(0.1f, 12f, 120, NumberFormat = "{0:F1}s")]
        public float waiting_time_for_one_hour_in_seconds = 9f;

        [Name("Minimum Waiting Time")]
        [Description("Specify the absolute minimum waiting time in seconds.\n(Recommended = 1s)")]
        [Slider(0.5f, 10f, 96, NumberFormat = "{0:F1}s")]
        public float minimum_waiting_time = 1f;

        [Name("Maximum Waiting Time")]
        [Description("Specify the absolute maximum waiting time in seconds\n(Recommended = One Hour Waiting Time * 1.5)")]
        [Slider(1f, 60f, 119, NumberFormat = "{0:F1}s")]
        public float maximum_waiting_time = 14f;
    }

    internal static class RCT_Settings
    {
        private static RelativeConsumptionTime_Settings settings = new RelativeConsumptionTime_Settings();

        public static void OnLoad()
        {
            settings.AddToModSettings("Relative Consumption Time");
        }
        public static RelativeConsumptionTime_Settings Get()
        {
            return settings;
        }
    }
}
