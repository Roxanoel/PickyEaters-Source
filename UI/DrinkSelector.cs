using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkSelector : MonoBehaviour, IChangeSelection
{
    [SerializeField] DrinkObject[] drinks;
    [SerializeField] Image imageField;

    private int currentIndex;

    private void Start()
    {
        currentIndex = 0;
        UpdateImage();
    }

    private void UpdateImage()
    {
        imageField.sprite = drinks[currentIndex].GetSprite();
    }

    public void Next()
    {
        if (currentIndex == drinks.Length - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
        UpdateImage();
    }

    public void Previous()
    {
        if (currentIndex == 0)
        {
            currentIndex = drinks.Length - 1;
        }
        else
        {
            currentIndex--;
        }
        UpdateImage();
    }

    public DrinkObject GetDrinkType()
    {
        return drinks[currentIndex];
    }
}
