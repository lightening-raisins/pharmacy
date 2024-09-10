using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleObjects : MonoBehaviour
{
    public GameObject objectToHide;
    public GameObject objectToShow;
    public Button toggleButton;  

    void Start()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleObjectsVisibility);
        }
    }

    public void ToggleObjectsVisibility()
    {
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);  
        }

        if (objectToShow != null)
        {
            objectToShow.SetActive(true);  
        }
    }
}
