using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // ノㄓ陪ボだ计 UI じン
    public int initialScore = 0;  // ﹍だ计匡

    private int score = 0;  // 纗だ计

    void Start()
    {
        Debug.Log("ScoreManager initialized.");

        // 絋玂 scoreText じン竒タ絋砞竚
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned in the Inspector.");
            return;
        }

        // ﹍てだ计
        score = initialScore;
        UpdateScoreUI();
    }

    // 睰だ计穝 UI
    public void AddScore(int points)
    {
        score += points;  // 糤だ计
        UpdateScoreUI();  // –Ωだ常穝 UI 陪ボ
    }

    // 穝 UI 陪ボだ计
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";  // 穝陪ボだ计
            Debug.Log("Score UI updated: " + scoreText.text);  // 块 UI 穝だ计
        }
    }
    // 莉讽玡だ计
    public int GetScore()
    {
        return score;
    }

    // 竚だ计穝 UI
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
}
