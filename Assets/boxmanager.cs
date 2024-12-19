using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ToolboxManager : MonoBehaviour
{
    // �c�l�����u��
    public GameObject[] tools;

    // �u���l��m
    private Dictionary<GameObject, Vector3> toolPositions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Quaternion> toolRotations = new Dictionary<GameObject, Quaternion>();

    void Start()
    {
        // ��l�ƨC�Ӥu�㪺��m�M����
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
                    // �p�G�u��S���Q����A�O�����m�M����
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
        // ��_�u����l��m�M����
        tool.transform.position = toolPositions[tool];
        tool.transform.rotation = toolRotations[tool];

        // �T�O�u�㪺Rigidbody�������z�v�T
        Rigidbody rb = tool.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
