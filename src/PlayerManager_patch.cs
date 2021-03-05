using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using UnityEngine;


namespace TLD_RelativeConsumptionTime
{
    [HarmonyPatch(typeof(PlayerManager), "UseFoodInventoryItem")]
    internal class PlayerManager_UseFoodInventoryItem_Patch
    {
        internal static float restoreTimeToEat;
        internal static void Prefix(PlayerManager __instance, GearItem gi)
        {
            MelonLoader.MelonLogger.Log("Prefix: restoreTimeToEat: {0}", restoreTimeToEat);
            if (gi.m_FoodItem)
            {
                restoreTimeToEat = gi.m_FoodItem.m_TimeToEatSeconds;
                MelonLoader.MelonLogger.Log("Prefix: restoreTimeToEat: {0}", restoreTimeToEat);
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
                MelonLoader.MelonLogger.Log("Postfix: relativeConsumedCalories: {0}", relativeConsumedCalories);

                gi.m_FoodItem.m_TimeToEatSeconds *= relativeConsumedCalories * RCT_Settings.settings.foodSliderValue;
                MelonLoader.MelonLogger.Log("Postfix: m_TimeToEatSeconds: {0}", gi.m_FoodItem.m_TimeToEatSeconds);

            }
        }
        internal static void Postfix(PlayerManager __instance, GearItem gi)
        {
            MelonLoader.MelonLogger.Log("Postfix: restoreTimeToEat: {0}", restoreTimeToEat);
            if (gi.m_FoodItem)
            {
                gi.m_FoodItem.m_TimeToEatSeconds = restoreTimeToEat;
                MelonLoader.MelonLogger.Log("Postfix: restoreTimeToEat: {0}", restoreTimeToEat);
            }
        }

    }


    [HarmonyPatch(typeof(PlayerManager), "DrinkFromWaterSupply")]
    internal class PlayerManager_DrinkFromWaterSupply_Patch
    {
        internal static float restoreTimeToDrink;
        internal static void Prefix(PlayerManager __instance, WaterSupply ws, float volumeAvailable)
        {
            if (volumeAvailable <= -1f)
            {
                volumeAvailable = ws.m_VolumeInLiters;
            }
            MelonLoader.MelonLogger.Log("Prefix: volumeAvailable: {0}", volumeAvailable);
            MelonLoader.MelonLogger.Log("Prefix: restoreTimeToDrink: {0}", restoreTimeToDrink);
            float volumeToDrink = __instance.CalculateWaterVolumeToDrink(volumeAvailable);
            MelonLoader.MelonLogger.Log("Prefix: volumeToDrink: {0}", volumeToDrink);

            float maxPossibleLiterToDrink = GameManager.GetThirstComponent().m_CurrentThirst / GameManager.GetThirstComponent().m_ThirstQuenchedPerLiter;
            if (GameManager.GetDysenteryComponent().HasDysentery())
            {
                maxPossibleLiterToDrink = Mathf.Max(maxPossibleLiterToDrink, GameManager.GetDysenteryComponent().GetWaterRemainingForCureLiters());
            }
            volumeToDrink = Mathf.Min(maxPossibleLiterToDrink, volumeAvailable);
            if ((double)maxPossibleLiterToDrink < 0.01)
            {
                volumeToDrink = 0;
            }
            MelonLoader.MelonLogger.Log("Prefix: volumeToDrink: {0}", volumeToDrink);


            restoreTimeToDrink = ws.m_TimeToDrinkSeconds;
            MelonLoader.MelonLogger.Log("Prefix: m_TimeToDrinkSeconds: {0}", restoreTimeToDrink);
            ws.m_TimeToDrinkSeconds *= volumeToDrink * 2f * RCT_Settings.settings.drinkSliderValue;
            MelonLoader.MelonLogger.Log("Prefix: ws.m_TimeToDrinkSeconds: {0}", ws.m_TimeToDrinkSeconds);
        }
        internal static void Postfix(PlayerManager __instance, WaterSupply ws, float volumeAvailable)
        {
            MelonLoader.MelonLogger.Log("Postfix: restoreTimeToDrink: {0}", restoreTimeToDrink);
            ws.m_TimeToDrinkSeconds = restoreTimeToDrink;
            MelonLoader.MelonLogger.Log("Prefix: ws.m_TimeToDrinkSeconds: {0}", ws.m_TimeToDrinkSeconds);
        }
    }
}
