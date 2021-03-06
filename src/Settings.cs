using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using ModSettings;

namespace RelativeConsumptionTime
{
	internal class RelativeConsumptionTime_Settings : JsonModSettings
	{
		[Section("Relative Consumption Time Multiplier")]

		[Name("Drinking Duration Multiplier")]
		[Description("The relative consumption duration of water is multiplied by this value.\n(Default = 1, Recommended = 2)")]
		[Slider(0.2f, 5f, 48)]
		public float drinkTimeMultiplier = 1f;

		[Name("Food Duration Multiplier")]
		[Description("The relative consumption duration of food items is multiplied by this value.\n(Default = 1, Recommended = 2)")]
		[Slider(0.2f, 5f, 48)]
		public float foodTimeMultiplier = 1f;

		[Section("Absolute Minimum")]

		[Name("Drinking Minimum Duration")]
		[Description("The absolute minimum drinking duration in seconds.\n(Default = 1s)")]
		[Slider(0.2f, 5f, 48)]
		public float drinkMinDuration = 1f;

		[Name("Food Minimum Duration")]
		[Description("The absolute minimum food item duration in seconds.\n(Default = 1s)")]
		[Slider(0.2f, 5f, 48)]
		public float foodMinDuration = 1f;
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
