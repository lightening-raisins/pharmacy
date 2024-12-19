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
        // 初始化每組物件的提示面板
        foreach (var group in itemGroups)
        {
            foreach (var panel in group.messagePanels)
            {
                if (panel != null)
                    panel.SetActive(false);          // 初始化時隱藏所有提示物件
            }
            grabbedItems[group.groupName] = new HashSet<GameObject>();
        }
    }

    // 抓取物品的方法
    public void GrabItem(GameObject item)
    {
        foreach (var group in itemGroups)
        {
            if (group.items.Contains(item) && !grabbedItems[group.groupName].Contains(item))
            {
                grabbedItems[group.groupName].Add(item);
                Debug.Log("已抓取物品: " + item.name + " (屬於組別: " + group.groupName + ")");

                // 檢查是否已抓取組別中的所有物件
                CheckAllItemsGrabbed(group);
                return;
            }
        }
        //Debug.Log("此物品不屬於任何需要抓取的組別或已被抓取: " + item.name);
    }

    private void CheckAllItemsGrabbed(ItemGroup group)
    {
        // 當抓取數量達到目標時，顯示提示物件列表中的所有物件
        if (grabbedItems[group.groupName].Count == group.items.Count)
        {
            foreach (var panel in group.messagePanels)
            {
                if (panel != null)
                    panel.SetActive(true);            // 顯示所有對應的提示物件
            }

            group.allItemsGrabbed = true;
            Debug.Log("組別 " + group.groupName + " 中所有物品已抓取，顯示對應提示框");
        }
        else
        {
            Debug.Log("尚未抓取組別 " + group.groupName + " 的所有物品");
        }
    }
}
