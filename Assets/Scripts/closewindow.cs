using UnityEngine;
using Valve.VR;

public class closewindow : MonoBehaviour
{
    public GameObject popupWindow; // ��e��ܪ����ܵ��f
    public GameObject newPopupWindow; // �n��ܪ��s���ܵ��f�]�i��^
    public SteamVR_Action_Boolean closeWindowAction; // ��ť���s�ʧ@

    private bool isWindowReplaced = false; // �O�_�ݭn�������f���аO

    private void Update()
    {
        // �˴�������s�O�_�Q���U (�Ҧp���U X �����������)
        if (closeWindowAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Close button pressed");

            if (isWindowReplaced)
            {
                ReplacePopupWindow();
            }
            else
            {
                ClosePopupWindow();
            }
        }
    }

    public void ClosePopupWindow()
    {
        if (popupWindow != null)
        {
            popupWindow.SetActive(false); // ������e���ܵ��f
            Debug.Log("Popup window closed");

            // �p�G�s�������Q�����A�h���ݭn��������L�ާ@
            if (newPopupWindow != null)
            {
                // �]�m�s���f����m�M���
                newPopupWindow.transform.position = popupWindow.transform.position; // �T�O�s���f�b�ۦP��m
                newPopupWindow.SetActive(true); // ��ܷs���f
                Debug.Log("New popup window displayed");
            }
        }
        else
        {
            Debug.LogError("No popup window assigned!");
        }
    }

    public void ReplacePopupWindow()
    {
        ClosePopupWindow(); // ������e��������ܷs�����]�p�G���^
    }

    // �]�m�O�_�ݭn�������f���аO
    public void SetReplaceWindow(bool replace)
    {
        isWindowReplaced = replace;
    }
}
