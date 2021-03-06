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
		[Slider(0.2f, 5f, 48)]
		public float drinkSliderValue = 1f;

		[Name("Food Duration Multiplier")]
		[Slider(0.2f, 5f, 48)]
		public float foodSliderValue = 1f;
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
