using UnityEngine;
using Valve.VR;

public class ToolAndPopupManager : MonoBehaviour
{
    // SteamVR 動作
    public SteamVR_Action_Boolean actionYButton;  // 用於工具切換的 Y 按鈕
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.LeftHand;

    // 工具管理
    public GameObject currentTool; // 當前抓取的工具
    public GameObject toolToSwitchWith; // 要交換的工具

    // 提示窗口管理
    public GameObject popupWindow; // 當前顯示的提示窗口
    public GameObject newPopupWindow; // 要顯示的新提示窗口（可選）

    private bool isWindowReplaced = false; // 是否需要替換窗口的標記

    void Update()
    {
        // 檢測是否按下 Y 按鈕
        if (actionYButton.GetStateDown(handType))
        {
            HandleYButtonPress();  // 處理 Y 按鈕按下邏輯
        }
    }

    // 按下 Y 按鈕時處理邏輯
    private void HandleYButtonPress()
    {
        if (currentTool != null && toolToSwitchWith != null)
        {
            // 1. 交換當前工具和指定工具
            SwapTools();

            // 2. 處理提示窗口邏輯
            if (isWindowReplaced)
            {
                ReplacePopupWindow();
            }
            else
            {
                ClosePopupWindow();
            }
        }
        else
        {
            Debug.LogWarning("工具或提示窗口未正確設置！");
        }
    }

    // 工具交換邏輯
    private void SwapTools()
    {
        // 檢查工具是否正確設置
        if (currentTool == null || toolToSwitchWith == null)
        {
            Debug.LogError("currentTool 或 toolToSwitchWith 尚未設置！");
            return;
        }

        // 隱藏當前工具
        currentTool.SetActive(false);

        // 顯示要交換的工具
        toolToSwitchWith.SetActive(true);

        // 確保交換的工具與當前工具的位置和旋轉一致
        toolToSwitchWith.transform.position = currentTool.transform.position;
        toolToSwitchWith.transform.rotation = currentTool.transform.rotation;

        // 更新當前工具為交換後的工具
        currentTool = toolToSwitchWith;

        Debug.Log("工具已交換！");

        // 可選：每次交換後將 toolToSwitchWith 設為 null，確保不會不小心再次交換
        toolToSwitchWith = null;  // 設為 null，直到重新指定新的工具
    }

    // 關閉當前提示窗口，並（可選）顯示新窗口
    public void ClosePopupWindow()
    {
        if (popupWindow != null)
        {
            popupWindow.SetActive(false); // 關閉當前提示窗口

            // 如果有新窗口，顯示它
            if (newPopupWindow != null)
            {
                newPopupWindow.transform.position = popupWindow.transform.position; // 保持位置一致
                newPopupWindow.SetActive(true); // 顯示新窗口
            }
        }
        else
        {
            Debug.LogWarning("當前提示窗口未設置！");
        }
    }

    // 替換提示窗口
    public void ReplacePopupWindow()
    {
        ClosePopupWindow(); // 關閉當前窗口並顯示新窗口（如果有）
    }

    // 設置是否需要替換窗口的標記
    public void SetReplaceWindow(bool replace)
    {
        isWindowReplaced = replace;
    }
}