using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    // 工具的引用
    public ToolController tool;

    // 呼叫此方法卸下裝備並重新激活工具
    public void RemoveEquipment()
    {
        if (tool != null)
        {
            tool.ReactivateTool();
        }
    }
}
