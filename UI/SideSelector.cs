using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideSelector : MonoBehaviour, IChangeSelection
{
    [SerializeField] SideObject[] sides;
    [SerializeField] Image imageField;

    private int currentIndex;

    private void Start()
    {
        currentIndex = 0;
        UpdateImage();
    }

    private void UpdateImage()
    {
        imageField.sprite = sides[currentIndex].GetSprite();
    }

    public void Next()
    {
        if (currentIndex == sides.Length - 1)
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
            currentIndex = sides.Length - 1;
        }
        else
        {
            currentIndex--;
        }
        UpdateImage();
    }

    public SideObject GetSideType()
    {
        return sides[currentIndex];
    }
}
