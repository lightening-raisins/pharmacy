using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class GrabItemsManager : MonoBehaviour
{
    [System.Serializable]
    public class ItemGroup
    {
        public string groupName;
        public List<GameObject> items;              
        public List<GameObject> messagePanels;      
        public bool allItemsGrabbed;                
    }

    public List<ItemGroup> itemGroups;              
    private Dictionary<string, HashSet<GameObject>> grabbedItems = new Dictionary<string, HashSet<GameObject>>();

    void Start()
    {
        // ��l�ƨC�ժ��󪺴��ܭ��O
        foreach (var group in itemGroups)
        {
            foreach (var panel in group.messagePanels)
            {
                if (panel != null)
                    panel.SetActive(false);          // ��l�Ʈ����éҦ����ܪ���
            }
            grabbedItems[group.groupName] = new HashSet<GameObject>();
        }
    }

    // ������~����k
    public void GrabItem(GameObject item)
    {
        foreach (var group in itemGroups)
        {
            if (group.items.Contains(item) && !grabbedItems[group.groupName].Contains(item))
            {
                grabbedItems[group.groupName].Add(item);
                Debug.Log("�w������~: " + item.name + " (�ݩ�էO: " + group.groupName + ")");

                // �ˬd�O�_�w����էO�����Ҧ�����
                CheckAllItemsGrabbed(group);
                return;
            }
        }
        //Debug.Log("�����~���ݩ����ݭn������էO�Τw�Q���: " + item.name);
    }

    private void CheckAllItemsGrabbed(ItemGroup group)
    {
        // �����ƶq�F��ؼЮɡA��ܴ��ܪ���C�����Ҧ�����
        if (grabbedItems[group.groupName].Count == group.items.Count)
        {
            foreach (var panel in group.messagePanels)
            {
                if (panel != null)
                    panel.SetActive(true);            // ��ܩҦ����������ܪ���
            }

            group.allItemsGrabbed = true;
            Debug.Log("�էO " + group.groupName + " ���Ҧ����~�w����A��ܹ������ܮ�");
        }
        else
        {
            Debug.Log("�|������էO " + group.groupName + " ���Ҧ����~");
        }
    }
}
