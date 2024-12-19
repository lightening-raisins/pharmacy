using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ToolboxManager : MonoBehaviour
{
    // 箱子內的工具
    public GameObject[] tools;

    // 工具初始位置
    private Dictionary<GameObject, Vector3> toolPositions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Quaternion> toolRotations = new Dictionary<GameObject, Quaternion>();

    void Start()
    {
        // 初始化每個工具的位置和旋轉
        foreach (GameObject tool in tools)
        {
            if (tool != null)
            {
                toolPositions[tool] = tool.transform.position;
                toolRotations[tool] = tool.transform.rotation;
            }
        }
    }

    void Update()
    {
        foreach (GameObject tool in tools)
        {
            if (tool != null)
            {
                Interactable interactable = tool.GetComponent<Interactable>();
                if (interactable != null)
                {
                    // 如果工具沒有被抓取，保持其位置和旋轉
                    if (interactable.attachedToHand == null)
                    {
                        LockTool(tool);
                    }
                }
            }
        }
    }

    private void LockTool(GameObject tool)
    {
        // 恢復工具到原始位置和旋轉
        tool.transform.position = toolPositions[tool];
        tool.transform.rotation = toolRotations[tool];

        // 確保工具的Rigidbody不受物理影響
        Rigidbody rb = tool.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
