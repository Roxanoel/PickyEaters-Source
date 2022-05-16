using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCourseSelector : MonoBehaviour, IChangeSelection
{
    [SerializeField] MainCourseObject[] mains;
    [SerializeField] Image imageField;

    private int currentIndex;

    private void Start()
    {
        currentIndex = 0;
        UpdateImage();
    }

    private void UpdateImage()
    {
        imageField.sprite = mains[currentIndex].GetSprite();
    }

    public void Next()
    {
        if (currentIndex == mains.Length - 1)
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
            currentIndex = mains.Length - 1;
        }
        else
        {
            currentIndex--;
        }
        UpdateImage();
    }

    public MainCourseObject GetMainCourseType()
    {
        return mains[currentIndex];
    }
}
