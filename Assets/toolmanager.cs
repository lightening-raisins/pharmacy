using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public int currentToolIndex = 0; // �ثe���\�������u�� ID�A�q 0 �}�l

    // �ˬd�O�_���\�����Ӥu��
    public bool CanPickUpTool(int toolIndex)
    {
        return toolIndex == currentToolIndex;
    }

    // ���\���_�u���A����U�@�Ӥu��
    public void UnlockNextTool()
    {
        currentToolIndex++;
    }
}
