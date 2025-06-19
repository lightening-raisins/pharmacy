using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CapHidden : MonoBehaviour
{
    public GameObject bottle;
    public GameObject cap;
    public GameObject water;
    public GameObject triggerObject; // 新增的 triggerObject
    public GameObject tips;

    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;

    private bool isBottleHeld = false;       // 瓶子是否被抓取
    private GameObject grabbingHand = null;  // 抓住瓶子的手（左手或右手）
    private bool isCapHidden = false;        // 瓶蓋是否已經被隱藏
    private bool hasTriggerObjectActivated = false; // triggerObject 是否顯示過一次

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
            if (tips != null) tips.SetActive(true);
            isCapHidden = true; // 標記瓶蓋已經被隱藏
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
    }
}
