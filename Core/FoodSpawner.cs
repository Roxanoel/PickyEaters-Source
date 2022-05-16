using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] ItemSelector selectorMenu;

    [Header("Spawning locations")]
    [SerializeField] GameObject[] DrinkSpawnLocations;
    [SerializeField] GameObject[] MainCourseSpawnLocations;
    [SerializeField] GameObject[] SideSpawnLocations;

    [Header("Parameters")]
    [SerializeField] float secondsBeforeDespawn;

    [Header("Mappings")]
    [SerializeField] DrinkPrefabMapping[] drinkPrefabMappings;
    [SerializeField] MainCoursePrefabMapping[] mainCoursePrefabMappings;
    [SerializeField] SidePrefabMapping[] sidePrefabMappings;

    [System.Serializable]
    struct DrinkPrefabMapping
    {
        public FoodItems.Drink drink;
        public GameObject prefab;
    }
    [System.Serializable]
    struct MainCoursePrefabMapping
    {
        public FoodItems.MainCourse mainCourse;
        public GameObject prefab;
    }
    [System.Serializable]
    struct SidePrefabMapping
    {
        public FoodItems.Side side;
        public GameObject prefab;
    }

    private void OnEnable()
    {
        selectorMenu.onSubmitChoice += SpawnFood;
    }

    private void OnDisable()
    {
        selectorMenu.onSubmitChoice -= SpawnFood;
    }

    private void SpawnFood(DrinkObject drink, MainCourseObject main, SideObject side)
    {
        GameObject drinkPrefab = drink.GetPrefab();
        GameObject mainCoursePrefab = main.GetPrefab();
        GameObject sidePrefab = side.GetPrefab();

        foreach (GameObject location in DrinkSpawnLocations)
        {
            StartCoroutine(SpawnFoodItemAndDestroy(drinkPrefab, location.transform.position));
        }
        foreach (GameObject location in MainCourseSpawnLocations)
        {
            StartCoroutine(SpawnFoodItemAndDestroy(mainCoursePrefab, location.transform.position));
        }
        foreach (GameObject location in SideSpawnLocations)
        {
            StartCoroutine(SpawnFoodItemAndDestroy(sidePrefab, location.transform.position));
        }

        StartCoroutine(DisableControlForSeconds(secondsBeforeDespawn));
            
    }

    private IEnumerator SpawnFoodItemAndDestroy(GameObject prefab, Vector3 location)
    {
        GameObject spawnedItem = Instantiate(prefab, location, Quaternion.identity);
        yield return new WaitForSeconds(secondsBeforeDespawn);
        Destroy(spawnedItem);
    }

    private IEnumerator DisableControlForSeconds(float duration)
    {
        selectorMenu.EnableBlockerCanvas(true);
        yield return new WaitForSeconds(duration);
        selectorMenu.EnableBlockerCanvas(false);
    }

}
