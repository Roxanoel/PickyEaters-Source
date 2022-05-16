using System.Collections;
using UnityEngine;

public class Eater : MonoBehaviour
{
    // This class is instantiated for each eater and is responsible for:
    // - Generating and storing this individual eater's prefenrences;
    // - Checking and storing whether the client is satisfied;
    // - Instantiating and destroying the VFX for eater reactions.
    
    [SerializeField] bool doesNotWant = true; //true for now, for now it will be something they don't want 

    [SerializeField] SuccessVFX successVFX;
    [SerializeField] FailureVFX failureVFX;
    private bool isSatisfied = false;
    [SerializeField] float vFXDurationInSeconds = 3;

    FoodItems.Drink drinkPreference = FoodItems.Drink.Null;
    FoodItems.MainCourse mainCoursePreference = FoodItems.MainCourse.Null;
    FoodItems.Side sidePreference = FoodItems.Side.Null;
    PreferenceType preferenceType = PreferenceType.Null;
   
    PreferenceType[] preferenceTypes;
    FoodItems.Drink[] drinks;
    FoodItems.MainCourse[] mainCourses;
    FoodItems.Side[] sides;

 
    private void Awake()
    {
        preferenceTypes = new PreferenceType[] { PreferenceType.Drink, PreferenceType.MainCourse, PreferenceType.Side};
        // For eventual optimisation, creating a centralized SO storing all the arrays would be simpler.
        drinks = new FoodItems.Drink[] { FoodItems.Drink.Lemonade, FoodItems.Drink.SodaCan, FoodItems.Drink.Wine };
        mainCourses = new FoodItems.MainCourse[] { FoodItems.MainCourse.Hamburger, FoodItems.MainCourse.Pizza, FoodItems.MainCourse.Sub };
        sides = new FoodItems.Side[] { FoodItems.Side.Fries, FoodItems.Side.Salad, FoodItems.Side.Sundae };
    }

    public void SetUp()
    {
        DeterminePreference();
    }

    public PreferenceType IndexToPreferenceType(int index) => index switch
    {
        0 => PreferenceType.Drink,
        1 => PreferenceType.MainCourse,
        2 => PreferenceType.Side,
        _ => PreferenceType.Null,
    };

    public bool GetIsSatisfied()
    {
        return isSatisfied;
    }

    private void DeterminePreference()
    {
        int preferenceTypeIndex = Random.Range(0, preferenceTypes.Length);
        preferenceType = IndexToPreferenceType(preferenceTypeIndex);

        switch (preferenceType)
        {
            case PreferenceType.Drink: 
                AssignDrinkPreference();
                return;

            case PreferenceType.MainCourse: 
                AssignMainCoursePreference();
                return;

            case PreferenceType.Side: 
                AssignSidePreference();
                return;
        }

        Debug.LogError("ERROR: Preference type assigned was null");

    }
    private void AssignDrinkPreference()
    {
        drinkPreference = drinks[Random.Range(0, drinks.Length)];
        Debug.Log($"{gameObject.name} does not want({doesNotWant}) {drinkPreference}");
    }

    private void AssignMainCoursePreference()
    {
        mainCoursePreference = mainCourses[Random.Range(0, mainCourses.Length)];
        Debug.Log($"{gameObject.name} does not want({doesNotWant}) {mainCoursePreference}");
    }

    private void AssignSidePreference()
    {
        sidePreference = sides[Random.Range(0, sides.Length)];
        Debug.Log($"{gameObject.name} does not want({doesNotWant}) {sidePreference}");
    }

    public void CheckIfSatisfied(FoodItems.Drink selectedDrink, FoodItems.MainCourse selectedMain, FoodItems.Side selectedSide)
    {
        switch (preferenceType)
        {
            case PreferenceType.Drink: 
                if (doesNotWant)
                {
                    isSatisfied = (selectedDrink == drinkPreference) ? false : true;
                }
                else
                {
                    isSatisfied = (selectedDrink == drinkPreference) ? true : false;
                }
                //Debug.Log($"{gameObject.name} is satisfied? {isSatisfied}");
                StartCoroutine(PlaySatisfactionVFX());
                return;
            case PreferenceType.MainCourse: 
                if (doesNotWant)
                {
                    isSatisfied = (selectedMain == mainCoursePreference) ? false : true;
                }
                else
                {
                    isSatisfied = (selectedMain == mainCoursePreference) ? true : false;
                }
                //Debug.Log($"{gameObject.name} is satisfied? {isSatisfied}");
                StartCoroutine(PlaySatisfactionVFX());
                return;
            case PreferenceType.Side: 
                if (doesNotWant)
                {
                    isSatisfied = (selectedSide == sidePreference) ? false : true;
                }
                else
                {
                    isSatisfied = (selectedSide == sidePreference) ? true : false;
                }
                //Debug.Log($"{gameObject.name} is satisfied? {isSatisfied}");
                StartCoroutine(PlaySatisfactionVFX());
                return;
        }
        Debug.LogError("Preference was null");
    }

    public PreferenceType GetPreferenceType()
    {
        return preferenceType;
    }

    public FoodItems.Drink GetDrinkPreference()
    {
        return drinkPreference;
    }

    public FoodItems.MainCourse GetMainCoursePreference()
    {
        return mainCoursePreference;
    }

    public FoodItems.Side GetSidePreference()
    {
        return sidePreference;
    }

    private IEnumerator PlaySatisfactionVFX()
    {
        GameObject vfx = isSatisfied ? successVFX.gameObject : failureVFX.gameObject;
        vfx.SetActive(true);
        yield return new WaitForSeconds(vFXDurationInSeconds);
        vfx.SetActive(false);
    }
}
