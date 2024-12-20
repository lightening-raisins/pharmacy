using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DustpanCollision : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // 要隱藏的物件
    [SerializeField] private GameObject displayObject;
    [SerializeField] private string glassJarTag = "GlassJar"; // 玻璃罐的標籤

    public GameObject bottleCap; // 瓶蓋物件

    public SteamVR_Input_Sources leftHand; // 左手
    public SteamVR_Input_Sources rightHand; // 右手
    public SteamVR_Action_Boolean grabAction; // 用來偵測抓取的行為

    private bool leftHandTouching = false;
    private bool rightHandTouching = false;
    private bool targetObjectHidden = false; // 標記目標物件是否已隱藏

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("碰撞到的物件是: " + collision.gameObject.name); // 印出碰撞物件的名稱
        // 確認碰撞到的物件是否是玻璃罐
        if (collision.gameObject.CompareTag(glassJarTag))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(false); // 隱藏目標物件
                targetObjectHidden = true; // 設置目標物件已隱藏
            }
            else
            {
                Debug.LogWarning("未指定要隱藏的物件！");
            }
        }
    }

    void Update()
    {
        // 檢查雙手是否抓取
        if (grabAction.GetStateDown(leftHand))
        {
            leftHandTouching = true;
        }
        if (grabAction.GetStateDown(rightHand))
        {
            rightHandTouching = true;
        }

        // 檢查雙手是否放開
        if (grabAction.GetStateUp(leftHand))
        {
            leftHandTouching = false;
        }
        if (grabAction.GetStateUp(rightHand))
        {
            rightHandTouching = false;
        }

        // 當雙手都觸碰且目標物件已隱藏時顯示瓶蓋
        if (leftHandTouching && rightHandTouching && targetObjectHidden)
        {
            bottleCap.SetActive(true);
            displayObject.SetActive(true);
        }
    }
}
