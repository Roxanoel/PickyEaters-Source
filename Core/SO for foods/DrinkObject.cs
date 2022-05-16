using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrinkObject", menuName = "Drink", order = 1)]
public class DrinkObject : ScriptableObject
{
    [SerializeField] FoodItems.Drink drinkType;
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject prefab;

    public FoodItems.Drink GetDrinkType()
    {
        return drinkType;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public GameObject GetPrefab()
    {
        return prefab;
    }
}
