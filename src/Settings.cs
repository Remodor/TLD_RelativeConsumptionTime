using ModSettings;

namespace RelativeConsumptionTime
{
    internal class RelativeConsumptionTime_Settings : JsonModSettings
    {
        [Section("Relative Consumption Time Multiplier")]

        [Name("Drinking Duration Multiplier")]
        [Description("The relative consumption duration of water is multiplied by this value.\n(Default = 1, Recommended = 1.5)")]
        [Slider(0.2f, 5f, 49)]
        public float drinkTimeMultiplier = 1f;

        [Name("Food Duration Multiplier")]
        [Description("The relative consumption duration of food items is multiplied by this value.\n(Default = 1, Recommended = 1.5)")]
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
    }

    internal static class RCT_Settings
    {
        internal static RelativeConsumptionTime_Settings settings = new RelativeConsumptionTime_Settings();

        public static void OnLoad()
        {
            settings.AddToModSettings("Relative Consumption Time");
        }
    }
}
