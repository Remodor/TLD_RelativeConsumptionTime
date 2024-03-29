# Relative Consumption Time for *The Long Dark*

This is a mod for **The Long Dark** by Hinterland Studio, Inc.

## Description

Every consume action in *The Long Dark* has a fixed duration, no matter how much is actually consumed. If you eat 1/10 from a steak or the hole steak, it all takes the same duration. This mod is a small Quality of Life mod which changes the absolute duration into a **configurable relative** duration. If you now eat half of a food item because you just can't pack any further calories, it will also only take half the duration!
It now also includes something similar with waiting times. No more hours spent outside in seconds without being able to react. There is now a **configurable relative** duration for many actions, like: Surveying, carcass harvesting, breaking down, crafting, fishing, inventory actions (clean, harvest, etc.), snow shelter, passing time (including cooking) and rope placing.

## Settings

There are several new settings in the *Mod Options*:

- Relative Consumption Time Multiplier:

  *Every consumable is now relative. This is the amount it is multiplied with. A value of 1 is orientated to the vanilla game. I.e. drinking half a liter and a value of 1 will result in the vanilla duration.*
  - Drinking Duration Multiplier
  - Food Duration Multiplier
- Absolute Minimum (in seconds):

  *This is the absolute minimum time to consume an item.*
  - Drinking Minimum Duration
  - Food Minimum Duration
- Minimum Threshold to Apply Multiplier

  _For items which have a low calories count, e.g. the *cat tail stalk* or a *tea*, the multiplier unnecessary prolongs the consume duration. With this option you can specify a minimum calories count for the multiplier to apply._
  - Minimum Calories
- Relative Waiting Time

  *Lets you enable relative waiting times*
  - One Hour Waiting Time
  
  *Specify the waiting time for one ingame hour in seconds. E.g. if set to 1 second, you will wait 1 second for every ingame hour passed.*
  - Minimum Waiting Time
  
  *Specify the absolute minimum waiting time in seconds*
  - Maximum Waiting Time
  
  *Specify the absolute maximum waiting time in seconds*


### Recommendations

I am currently using:

- Drinking Duration Multiplier = 1.2
- Food Duration Multiplier = 1.2
- Drinking Minimum Duration = 1s
- Food Minimum Duration = 1s
- Minimum Calories = 160 (150 = *cat tail stalk*)
- One Hour Waiting Time = 9
- Minimum Waiting Time = 1
- Maximum Waiting Time = 14

However, the values are so easily changed and applied right away you can try and modify to your likings.

## Installation

1. If you haven't done so already, install [MelonLoader](https://github.com/LavaGang/MelonLoader/releases) v0.2.7.4+
2. If you haven't done so already, install [ModSettings](https://github.com/zeobviouslyfakeacc/ModSettings/releases) v1.7+
3. Download the latest version of `RelativeConsumptionTime.dll` from the [releases](https://github.com/Remodor/TLD_RelativeConsumptionTime/releases) page
4. Move `RelativeConsumptionTime.dll` into the *Mods* folder of your *The Long Dark* installation directory

### Uninstallation

The mod can always be uninstalled. Just delete the `RelativeConsumptionTime.dll` from the *Mods* folder.

## Modding Discord Server

If you have any questions/ suggestions or you just wanna say *"Thank you!"* visit the **[TLD Modding Discord Server](https://discord.gg/nb2jQez)**!
