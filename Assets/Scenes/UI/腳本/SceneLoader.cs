using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 可在 Inspector 中設定的場景名稱
    public string sceneName;

    // 可給按鈕使用的方法
    public void LoadSceneByName()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("請設定要切換的場景名稱！");
        }
    }
}
