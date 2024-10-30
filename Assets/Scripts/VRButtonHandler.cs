using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRButtonHandler : MonoBehaviour
{
    public GameObject popupWindow; // ��J�A�����ܵ��f
    public ScoreManager scoreManager; // ��J ScoreManager ���

    // �������I����s���ɭ�
    private void OnHandHoverBegin(Hand hand)
    {
        // ��ܴ��ܵ��f
        popupWindow.SetActive(true);
    }

    private void OnHandHoverEnd(Hand hand)
    {
        // ���ô��ܵ��f (�p�G�ݭn)
        // popupWindow.SetActive(false);
    }

    // ���I����s�ë��U��
    private void OnHandClick(Hand hand)
    {
        // �[��
        scoreManager.AddScore(5); // ���]�A�w�g�b ScoreManager ���w�q�F AddScore ��k

        // �i�H�b�o����ܴ��ܵ��f�����e
        popupWindow.SetActive(true);
    }
}
    