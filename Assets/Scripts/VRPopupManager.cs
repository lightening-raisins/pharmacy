using UnityEngine;
using Valve.VR;

public class VRPopupManager : MonoBehaviour
{
    public GameObject[] popupWindows; // ��J�Ҧ����ܵ��f
    private int currentPopupIndex = 0;

    public SteamVR_Action_Boolean closeAction; // ��JSteamVR��X��ާ@

    void Update()
    {
        if (closeAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            if (popupWindows.Length > 0)
            {
                // ���÷�e���ܵ��f
                popupWindows[currentPopupIndex].SetActive(false);

                // ��ܤU�@�Ӵ��ܵ��f
                currentPopupIndex = (currentPopupIndex + 1) % popupWindows.Length;
                popupWindows[currentPopupIndex].SetActive(true);
            }
        }
    }
}
