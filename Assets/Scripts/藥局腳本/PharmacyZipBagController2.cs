using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PharmacyZipBagController2 : MonoBehaviour
{
    [Header("物件參考")]
    public GameObject wholeZiplockBag;     // 整袋夾鏈袋
    public GameObject singleZiplockBag1;   // 第一個單個夾鏈袋
    public GameObject singleZiplockBag2;   // 第二個單個夾鏈袋
    public GameObject singleZiplockBag3;   // 第三個單個夾鏈袋

    [Header("條件物件")]
    public GameObject conditionObject1;
    public GameObject conditionObject2;
    public GameObject conditionObject3;    // 第三個條件物件

    [Header("提示文字")]
    public GameObject tips1;
    public GameObject tips2;
    public GameObject tips3;               // 第三個提示

    [Header("手部 Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;

    [Header("單個夾鏈袋位置偏移")]
    public Vector3 ziplockOffset = new Vector3(0, 0.1f, 0);

    private bool leftHandTouching = false;
    private bool rightHandTouching = false;

    private bool ziplockBag1Shown = false;
    private bool ziplockBag2Shown = false;
    private bool ziplockBag3Shown = false; // 第三個顯示判斷

    void Start()
    {
        singleZiplockBag1.SetActive(false);
        singleZiplockBag2.SetActive(false);
        singleZiplockBag3.SetActive(false); // 第三個初始化隱藏
    }

    void Update()
    {
        leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);
        rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

        if (leftHandTouching && rightHandTouching)
        {
            if (conditionObject1.activeSelf && !ziplockBag1Shown)
            {
                ShowSingleZiplockBag(singleZiplockBag1);
                ziplockBag1Shown = true;
                if (tips1 != null) tips1.SetActive(true);
            }
            else if (conditionObject2.activeSelf && !ziplockBag2Shown)
            {
                ShowSingleZiplockBag(singleZiplockBag2);
                ziplockBag2Shown = true;
                if (tips2 != null) tips2.SetActive(true);
            }
            else if (conditionObject3.activeSelf && !ziplockBag3Shown)
            {
                ShowSingleZiplockBag(singleZiplockBag3);
                ziplockBag3Shown = true;
                if (tips3 != null) tips3.SetActive(true);
            }
        }
    }

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

    void ShowSingleZiplockBag(GameObject ziplockBag)
    {
        ziplockBag.SetActive(true);
        ziplockBag.transform.position = wholeZiplockBag.transform.position + ziplockOffset;
        Debug.Log(ziplockBag.name + " 出現！");
    }
}
