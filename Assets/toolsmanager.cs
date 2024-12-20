using UnityEngine;
using Valve.VR.InteractionSystem;
using System.Collections.Generic;

public class SpillBoxToolManager : MonoBehaviour
{
    // 工具的順序列表
    public List<GameObject> tools;

    private int currentToolIndex = 0; // 當前允許抓取的工具索引

    void Start()
    {
        // 初始化工具的啟用狀態
        UpdateToolAvailability();
    }

    // 更新工具啟用狀態
    private void UpdateToolAvailability()
    {
        for (int i = 0; i < tools.Count; i++)
        {
            if (tools[i] != null)
            {
                // 只有當前工具的 Interactable 組件啟用
                Interactable interactable = tools[i].GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.enabled = (i == currentToolIndex);
                }
            }
        }
    }

    // 當工具被抓取時呼叫此方法
    public void OnToolGrabbed(GameObject grabbedTool)
    {
        // 確認抓取的工具是當前允許的工具
        if (tools[currentToolIndex] == grabbedTool)
        {
            Debug.Log($"工具 {grabbedTool.name} 被抓取！");

            // 禁用當前工具的 Interactable
            Interactable interactable = grabbedTool.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.enabled = false;
            }

            // 更新索引到下一個工具
            currentToolIndex++;
            if (currentToolIndex < tools.Count)
            {
                UpdateToolAvailability();
            }
        }
        else
        {
            Debug.LogWarning("未按照順序抓取工具！");
        }
    }
}
