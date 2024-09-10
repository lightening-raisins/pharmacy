using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneSwitcher : MonoBehaviour
{
    public string targetSceneName;
    public Button specificButton;

    private void Start()
    {
        if (specificButton != null)
        {
            specificButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Specific button is not assigned.");
        }
    }

    void OnButtonClick()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
