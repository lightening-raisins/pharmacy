using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreOnShow : MonoBehaviour
{
    public ScoreManager scoreManager; // �Ѧ� ScoreManager �}��
    public int scoreToAdd = 5; // ��ܫ�n�[������

    private bool hasScored = false; // �T�O�C�Ӫ���u�[�@����

    void Update()
    {
        // �p�G����Q��ܥB�|���[�L��
        if (gameObject.activeSelf && !hasScored)
        {
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreToAdd); // �[��
                hasScored = true; // �T�O�u�[���@��
                Debug.Log($"{gameObject.name} is now visible. Added {scoreToAdd} points.");
            }
            else
            {
                Debug.LogError("ScoreManager is not assigned.");
            }
        }
    }
}
