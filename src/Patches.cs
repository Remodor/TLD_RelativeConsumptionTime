using HarmonyLib;
using UnityEngine;


namespace RelativeConsumptionTime
{
    using Settings = RCT_Settings;
    [HarmonyPatch(typeof(PlayerManager), "UseFoodInventoryItem")]
    internal class PlayerManager_UseFoodInventoryItem_Patch
    {
        internal static float restoreTimeToEat; //Used to restore previous value after the duration is changed.
        internal static void Prefix(PlayerManager __instance, GearItem gi)
        {
            if (gi.m_FoodItem && gi.m_FoodItem.m_CaloriesTotal >= Settings.Get().foodMinTreshold)
            {
                restoreTimeToEat = gi.m_FoodItem.m_TimeToEatSeconds;
                float totalCalories = gi.m_FoodItem.m_CaloriesTotal;
                float maxPossibleCaloriesByFood = GameManager.GetHungerComponent().m_MaxReserveCalories - GameManager.GetHungerComponent().GetCalorieReserves();
                float maxPossibleCaloriesByThirst = 0f;
                if (gi.m_FoodItem.m_ReduceThirst > 0f && gi.m_FoodItem.m_IsDrink)
                {
                    float relativeMaxThirst = GameManager.GetThirstComponent().m_CurrentThirst / gi.m_FoodItem.m_ReduceThirst;
                    relativeMaxThirst = Mathf.Clamp(relativeMaxThirst, 0f, 1f);
                    maxPossibleCaloriesByThirst = relativeMaxThirst * totalCalories;
                }
                float maxPossibleCalories = Mathf.Max(maxPossibleCaloriesByFood, maxPossibleCaloriesByThirst);
                float consumedCalories = Mathf.Min(gi.m_FoodItem.m_CaloriesRemaining, maxPossibleCalories);
                if (gi.m_StackableItem)
                {
                    consumedCalories = gi.m_FoodItem.m_CaloriesRemaining;
                }
                float relativeConsumedCalories = Mathf.Clamp(consumedCalories / totalCalories, 0.1f, 1f);
                // The above code is extracted from the UseFoodInventoryItem method to circumvent unnecessary calls.

                gi.m_FoodItem.m_TimeToEatSeconds *= relativeConsumedCalories * Settings.Get().foodTimeMultiplier;
                gi.m_FoodItem.m_TimeToEatSeconds = Mathf.Max(gi.m_FoodItem.m_TimeToEatSeconds, Settings.Get().foodMinDuration);
            }
        }
        internal static void Postfix(PlayerManager __instance, GearItem gi)
        {
            if (gi.m_FoodItem && gi.m_FoodItem.m_CaloriesTotal >= Settings.Get().foodMinTreshold)
            {
                gi.m_FoodItem.m_TimeToEatSeconds = restoreTimeToEat;
            }
        }

    }
    [HarmonyPatch(typeof(PlayerManager), "DrinkFromWaterSupply")]
    internal class PlayerManager_DrinkFromWaterSupply_Patch
    {
        internal static float restoreTimeToDrink; //Used to restore previous value after the duration is changed.
        internal static void Prefix(PlayerManager __instance, WaterSupply ws, float volumeAvailable)
        {
            if (volumeAvailable <= -1f)
            {
                volumeAvailable = ws.m_VolumeInLiters;
            }
            float maxPossibleLiterToDrink = GameManager.GetThirstComponent().m_CurrentThirst / GameManager.GetThirstComponent().m_ThirstQuenchedPerLiter;
            if (GameManager.GetDysenteryComponent().HasDysentery())
            {
                maxPossibleLiterToDrink = Mathf.Max(maxPossibleLiterToDrink, GameManager.GetDysenteryComponent().GetWaterRemainingForCureLiters());
            }
            float volumeToDrink = Mathf.Min(maxPossibleLiterToDrink, volumeAvailable);
            if ((double)maxPossibleLiterToDrink < 0.01)
            {
                volumeToDrink = 0;
            }
            // The above code is extracted from the CalculateWaterVolumeToDrink method to circumvent unnecessary calls.
            restoreTimeToDrink = ws.m_TimeToDrinkSeconds;
            ws.m_TimeToDrinkSeconds *= volumeToDrink * 2f * Settings.Get().drinkTimeMultiplier;
            ws.m_TimeToDrinkSeconds = Mathf.Max(ws.m_TimeToDrinkSeconds, Settings.Get().drinkMinDuration);
        }
        internal static void Postfix(PlayerManager __instance, WaterSupply ws)
        {
            ws.m_TimeToDrinkSeconds = restoreTimeToDrink;
        }
    }
    //* Relative waiting time.
    // Surveying.
    [HarmonyPatch(typeof(CharcoalItem), "HasSurveyVisibility")]
    internal class CharcoalItem_HasSurveyVisibility
    {
        internal static void Prefix()
        {
            GearItem gearItem = GameManager.GetPlayerManagerComponent().m_ItemInHands;
            if (Settings.Get().relative_waiting_time && gearItem && gearItem.m_CharcoalItem)
            {
                CharcoalItem instance = gearItem.m_CharcoalItem;
                instance.m_SurveyRealSeconds = Implementation.MinutesToRealtime(instance.m_SurveyGameMinutes);
            }
        }
    }
    // Carcass Harvesting.
    [HarmonyPatch(typeof(Panel_BodyHarvest), "AccelerateTimeOfDay")]
    internal class Panel_BodyHarvest_AccelerateTimeOfDay
    {
        internal static void Prefix(Panel_BodyHarvest __instance, int minutes)
        {
            if (Settings.Get().relative_waiting_time)
            {
                __instance.m_HarvestTimeSeconds = Implementation.MinutesToRealtime(minutes);
                __instance.m_ProgressBarTimeSeconds = __instance.m_HarvestTimeSeconds;
            }
        }
    }
    // Breaking down.
    [HarmonyPatch(typeof(Panel_BreakDown), "OnBreakDown")]
    internal class Panel_BreakDown_OnBreakDown
    {
        internal static void Prefix(Panel_BreakDown __instance)
        {
            if (Settings.Get().relative_waiting_time)
            {
                __instance.m_SecondsToBreakDown = Implementation.HoursToRealtime(__instance.m_DurationHours);
            }
        }
    }
    // Crafting
    [HarmonyPatch(typeof(Panel_Crafting), "GetFinalCraftingTimeWithAllModifiers")]
    internal class Panel_Crafting_OnBeginCrafting
    {
        internal static void Postfix(Panel_Crafting __instance, int __result)
        {
            if (Settings.Get().relative_waiting_time)
            {
                __instance.m_RealTimeCraftingDuration = Implementation.MinutesToRealtime(__result);
            }
        }
    }
    // Fishing
    [HarmonyPatch(typeof(Panel_GenericProgressBar), "Launch")]
    internal class Panel_GenericProgressBar_Launch
    {
        internal static void Prefix(ref float seconds, ref float minutes)
        {
            if (Settings.Get().relative_waiting_time && minutes != 0)
            {
                seconds = Implementation.MinutesToRealtime(minutes);
            }
        }
    }
    // Inventory actions: Clean, harvest, read, repair, sharpen
    [HarmonyPatch(typeof(Panel_Inventory_Examine), "AccelerateTimeOfDay")]
    internal class Panel_Inventory_Examine_AccelerateTimeOfDay
    {
        internal static void Prefix(Panel_Inventory_Examine __instance, int minutes)
        {
            if (Settings.Get().relative_waiting_time)
            {
                float seconds = Implementation.MinutesToRealtime(minutes);
                __instance.m_ProgressBarTimeSeconds = seconds;
                __instance.m_HarvestTimeSeconds = seconds;
                __instance.m_CleanTimeSeconds = seconds;
                __instance.m_ReadTimeSeconds = seconds;
                __instance.m_RepairTimeSeconds = seconds;
                __instance.m_SharpenTimeSeconds = seconds;
            }
        }
    }
    // Snow shelter
    [HarmonyPatch(typeof(Panel_SnowShelterBuild), "OnBuild")]
    internal class Panel_SnowShelterBuild_OnBuild
    {
        internal static void Prefix(Panel_SnowShelterBuild __instance)
        {
            if (Settings.Get().relative_waiting_time)
            {
                __instance.m_RealtimeSecondsToBuild = Implementation.HoursToRealtime(__instance.m_DurationHours);
            }
        }
    }
    [HarmonyPatch(typeof(Panel_SnowShelterInteract), "OnInteractionCommon")]
    internal class Panel_SnowShelterInteract_OnInteractionCommon
    {
        internal static void Prefix(Panel_SnowShelterInteract __instance)
        {
            if (Settings.Get().relative_waiting_time)
            {
                __instance.m_RealtimeSecondsToRepairOrDismantle = Implementation.HoursToRealtime(__instance.GetTaskDurationInHours());
            }
        }
    }
    // Pass time
    [HarmonyPatch(typeof(PassTime), "Begin")]
    internal class PassTime_Begin
    {
        internal static void Prefix(PassTime __instance, float hours)
        {
            if (Settings.Get().relative_waiting_time)
            {
                float maximumTimePerHour = Settings.Get().maximum_waiting_time / hours;
                __instance.m_PassTimeRealtimeSecondsPerHour = Mathf.Clamp(Settings.Get().waiting_time_for_one_hour_in_seconds, Settings.Get().minimum_waiting_time, maximumTimePerHour);
            }
        }
    }
    // Rope placing
    [HarmonyPatch(typeof(RopeAnchorPoint), "ActionStarted")]
    internal class RopeAnchorPoint_ActionStarted
    {
        internal static void Prefix(RopeAnchorPoint __instance)
        {
            if (Settings.Get().relative_waiting_time)
            {
                __instance.m_ProgressBarDurationSeconds = Implementation.MinutesToRealtime(__instance.m_Rope.m_DeployDurationMinutes);
            }
        }
    }
}
