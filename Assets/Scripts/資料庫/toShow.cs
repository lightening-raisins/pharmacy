using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toShow : MonoBehaviour
{
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
       
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }
}
