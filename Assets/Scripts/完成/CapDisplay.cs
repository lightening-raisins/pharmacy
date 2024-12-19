using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CapDisplay : MonoBehaviour
{
    public GameObject bottleCap; // 瓶蓋物件
    public GameObject lowWater; // 低水位標籤
    public GameObject highWater; // 高水位標籤

    public SteamVR_Input_Sources leftHand; // 左手
    public SteamVR_Input_Sources rightHand; // 右手
    public SteamVR_Action_Boolean grabAction; // 用來偵測抓取的行為

    private bool leftHandTouching = false;
    private bool rightHandTouching = false;

    // 當前的水位是否符合顯示瓶蓋的條件
    private bool waterLevelOk = false;

    public WaterLevelControl waterLevelControl; // 在此引用 WaterLevelControl 腳本

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

        // 檢查水位標籤是否存在
        waterLevelOk = lowWater.activeInHierarchy && highWater.activeInHierarchy;

        // 當雙手都觸碰且水位符合條件時顯示瓶蓋
        if (leftHandTouching && rightHandTouching && waterLevelOk)
        {
            bottleCap.SetActive(true);
            waterLevelControl.isCapVisible = true; // 當瓶蓋顯示後，允許搖晃動作
        }
    }
}
