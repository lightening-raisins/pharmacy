using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    // �u�㪺�ޥ�
    public ToolController tool;

    // �I�s����k���U�˳ƨí��s�E���u��
    public void RemoveEquipment()
    {
        if (tool != null)
        {
            tool.ReactivateTool();
        }
    }
}
