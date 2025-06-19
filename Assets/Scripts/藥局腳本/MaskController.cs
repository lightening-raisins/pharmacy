using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MaskController : MonoBehaviour
{
    [Header("物件參考")]
    public GameObject wholeMaskBox; // 整袋夾鏈袋（父層）
    public GameObject mask;            // 口罩模型（子物件）
    public GameObject tips;

    [Header("手部 Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;  // 左手 Pose
    public SteamVR_Behaviour_Pose rightHandPose; // 右手 Pose

    private bool leftHandTouching = false;  // 左手是否接觸
    private bool rightHandTouching = false; // 右手是否接觸
    private bool maskShown = false;         // 口罩是否已顯示

    void Start()
    {
        // 一開始隱藏口罩
        mask.SetActive(false);
    }

    void Update()
    {
        // 檢查左手是否接觸
        leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);

        // 檢查右手是否接觸
        rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

        // 當兩隻手都觸碰到夾鏈袋且口罩還沒出現過
        if (leftHandTouching && rightHandTouching && !maskShown)
        {
            ShowMask();
        }
    }

    bool IsHandTouchingZipBag(Transform handTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(handTransform.position, 0.1f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == wholeMaskBox)
            {
                return true;
            }
        }
        return false;
    }

    void ShowMask()
    {
        Vector3 spawnPosition = wholeMaskBox.transform.position + new Vector3(0, 0.2f, 0); // 可調整偏移量
        mask.transform.position = spawnPosition;

        mask.SetActive(true);
        tips.SetActive(true);
        maskShown = true;
        Debug.Log("口罩出現！");
    }

}
