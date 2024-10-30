using UnityEngine;
using UnityEngine.UI;  // �T�O�ޤJ�F�o�өR�W�Ŷ��Өϥ�Text UI����

public class ScoreManager : MonoBehaviour
{
    public GameObject intercomButton;  // �����������s������
    public Text scoreText;  // �Ψ���ܤ��ƪ�UI Text����
    private bool step1Completed = false;
    private int score = 0;  // �x�s����

    void Start()
    {
        UpdateScoreUI();  // ��l�ɧ�s�@���������
    }

    void OnTriggerEnter(Collider other)
    {
        // �ˬd�I������H�O�_��VR��α��
        if (other.CompareTag("hand") && !step1Completed)
        {
            OnIntercomButtonPressed();
        }
    }


    // ����U���������s��Ĳ�o
    void OnIntercomButtonPressed()
    {
        if (!step1Completed)
        {
            step1Completed = true;
            AddScore(5);  // �������s�ʧ@�[5��
        }
    }

    // �K�[���ƨç�sUI
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();  // �C���[���᳣��sUI���
    }

    // ��sUI�W��ܪ�����
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
