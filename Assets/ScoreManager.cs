using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // �Ψ���ܤ��ƪ� UI ����
    public int initialScore = 0;  // ��l���ơ]�i��^

    private int score = 0;  // �x�s����

    void Start()
    {
        Debug.Log("ScoreManager initialized.");

        // �T�O scoreText ����w�g���T�]�m
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned in the Inspector.");
            return;
        }

        // ��l�Ƥ���
        score = initialScore;
        UpdateScoreUI();
    }

    // �K�[���ƨç�s UI
    public void AddScore(int points)
    {
        score += points;  // �W�[����
        UpdateScoreUI();  // �C���[���᳣��s UI ���
    }

    // ��s UI �W��ܪ�����
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";  // ��s��ܪ�����
            Debug.Log("Score UI updated: " + scoreText.text);  // ��X UI ��s�᪺����
        }
    }
    // �����e����
    public int GetScore()
    {
        return score;
    }

    // ���m���ƨç�s UI
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
}
