using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination
{
    // Fields
    private DrinkObject drink;
    private MainCourseObject mainCourse;
    private SideObject side;

    // Properties 
    public DrinkObject Drink { get { return drink; } set { drink = value; } }
    public MainCourseObject MainCourse { get { return mainCourse; } set { mainCourse = value; } }
    public SideObject Side { get { return side; } set { side = value; } }

    // Constructor
    public Combination(DrinkObject chosenDrink, MainCourseObject chosenMainCourse, SideObject chosenSide)
    {
        Drink = chosenDrink;
        MainCourse = chosenMainCourse;
        Side = chosenSide;
    }

}
