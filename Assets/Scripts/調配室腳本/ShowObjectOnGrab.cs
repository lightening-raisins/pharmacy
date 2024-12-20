using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShowObjectOnGrab : MonoBehaviour
{
    public GameObject objectToShow; // 要顯示的物件

    private Interactable interactable; // 互動系統的腳本

    void Start()
    {
        // 確保目標物件一開始隱藏
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }

        // 獲取當前物件的 Interactable 組件
        interactable = GetComponent<Interactable>();
        if (interactable == null)
        {
            Debug.LogError("此物件需要有 Interactable 組件！");
        }
    }

    void HandHoverUpdate(Hand hand)
    {
        // 當物件被抓取
        if (interactable != null && hand.GetGrabStarting() != GrabTypes.None)
        {
            if (objectToShow != null)
            {
                objectToShow.SetActive(true); // 顯示目標物件
            }
        }
    }
}
