using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRButtonHandler : MonoBehaviour
{
    public GameObject popupWindow; // ��J�A�����ܵ��f
    public ScoreManager scoreManager; // ��J ScoreManager ����

    private bool scoreAdded = false; // �l�ܬO�_�w�g�[�L����

    // �������I����s���ɭ�
    private void OnHandHoverBegin(Hand hand)
    {
        // ��ܴ��ܵ��f
        popupWindow.SetActive(true);

        // �p�G�٨S�[�L���ơA�[��
        if (!scoreAdded && scoreManager != null)
        {
            scoreManager.AddScore(5);
            scoreAdded = true; // �O���w�g�[�L����
        }
        else if (scoreManager == null)
        {
            Debug.LogError("ScoreManager is not assigned in VRButtonHandler.");
        }
    }

    private void OnHandHoverEnd(Hand hand)
    {
        // ���ô��ܵ��f (�p�G�ݭn)
        // popupWindow.SetActive(false);
    }
}
