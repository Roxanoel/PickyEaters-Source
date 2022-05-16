using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SideObject", menuName = "Side", order = 1)]
public class SideObject : ScriptableObject
{
    [SerializeField] FoodItems.Side sideType;
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject prefab;

    public FoodItems.Side GetSideType()
    {
        return sideType;
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
