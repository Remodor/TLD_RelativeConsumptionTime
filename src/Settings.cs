using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using ModSettings;

namespace TLD_RelativeConsumptionTime
{
	internal class RelativeConsumptionTime_Settings : ModSettingsBase
	{
		[Section("Relative Consumption Time Multiplier")]
		[Name("Drinking Time Multiplier")]
		[Slider(0.2f, 4f, 38)]
		public float drinkSliderValue = 1f;

		[Name("Food Item Time Multiplier")]
		[Slider(0.2f, 4f, 38)]
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
