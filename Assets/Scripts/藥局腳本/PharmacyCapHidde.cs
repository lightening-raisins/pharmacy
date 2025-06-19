using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PharmacyCapHidde : MonoBehaviour
{
    public GameObject bottle;
    public GameObject cap;
    public GameObject triggerObject; // 新增的 triggerObject
    public GameObject hintDisplay;
    public GameObject tips1;
    public GameObject tips2;

    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;

    // 新增的變數
    public GameObject objectToHideAfterDustpan; // 被畚箕碰後要隱藏的物件

    private bool isBottleHeld = false;       // 瓶子是否被抓取
    private GameObject grabbingHand = null;  // 抓住瓶子的手（左手或右手）
    private bool isCapHidden = false;        // 瓶蓋是否已經被隱藏
    private bool hasTriggerObjectActivated = false; // triggerObject 是否顯示過一次

    // 新增的狀態追蹤
    private bool dustpanTriggered = false;
    private bool leftHandTouchingBottle = false;
    private bool rightHandTouchingBottle = false;

    private void Update()
    {
        // 如果 triggerObject 顯示過一次，標記為已啟動
        if (!hasTriggerObjectActivated && triggerObject.activeSelf)
        {
            hasTriggerObjectActivated = true;
        }

        // 只有當 triggerObject 顯示過一次後，才進行瓶子抓取邏輯
        if (hasTriggerObjectActivated)
        {
            // 檢測抓取按鈕狀態（左手或右手）
            bool isLeftHandGrabbing = grabAction.GetState(leftHandPose.inputSource);
            bool isRightHandGrabbing = grabAction.GetState(rightHandPose.inputSource);

            // 確認瓶子是否被抓住並記錄抓住的手
            if (isLeftHandGrabbing)
            {
                isBottleHeld = true;
                grabbingHand = leftHandPose.gameObject; // 設定左手為抓取手
            }
            else if (isRightHandGrabbing)
            {
                isBottleHeld = true;
                grabbingHand = rightHandPose.gameObject; // 設定右手為抓取手
            }
            else
            {
                isBottleHeld = false;
                grabbingHand = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 只有在 triggerObject 顯示過一次且瓶子被抓住的情況下才檢查隱藏邏輯
        if (hasTriggerObjectActivated && isBottleHeld && !isCapHidden && other.CompareTag("hand") && other.gameObject != grabbingHand)
        {
            cap.SetActive(false);
            hintDisplay.SetActive(true);
            if (tips1 != null) tips1.SetActive(true);
            isCapHidden = true; // 標記瓶蓋已經被隱藏
        }

        // 紀錄左手右手是否有碰到瓶身
        if (other.CompareTag("hand"))
        {
            if (other.gameObject == leftHandPose.gameObject)
                leftHandTouchingBottle = true;
            if (other.gameObject == rightHandPose.gameObject)
                rightHandTouchingBottle = true;

            // Dustpan 已碰過 && 雙手都碰到瓶身 => 蓋回瓶蓋（只觸發一次）
            if (dustpanTriggered && leftHandTouchingBottle && rightHandTouchingBottle && !cap.activeSelf)
            {
                cap.SetActive(true);
                if (tips2 != null) tips2.SetActive(true);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        // 如果瓶蓋已經隱藏，不再恢復瓶蓋
        if (isCapHidden)
        {
            return;
        }

        // 當手離開瓶蓋後恢復瓶蓋（只有在瓶蓋未被永久隱藏的情況下）
        if (other.CompareTag("hand"))
        {
            cap.SetActive(true);
        }

        // 清除手離開的狀態
        if (other.CompareTag("hand"))
        {
            if (other.gameObject == leftHandPose.gameObject)
                leftHandTouchingBottle = false;
            if (other.gameObject == rightHandPose.gameObject)
                rightHandTouchingBottle = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 瓶蓋打開後遇到 Dustpan => 隱藏指定物件
        if (isCapHidden && collision.gameObject.CompareTag("Dustpan") && !dustpanTriggered)
        {
            if (objectToHideAfterDustpan != null)
            {
                objectToHideAfterDustpan.SetActive(false);
                dustpanTriggered = true;
            }
        }
    }
}
