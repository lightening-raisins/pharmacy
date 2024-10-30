using UnityEngine;
using Valve.VR;

public class closewindow : MonoBehaviour
{
    public GameObject popupWindow; // 當前顯示的提示窗口
    public GameObject newPopupWindow; // 要顯示的新提示窗口（可選）
    public SteamVR_Action_Boolean closeWindowAction; // 監聽按鈕動作

    private bool isWindowReplaced = false; // 是否需要替換窗口的標記

    private void Update()
    {
        // 檢測控制器按鈕是否被按下 (例如按下 X 鍵來關閉視窗)
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
            popupWindow.SetActive(false); // 關閉當前提示窗口
            Debug.Log("Popup window closed");

            // 如果新視窗未被指派，則不需要執行任何其他操作
            if (newPopupWindow != null)
            {
                // 設置新窗口的位置和顯示
                newPopupWindow.transform.position = popupWindow.transform.position; // 確保新窗口在相同位置
                newPopupWindow.SetActive(true); // 顯示新窗口
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
        ClosePopupWindow(); // 關閉當前視窗並顯示新視窗（如果有）
    }

    // 設置是否需要替換窗口的標記
    public void SetReplaceWindow(bool replace)
    {
        isWindowReplaced = replace;
    }
}
