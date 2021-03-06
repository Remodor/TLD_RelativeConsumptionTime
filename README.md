# Relative Consumption Time for *The Long Dark*

Every consume action in *The Long Dark* has a fixed duration, no matter how much is actually consumed. If you eat 1/10 from a steak or the hole steak, it all takes the same duration. This mod is a small Quality of Life mod which changes the absolute duration into a **configurable relative** duration. If you now eat half of a food item because you just can't pack any further calories, it will also only take half the duration!

## Settings

There are four new settings in the *Mod Options*:

- Relative Consumption Time Multiplier:
  1. Drinking Duration Multiplier
  2. Food Duration Multiplier
- Absolute Minimum (in seconds):
  1. Drinking Minimum Duration
  2. Food Minimum Duration

For both values a duration of 1 is oriented to the vanilla game. I.e. drinking half a liter will result in the original duration (Maximum amount to drink is 0.67). Drinking a quarter will be half the duration, etc.
Eating half of a food item will result in half of the default duration of that food item.

### Recommendations

I am currently using:

- Drinking Duration Multiplier = 2
- Food Duration Multiplier = 2
- Drinking Minimum Duration = 1s
- Food Minimum Duration = 1s

However, the values are so easily changed and applied right away you can try and modify to your likings.

## Installation

1. If you haven't done so already, install [MelonLoader](https://github.com/LavaGang/MelonLoader/releases) v0.2.7.4+
2. If you haven't done so already, install [ModSettings](https://github.com/zeobviouslyfakeacc/ModSettings/releases) v1.7+
3. Download the latest version of `RelativeConsumptionTime.dll` from the [releases](https://github.com/Remodor/TLD_RelativeConsumptionTime/releases) page
4. Move `RelativeConsumptionTime.dll` into the *Mods* folder of your *The Long Dark* installation directory

### Uninstallation

The mod can always be uninstalled. Just delete the `RelativeConsumptionTime.dll` from the *Mods* folder.
