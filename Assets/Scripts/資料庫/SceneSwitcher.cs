using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    public string targetSceneName;
    public Button specificButton;

    void Start()
    {
        if (specificButton != null)
        {
            // �����w�����s�K�[�I���ƥ��ť��
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