using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // �i�b Inspector ���]�w�������W��
    public string sceneName;

    // �i�����s�ϥΪ���k
    public void LoadSceneByName()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("�г]�w�n�����������W�١I");
        }
    }
}
