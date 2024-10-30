using UnityEngine;
using Valve.VR;  // 使用 SteamVR SDK

public class ClosePopupButton : MonoBehaviour
{
    // 需要關閉的提示窗口
    public GameObject popupWindow;

    // 碰撞進入事件
    private void OnTriggerEnter(Collider other)
    {
        // 檢查碰觸到的對象是否是控制器
        if (other.CompareTag("hand")) // 確保控制器帶有標籤
        {
            ClosePopup();
        }
    }

    // 關閉提示窗口
    public void ClosePopup()
    {
        popupWindow.SetActive(false);
    }
}
