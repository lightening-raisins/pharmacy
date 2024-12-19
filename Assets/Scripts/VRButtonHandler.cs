using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRButtonHandler : MonoBehaviour
{
    public GameObject popupWindow; // ��J�A�����ܵ��f
    public ScoreManager scoreManager; // ��J ScoreManager ����

    // �������I����s���ɭ�
    private void OnHandHoverBegin(Hand hand)
    {
        // ��ܴ��ܵ��f
        popupWindow.SetActive(true);

        // �[ 5 ��
        if (scoreManager != null)
        {
            scoreManager.AddScore(5);
        }
        else
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
