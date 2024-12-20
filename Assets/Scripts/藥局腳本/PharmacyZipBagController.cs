using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PharmacyZipBagController : MonoBehaviour
{
    [Header("物件參考")]
    public GameObject wholeZiplockBag;   // 整袋夾鏈袋物件
    public GameObject singleZiplockBag1;  // 第一個單個夾鏈袋物件
    public GameObject singleZiplockBag2;  // 第二個單個夾鏈袋物件

    [Header("條件物件")]
    public GameObject conditionObject1; // 第一個條件物件
    public GameObject conditionObject2; // 第二個條件物件


    [Header("手部 Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;  // 左手 Pose
    public SteamVR_Behaviour_Pose rightHandPose; // 右手 Pose

    [Header("單個夾鏈袋位置偏移")]
    public Vector3 ziplockOffset = new Vector3(0, 0.1f, 0); // 單個夾鏈袋的偏移量

    private bool leftHandTouching = false; // 左手是否接觸
    private bool rightHandTouching = false; // 右手是否接觸
    private bool ziplockBag1Shown = false; // 第一個單個夾鏈袋是否顯示
    private bool ziplockBag2Shown = false; // 第二個單個夾鏈袋是否顯示

    void Start()
    {
        // 初始隱藏所有單個夾鏈袋
        singleZiplockBag1.SetActive(false);
        singleZiplockBag2.SetActive(false);
    }

    void Update()
    {
        // 偵測左手是否接觸整袋夾鏈袋
        leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);

        // 偵測右手是否接觸整袋夾鏈袋
        rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

        // 只有當兩隻手都觸碰到整袋夾鏈袋時，顯示單個夾鏈袋
        if (leftHandTouching && rightHandTouching)
        {
            if (conditionObject1.activeSelf && !ziplockBag1Shown)
            {
                ShowSingleZiplockBag(singleZiplockBag1);
                ziplockBag1Shown = true;
            }
            else if (conditionObject2.activeSelf && !ziplockBag2Shown)
            {
                ShowSingleZiplockBag(singleZiplockBag2);
                ziplockBag2Shown = true;
            }
        }
    }

    /// <summary>
    /// 檢測手是否接觸整袋夾鏈袋
    /// </summary>
    bool IsHandTouchingZipBag(Transform handTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(handTransform.position, 0.1f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == wholeZiplockBag)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 顯示單個夾鏈袋並設定位置
    /// </summary>
    void ShowSingleZiplockBag(GameObject ziplockBag)
    {
        ziplockBag.SetActive(true);
        ziplockBag.transform.position = wholeZiplockBag.transform.position + ziplockOffset;
        Debug.Log(ziplockBag.name + " 出現！");
    }
}
