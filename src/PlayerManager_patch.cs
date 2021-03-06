using Harmony;
using UnityEngine;


namespace RelativeConsumptionTime
{
    [HarmonyPatch(typeof(PlayerManager), "UseFoodInventoryItem")]
    internal class PlayerManager_UseFoodInventoryItem_Patch
    {
        internal static float restoreTimeToEat; //Used to restore previous value after the duration is changed.
        internal static void Prefix(PlayerManager __instance, GearItem gi)
        {
            if (gi.m_FoodItem)
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

                gi.m_FoodItem.m_TimeToEatSeconds *= relativeConsumedCalories * RCT_Settings.settings.foodSliderValue;
            }
        }
        internal static void Postfix(PlayerManager __instance, GearItem gi)
        {
            if (gi.m_FoodItem)
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
            ws.m_TimeToDrinkSeconds *= volumeToDrink * 2f * RCT_Settings.settings.drinkSliderValue;
        }
        internal static void Postfix(PlayerManager __instance, WaterSupply ws)
        {
            ws.m_TimeToDrinkSeconds = restoreTimeToDrink;
        }
    }
}
