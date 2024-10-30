using UnityEngine;

public class Close: MonoBehaviour
{
    public GameObject popupWindow; // 拖入你的提示窗口

    private void OnTriggerEnter(Collider other)
    {
        // 檢查碰撞對象是否是 VR 控制器（標籤 "hand"）
        if (other.CompareTag("hand"))
        {
            ClosePopupWindow();
        }
    }

    public void ClosePopupWindow()
    {
        if (popupWindow != null)
        {
            popupWindow.SetActive(false); // 關閉提示窗口
        }
    }
}
