using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainCourseObject", menuName = "Main course", order = 1)]
public class MainCourseObject : ScriptableObject
{
    [SerializeField] FoodItems.MainCourse mainCourseType;
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject prefab;

    public FoodItems.MainCourse GetMainCourseType()
    {
        return mainCourseType;
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
