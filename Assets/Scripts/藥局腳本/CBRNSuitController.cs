using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CBRNSuitController : MonoBehaviour
{
    [Header("物件參考")]
    public GameObject wholeCBRNSuit; 
    public GameObject suit;
    public GameObject tips;

    [Header("手部 Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;  // 左手 Pose
    public SteamVR_Behaviour_Pose rightHandPose; // 右手 Pose

    private bool leftHandTouching = false;  // 左手是否接觸
    private bool rightHandTouching = false; // 右手是否接觸
    private bool suitShown = false;         // 口罩是否已顯示

    void Start()
    {
        // 一開始隱藏口罩
        suit.SetActive(false);
    }

    void Update()
    {
        // 檢查左手是否接觸
        leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);

        // 檢查右手是否接觸
        rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

        // 當兩隻手都觸碰到夾鏈袋且口罩還沒出現過
        if (leftHandTouching && rightHandTouching && !suitShown)
        {
            ShowSuit();
        }
    }

    bool IsHandTouchingZipBag(Transform handTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(handTransform.position, 0.1f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == wholeCBRNSuit)
            {
                return true;
            }
        }
        return false;
    }

    void ShowSuit()
    {
        Vector3 spawnPosition = wholeCBRNSuit.transform.position + new Vector3(0, 0.25f, 0.3f); // 可依需求調整偏移量
        suit.transform.position = spawnPosition;

        suit.SetActive(true);
        if (tips != null) tips.SetActive(true);
        suitShown = true;
        Debug.Log("防護服出現！");
    }

}
