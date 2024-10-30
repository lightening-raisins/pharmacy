using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRTriggerHandler : MonoBehaviour
{
    public GameObject objectToShow; // 拖入你想顯示的物件
    public GameObject objectToHide; // 拖入你想隱藏的物件

    public GameObject targetObject1; // 指定的第一個碰觸物件
    public GameObject targetObject2; // 指定的第二個碰觸物件

    private bool isTarget1Touched = false; // 記錄 targetObject1 是否被觸碰
    private bool isTarget2Touched = false; // 記錄 targetObject2 是否被觸碰

    // 當控制器的手碰到物件的時候
    private void OnHandHoverBegin(Hand hand)
    {
        Debug.Log("Hand hovered over: " + hand.gameObject.name);

        if (hand.gameObject == targetObject1)
        {
            isTarget1Touched = true;
            Debug.Log("Target 1 touched");
        }
        else if (hand.gameObject == targetObject2)
        {
            isTarget2Touched = true;
            Debug.Log("Target 2 touched");
        }

        if (isTarget1Touched && isTarget2Touched)
        {
            ShowObject();
            HideObject();
            Debug.Log("Both targets touched. Showing object and hiding another.");
        }
    }

    private void OnHandHoverEnd(Hand hand)
    {
        Debug.Log("Hand stopped hovering over: " + hand.gameObject.name);

        if (hand.gameObject == targetObject1)
        {
            isTarget1Touched = false;
            Debug.Log("Target 1 released");
        }
        else if (hand.gameObject == targetObject2)
        {
            isTarget2Touched = false;
            Debug.Log("Target 2 released");
        }
    }

    private void ShowObject()
    {
        // 顯示物件
        objectToShow.SetActive(true);
    }

    private void HideObject()
    {
        // 隱藏物件
        objectToHide.SetActive(false);
    }
}
