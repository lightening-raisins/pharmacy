using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class GrabItemsManager : MonoBehaviour
{
    public List<GameObject> requiredItems;  
    public GameObject messagePanel;          
    private HashSet<GameObject> grabbedItems = new HashSet<GameObject>();  // 追蹤已抓取的物品

    void Start()
    {
        messagePanel.SetActive(false);  
    }

    // 抓取物品的方法
    public void GrabItem(GameObject item)
    {
        Debug.Log("GrabItem 被調用，物品名稱: " + item.name); 

        // 檢查是否是需要的物品
        if (requiredItems.Contains(item) && !grabbedItems.Contains(item))
        {
            grabbedItems.Add(item);  
            CheckAllItemsGrabbed(); 
        }
        else
        {
            Debug.Log("此物品不在需要抓取的物品列表中或已被抓取: " + item.name);
        }
    }

    private void CheckAllItemsGrabbed()
    {

        if (grabbedItems.Count == requiredItems.Count)
        {
            ShowMessage();  
        }
        else
        {
            Debug.Log("尚未抓取所有需要的物品");
        }
    }

    private void ShowMessage()
    {
        messagePanel.SetActive(true);
        Debug.Log("所有物品已抓取，顯示提示框");
    }
}
