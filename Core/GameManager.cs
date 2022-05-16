using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ItemSelector selectorMenu;
    private Eater[] eaters;

    bool attemptFailed = false;
    public event Action onAttemptFailed;
    public event Action onAttemptSuccessful;
    public event Action onListOfAttemptsUpdated;

    [Header("Parameters - List of attempted combinations")]
    [SerializeField] int maxLengthOfList = 5;
    private List<Combination> attemptedCombinations;

    void Awake()
    {
        eaters = GenerateListOfActiveEaters();
        attemptedCombinations = new List<Combination>();
    }

    private void OnEnable()
    {
        selectorMenu.onSubmitChoice += CheckSelectionAgainstAllClients;
    }

    private void OnDisable()
    {
        selectorMenu.onSubmitChoice -= CheckSelectionAgainstAllClients;
    }

    private void Start()
    {
        StartCoroutine(SetupCompatiblePreferencesSet());
    }

    private IEnumerator SetupCompatiblePreferencesSet()
    {
        do
        {
            // Ensures UI controls are disabled while in the generation phase
            selectorMenu.EnableBlockerCanvas(true);
            // Waits until all preferences were generating before moving on with code execution
            yield return StartCoroutine(GeneratePrefsForAllEaters());
        }
        while (IsImpossible());

        // Once a possible combination has been generated, re-enable UI controls
        selectorMenu.EnableBlockerCanvas(false);
    }

    private Eater[] GenerateListOfActiveEaters()
    {
        return FindObjectsOfType<Eater>();
    }

    private IEnumerator GeneratePrefsForAllEaters()
    {
        foreach (Eater eater in eaters)
        {
            eater.SetUp();
        }
        Debug.Log($"Combination is impossible: {IsImpossible()}");
        yield return null;
    }

    private bool IsImpossible()
    {
        // Make a hashset of ints and just use the int values for each enum to compare
        HashSet<int> itemsToCompare = new HashSet<int>();

        // Loop on all eaters to get their preference type
        foreach (Eater eater in eaters)
        {
            PreferenceType preferenceType = eater.GetPreferenceType();
            switch (preferenceType)
            {
                case PreferenceType.Drink:
                    itemsToCompare.Add((int)eater.GetDrinkPreference());
                    break;
                case PreferenceType.MainCourse:
                    itemsToCompare.Add((int)eater.GetMainCoursePreference());
                    break;
                case PreferenceType.Side:
                    itemsToCompare.Add((int)eater.GetSidePreference());
                    break;
                case PreferenceType.Null:
                    Debug.LogError($"Preference Type for {eater} was null");
                    break;
            }
        }

        // Check if duplicates were added by looking at length of the hashset. 
        // will only return false if there are exactly as many preferences as there are eaters.
        return !(itemsToCompare.Count == eaters.Length);
    }

    void CheckSelectionAgainstAllClients(DrinkObject drink, MainCourseObject main, SideObject side)
    {
        // Debug for now
        //Debug.Log($"Selection: Drink = {drink}, Main = {main}, Side = {side}");

        // Reset attempt failure check
        attemptFailed = false;
        // Checks each client
        foreach (Eater eater in eaters)
        {
            eater.CheckIfSatisfied(drink.GetDrinkType(), main.GetMainCourseType(), side.GetSideType());  // Separate because I want some code to run in there for each character for future VFX
            if (!eater.GetIsSatisfied())
            {
                attemptFailed = true;
            }
        }
        // Once all clients are verified, check if failure (if at least one was not satisfied)
        if (attemptFailed && onAttemptFailed != null)
        {
            onAttemptFailed.Invoke();
        }
        else if (!attemptFailed && onAttemptSuccessful != null)
        {
            onAttemptSuccessful.Invoke();
        }

        AddAttemptToList(drink, main, side);

    }

    private void AddAttemptToList(DrinkObject drink, MainCourseObject main, SideObject side)
    {
        Combination combinationToAdd = new Combination(drink, main, side);

        if (attemptedCombinations.Count < maxLengthOfList)
        {
            attemptedCombinations.Add(combinationToAdd);
        }
        else if (attemptedCombinations.Count == maxLengthOfList)
        {
            attemptedCombinations.RemoveAt(0);
            attemptedCombinations.Add(combinationToAdd);
        }
        else
        {
            Debug.LogError("List of attempts went out of bounds");
        }

        // for debugging
        foreach (Combination combination in attemptedCombinations)
        {
            Debug.Log($"{combination.Drink.GetDrinkType()}, {combination.MainCourse.GetMainCourseType()}, {combination.Side.GetSideType()}");
        }

        if (onListOfAttemptsUpdated != null)
        { onListOfAttemptsUpdated.Invoke(); }
       
    }
    public List<Combination> GetListOfAttemptedCombinations()
    {
        return attemptedCombinations;
    }
}
