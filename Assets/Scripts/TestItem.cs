using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TestItem : MonoBehaviour
{
    private GrabItemsManager grabItemsManager;

    void Start()
    {
        // 獲取 GrabItemsManager 的實例
        grabItemsManager = FindObjectOfType<GrabItemsManager>();
        if (grabItemsManager == null)
        {
            Debug.LogError("未找到 GrabItemsManager！");
        }
    }

    void OnAttachedToHand(Hand hand)
    {
        // 當物品被抓取時調用 GrabItem 方法
        grabItemsManager.GrabItem(gameObject);
    }
}
