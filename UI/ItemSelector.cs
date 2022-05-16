using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public delegate void SubmitChoiceHandler(DrinkObject drink, MainCourseObject main, SideObject side);

public class ItemSelector : MonoBehaviour
{
    [Header("Selectors")]
    [SerializeField] DrinkSelector drinkSelector;
    [SerializeField] MainCourseSelector mainCourseSelector;
    [SerializeField] SideSelector sideSelector;

    DrinkObject selectedDrink;
    MainCourseObject selectedMainCourse;
    SideObject selectedSide;

    [Header("Blocker")]
    [SerializeField] Canvas blockerCanvas;

    public event SubmitChoiceHandler onSubmitChoice;

    public void EnableBlockerCanvas (bool isEnabled)
    {
        blockerCanvas.gameObject.SetActive(isEnabled);
    }
    
    public void SubmitChoices()
    {
        // Set each variable to the right value
        selectedDrink = drinkSelector.GetDrinkType();
        selectedMainCourse = mainCourseSelector.GetMainCourseType();
        selectedSide = sideSelector.GetSideType();
        
        // Trigger an event with 3 parameters, GameManager is the listener
        if (onSubmitChoice != null)
        {
            onSubmitChoice.Invoke(selectedDrink, selectedMainCourse, selectedSide);
        }
    }
}
