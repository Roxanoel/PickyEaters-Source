using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviousAttemptsPanel : MonoBehaviour
{
    // Mappings 
    [SerializeField] RowMapping[] rowMappings;

    [System.Serializable]
    public struct RowMapping
    {
        public Image drinkImage;
        public Image mainImage;
        public Image sideImage;
    }

    [Header("References")]
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject attemptsButton;

    private List<Combination> attemptedCombinations;

    private void OnEnable()
    {
        gameManager.onListOfAttemptsUpdated += UpdateDisplay;
    }

    private void OnDisable()
    {
        gameManager.onListOfAttemptsUpdated -= UpdateDisplay;
    }

    private void Start()
    {
        attemptedCombinations = gameManager.GetListOfAttemptedCombinations();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        foreach (Combination combination in attemptedCombinations)
        {
            int index = attemptedCombinations.IndexOf(combination);
            rowMappings[index].drinkImage.sprite = combination.Drink.GetSprite();
            rowMappings[index].mainImage.sprite = combination.MainCourse.GetSprite();
            rowMappings[index].sideImage.sprite = combination.Side.GetSprite();
        }
    }

    public void ClosePanel()
    {
        attemptsButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
