using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodDatabase", menuName = "FoodDatabase")]
public class FoodDatabase : ScriptableObject
{
    public List<Food> foods;

    private Dictionary<string, Food> foodDictionary;

    private void OnEnable()
    {
        foodDictionary = new Dictionary<string, Food>();
        foreach (var food in foods)
        {
            foodDictionary[food.foodId] = food;
        }
    }

    public Food GetFoodById(string foodId)
    {
        foodDictionary.TryGetValue(foodId, out Food food);
        return food;
    }
}
