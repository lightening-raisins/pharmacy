using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WaterLevelControl : MonoBehaviour
{
    // 低水位物件
    public GameObject lowWater;
    // 高水位物件
    public GameObject highWater;
    // 混合水位物件
    public GameObject mixWaterLevel;
    public GameObject tips1;
    public GameObject tips2;
    public GameObject tips3;    

    private bool isLowWaterTriggered = false; // 是否已觸發低水位
    private bool isHighWaterTriggered = false; // 是否已觸發高水位
    private bool isHideCoroutineStarted = false;
    private string firstTriggeredTag = ""; // 記錄第一個碰到的水柱標籤

    public SteamVR_Input_Sources leftHandInputSource;
    public SteamVR_Input_Sources rightHandInputSource;

    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;
    public SteamVR_Action_Boolean grabAction;

    private Quaternion lastLeftHandRotation;
    private Quaternion lastRightHandRotation;
    private float shakeThreshold = 15.0f;
    private bool isCapHidden = false;

    public GameObject cap; // 瓶蓋物件
    public GameObject waterStream;

    void Start()
    {
        // 確保低水位和高水位物件開始時隱藏
        if (lowWater != null)
        {
            lowWater.SetActive(false);
        }
        if (highWater != null)
        {
            highWater.SetActive(false);
        }
        if (mixWaterLevel != null)
        {
            mixWaterLevel.SetActive(false);
        }

        // 記錄初始旋轉角度
        lastLeftHandRotation = leftHandPose.transform.rotation;
        lastRightHandRotation = rightHandPose.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 如果低水位還沒觸發，並且碰到的是任意水柱
        if (!isLowWaterTriggered && (other.CompareTag("WaterStream") || other.CompareTag("BleachStream")))
        {
            // 顯示低水位物件
            if (lowWater != null)
            {
                lowWater.SetActive(true);
                if (tips1 != null) tips1.SetActive(true);
                isLowWaterTriggered = true; // 標記低水位已觸發
                firstTriggeredTag = other.tag; // 記錄第一個碰到的水柱類型
            }
        }
        // 如果低水位已觸發但高水位還沒觸發，並且碰到的是另一種水柱
        else if (isLowWaterTriggered && !isHighWaterTriggered)
        {
            // 判斷碰到的是否為與第一個不同的水柱
            if ((other.CompareTag("WaterStream") || other.CompareTag("BleachStream")) && other.tag != firstTriggeredTag)
            {
                // 顯示高水位物件
                if (highWater != null)
                {
                    highWater.SetActive(true);
                    if (tips2 != null) tips2.SetActive(true);
                    isHighWaterTriggered = true; // 標記高水位已觸發
                }
            }
        }
    }

    public bool isCapVisible = false; // 用來檢查瓶蓋是否已顯示
    private bool isShakeComplete = false; // 用來檢查搖晃是否完成

    void Update()
    {
        // 取得左右手控制器的當前旋轉
        Quaternion currentLeftHandRotation = leftHandPose.transform.rotation;
        Quaternion currentRightHandRotation = rightHandPose.transform.rotation;

        // 計算旋轉角度變化量
        float leftHandRotationChange = Quaternion.Angle(currentLeftHandRotation, lastLeftHandRotation);
        float rightHandRotationChange = Quaternion.Angle(currentRightHandRotation, lastRightHandRotation);

        // 檢查搖晃條件
        if (isCapVisible && !isShakeComplete)
        {
            if (leftHandRotationChange > shakeThreshold || rightHandRotationChange > shakeThreshold)
            {
                if (isLowWaterTriggered && isHighWaterTriggered)
                {
                    lowWater.SetActive(false);
                    highWater.SetActive(false);
                    mixWaterLevel.SetActive(true);
                    if (tips3 != null) tips3.SetActive(true);
                    isShakeComplete = true;
                }
            }
        }

        if (waterStream.activeSelf && (!mixWaterLevel.activeSelf || !isCapHidden))
        {
            waterStream.SetActive(false); // 避免誤觸導致水柱提前出現
        }

        lastLeftHandRotation = currentLeftHandRotation;
        lastRightHandRotation = currentRightHandRotation;

        // 檢查瓶蓋隱藏條件
        if (isShakeComplete && isCapVisible && !isCapHidden && !isHideCoroutineStarted)
        {
            if (BothHandsInteractingWithCap())
            {
                isHideCoroutineStarted = true; // 防止重複進入協程
                StartCoroutine(HideCapWithDelay());
            }
        }
    }

    // 延遲3秒隱藏瓶蓋
    private IEnumerator HideCapWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // 延遲
        cap.SetActive(false); // 隱藏瓶蓋
        isCapHidden = true;  // 防止重複隱藏

        // 只有在 mixWaterLevel 已經顯示的狀態下，才啟用水柱
        if (mixWaterLevel != null && mixWaterLevel.activeSelf)
        {
            waterStream.SetActive(true); // 啟用水流
        }
    }

    // 檢查是否兩隻手接觸瓶蓋並正確操作
    private bool BothHandsInteractingWithCap()
    {
        bool isLeftHandTouching = IsHandTouchingCap(leftHandPose);
        bool isRightHandTouching = IsHandTouchingCap(rightHandPose);

        // 確保兩隻手的狀態符合條件
        bool isLeftHandGrabbing = grabAction.GetState(leftHandInputSource);
        bool isRightHandGrabbing = grabAction.GetState(rightHandInputSource);

        // 一隻手抓取瓶子，另一隻手接觸瓶蓋
        return (isLeftHandGrabbing && isRightHandTouching) || (isRightHandGrabbing && isLeftHandTouching);
    }

    // 檢查指定的手是否接觸瓶蓋
    private bool IsHandTouchingCap(SteamVR_Behaviour_Pose handPose)
    {
        Collider[] colliders = Physics.OverlapSphere(cap.transform.position, 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("hand") && collider.gameObject == handPose.gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
