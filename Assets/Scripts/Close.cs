using UnityEngine;

public class Close: MonoBehaviour
{
    public GameObject popupWindow; // ��J�A�����ܵ��f

    private void OnTriggerEnter(Collider other)
    {
        // �ˬd�I����H�O�_�O VR ����]���� "hand"�^
        if (other.CompareTag("hand"))
        {
            ClosePopupWindow();
        }
    }

    public void ClosePopupWindow()
    {
        if (popupWindow != null)
        {
            popupWindow.SetActive(false); // �������ܵ��f
        }
    }
}
