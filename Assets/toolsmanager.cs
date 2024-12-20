using UnityEngine;
using Valve.VR.InteractionSystem;
using System.Collections.Generic;

public class SpillBoxToolManager : MonoBehaviour
{
    // �u�㪺���ǦC��
    public List<GameObject> tools;

    private int currentToolIndex = 0; // ��e���\������u�����

    void Start()
    {
        // ��l�Ƥu�㪺�ҥΪ��A
        UpdateToolAvailability();
    }

    // ��s�u��ҥΪ��A
    private void UpdateToolAvailability()
    {
        for (int i = 0; i < tools.Count; i++)
        {
            if (tools[i] != null)
            {
                // �u����e�u�㪺 Interactable �ե�ҥ�
                Interactable interactable = tools[i].GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.enabled = (i == currentToolIndex);
                }
            }
        }
    }

    // ��u��Q����ɩI�s����k
    public void OnToolGrabbed(GameObject grabbedTool)
    {
        // �T�{������u��O��e���\���u��
        if (tools[currentToolIndex] == grabbedTool)
        {
            Debug.Log($"�u�� {grabbedTool.name} �Q����I");

            // �T�η�e�u�㪺 Interactable
            Interactable interactable = grabbedTool.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.enabled = false;
            }

            // ��s���ި�U�@�Ӥu��
            currentToolIndex++;
            if (currentToolIndex < tools.Count)
            {
                UpdateToolAvailability();
            }
        }
        else
        {
            Debug.LogWarning("�����Ӷ��ǧ���u��I");
        }
    }
}
