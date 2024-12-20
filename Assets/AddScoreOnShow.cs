using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreOnShow : MonoBehaviour
{
    public ScoreManager scoreManager; // 參考 ScoreManager 腳本
    public int scoreToAdd = 5; // 顯示後要加的分數

    private bool hasScored = false; // 確保每個物件只加一次分

    void Update()
    {
        // 如果物件被顯示且尚未加過分
        if (gameObject.activeSelf && !hasScored)
        {
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreToAdd); // 加分
                hasScored = true; // 確保只加分一次
                Debug.Log($"{gameObject.name} is now visible. Added {scoreToAdd} points.");
            }
            else
            {
                Debug.LogError("ScoreManager is not assigned.");
            }
        }
    }
}
