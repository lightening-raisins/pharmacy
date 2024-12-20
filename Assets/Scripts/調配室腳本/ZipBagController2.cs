using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ZipBagController2 : MonoBehaviour
{
    [Header("物件參考")]
    public GameObject wholeZiplockBag;   // 整袋夾鏈袋物件
    public GameObject singleZiplockBag;  // 單個夾鏈袋物件
    public GameObject triggerObject;     // 控制是否開始檢查觸碰的物件（例如：一個特定的物件）

    [Header("手部 Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;  // 左手 Pose
    public SteamVR_Behaviour_Pose rightHandPose; // 右手 Pose

    [Header("單個夾鏈袋位置偏移")]
    public Vector3 ziplockOffset = new Vector3(0, 0.1f, 0); // 單個夾鏈袋的偏移量

    private bool leftHandTouching = false; // 左手是否接觸
    private bool rightHandTouching = false; // 右手是否接觸
    private bool ziplockBagShown = false; // 是否顯示單個夾鏈袋
    private bool isTriggerObjectActive = false; // 控制是否開始檢查觸碰的變數

    void Start()
    {
        singleZiplockBag.SetActive(false); // 初始隱藏單個夾鏈袋
        isTriggerObjectActive = false;     // 初始不開始檢查觸碰
    }

    void Update()
    {
        // 只有在triggerObject出現後，才開始檢查手部是否接觸夾鏈袋
        if (triggerObject.activeSelf && !isTriggerObjectActive)
        {
            isTriggerObjectActive = true; // 開始檢查觸碰
        }

        // 如果開始檢查觸碰
        if (isTriggerObjectActive)
        {
            // 偵測左手是否接觸整袋夾鏈袋
            leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);

            // 偵測右手是否接觸整袋夾鏈袋
            rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

            // 只有當兩隻手都觸碰到整袋夾鏈袋時，顯示單個夾鏈袋
            if (leftHandTouching && rightHandTouching && !ziplockBagShown)
            {
                ShowSingleZiplockBag();
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
    void ShowSingleZiplockBag()
    {
        singleZiplockBag.SetActive(true);
        singleZiplockBag.transform.position = wholeZiplockBag.transform.position + ziplockOffset;
        ziplockBagShown = true;

        Debug.Log("單個夾鏈袋出現！");
    }
}
