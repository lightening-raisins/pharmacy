using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public int currentToolIndex = 0; // 目前允許拿取的工具 ID，從 0 開始

    // 檢查是否允許拿取該工具
    public bool CanPickUpTool(int toolIndex)
    {
        return toolIndex == currentToolIndex;
    }

    // 當成功拿起工具後，解鎖下一個工具
    public void UnlockNextTool()
    {
        currentToolIndex++;
    }
}
