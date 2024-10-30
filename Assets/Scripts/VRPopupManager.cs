using UnityEngine;
using Valve.VR;

public class VRPopupManager : MonoBehaviour
{
    public GameObject[] popupWindows; // 拖入所有提示窗口
    private int currentPopupIndex = 0;

    public SteamVR_Action_Boolean closeAction; // 拖入SteamVR的X鍵操作

    void Update()
    {
        if (closeAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            if (popupWindows.Length > 0)
            {
                // 隱藏當前提示窗口
                popupWindows[currentPopupIndex].SetActive(false);

                // 顯示下一個提示窗口
                currentPopupIndex = (currentPopupIndex + 1) % popupWindows.Length;
                popupWindows[currentPopupIndex].SetActive(true);
            }
        }
    }
}
