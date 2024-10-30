using UnityEngine;
using Valve.VR;  // �ϥ� SteamVR SDK

public class ClosePopupButton : MonoBehaviour
{
    // �ݭn���������ܵ��f
    public GameObject popupWindow;

    // �I���i�J�ƥ�
    private void OnTriggerEnter(Collider other)
    {
        // �ˬd�IĲ�쪺��H�O�_�O���
        if (other.CompareTag("hand")) // �T�O����a������
        {
            ClosePopup();
        }
    }

    // �������ܵ��f
    public void ClosePopup()
    {
        popupWindow.SetActive(false);
    }
}
