using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TissueBoxController : MonoBehaviour
{
    public GameObject tissueBox; // 整盒擦手紙
    public GameObject singleTissue1; // 第一個單張擦手紙
    public GameObject singleTissue2; // 第二個單張擦手紙
    public GameObject conditionObject1; // 第一個條件物件
    public GameObject conditionObject2; // 第二個條件物件
    public SteamVR_Behaviour_Pose leftHandPose; // 左手的Pose
    public SteamVR_Behaviour_Pose rightHandPose; // 右手的Pose
    public Vector3 tissueOffset = new Vector3(0, 0.1f, 0); // 單張擦手紙相對於整盒擦手紙的偏移

    private bool leftHandTouching = false; // 左手是否接觸
    private bool rightHandTouching = false; // 右手是否接觸
    private bool tissue1Shown = false; // 第一個單張擦手紙是否顯示
    private bool tissue2Shown = false; // 第二個單張擦手紙是否顯示
    private bool isGrabbing = false; // 是否抓取擦手紙

    private Rigidbody tissueRigidbody1; // 第一個單張擦手紙的Rigidbody
    private Rigidbody tissueRigidbody2; // 第二個單張擦手紙的Rigidbody

    void Start()
    {
        singleTissue1.SetActive(false); // 初始隱藏第一個單張擦手紙
        singleTissue2.SetActive(false); // 初始隱藏第二個單張擦手紙
        tissueRigidbody1 = singleTissue1.GetComponent<Rigidbody>(); // 獲取第一個單張擦手紙的Rigidbody
        tissueRigidbody2 = singleTissue2.GetComponent<Rigidbody>(); // 獲取第二個單張擦手紙的Rigidbody

        if (tissueRigidbody1 != null)
        {
            tissueRigidbody1.isKinematic = true; // 禁用物理引擎影響
        }

        if (tissueRigidbody2 != null)
        {
            tissueRigidbody2.isKinematic = true; // 禁用物理引擎影響
        }
    }

    void Update()
    {
        // 檢查conditionObject1和conditionObject2是否出現
        if (conditionObject1.activeSelf && !tissue1Shown)
        {
            // 檢查雙手是否觸碰到整盒擦手紙
            if (leftHandPose.transform != null && IsHandTouchingTissueBox(leftHandPose.transform) &&
                rightHandPose.transform != null && IsHandTouchingTissueBox(rightHandPose.transform))
            {
                Debug.Log("Both hands are touching the tissue box for the first tissue.");
                singleTissue1.SetActive(true); // 顯示第一個單張擦手紙
                tissue1Shown = true; // 記錄已經顯示過第一個單張擦手紙

                // 設置第一個單張擦手紙的位置，讓它在整盒擦手紙的上方
                singleTissue1.transform.position = tissueBox.transform.position + tissueOffset;

                // 禁用物理引擎影響，讓擦手紙與手同步移動
                if (tissueRigidbody1 != null)
                {
                    tissueRigidbody1.isKinematic = true;
                }
            }
        }

        if (conditionObject2.activeSelf && !tissue2Shown)
        {
            // 檢查雙手是否觸碰到整盒擦手紙
            if (leftHandPose.transform != null && IsHandTouchingTissueBox(leftHandPose.transform) &&
                rightHandPose.transform != null && IsHandTouchingTissueBox(rightHandPose.transform))
            {
                Debug.Log("Both hands are touching the tissue box for the second tissue.");
                singleTissue2.SetActive(true); // 顯示第二個單張擦手紙
                tissue2Shown = true; // 記錄已經顯示過第二個單張擦手紙

                // 設置第二個單張擦手紙的位置
                singleTissue2.transform.position = tissueBox.transform.position + tissueOffset;

                // 禁用物理引擎影響，讓擦手紙與手同步移動
                if (tissueRigidbody2 != null)
                {
                    tissueRigidbody2.isKinematic = true;
                }
            }
        }

        // 當手抓取時，讓擦手紙跟隨手移動
        if (leftHandTouching || rightHandTouching)
        {
            if (!isGrabbing)
            {
                isGrabbing = true;
                if (tissueRigidbody1 != null)
                    tissueRigidbody1.isKinematic = true; // 禁用物理引擎影響
                if (tissueRigidbody2 != null)
                    tissueRigidbody2.isKinematic = true; // 禁用物理引擎影響
            }

            // 確保擦手紙位置跟隨手的位置
            Vector3 handPosition = leftHandTouching ? leftHandPose.transform.position : rightHandPose.transform.position;
            if (tissue1Shown)
            {
                singleTissue1.transform.position = handPosition;
            }
            if (tissue2Shown)
            {
                singleTissue2.transform.position = handPosition;
            }
        }
        else if (isGrabbing) // 放開手時恢復物理影響
        {
            if (tissueRigidbody1 != null)
                tissueRigidbody1.isKinematic = false; // 恢復物理引擎影響
            if (tissueRigidbody2 != null)
                tissueRigidbody2.isKinematic = false; // 恢復物理引擎影響

            isGrabbing = false;
        }
    }

    // 判斷手是否接觸到"整盒擦手紙"
    bool IsHandTouchingTissueBox(Transform handTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(handTransform.position, 0.1f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == tissueBox)
            {
                return true;
            }
        }
        return false;
    }
}
