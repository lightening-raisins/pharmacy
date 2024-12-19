using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TissueBoxController2 : MonoBehaviour
{
    public GameObject tissueBox; // 整盒擦手紙
    public GameObject singleTissue2; // 單張擦手紙
    public SteamVR_Behaviour_Pose leftHandPose; // 左手的Pose
    public SteamVR_Behaviour_Pose rightHandPose; // 右手的Pose
    public Vector3 tissueOffset = new Vector3(0, 0.1f, 0); // 單張擦手紙相對於整盒擦手紙的偏移

    private bool leftHandTouching = false; // 左手是否接觸
    private bool rightHandTouching = false; // 右手是否接觸
    private bool tissueShown = false; // 單張擦手紙是否顯示
    private bool isGrabbing = false; // 是否抓取擦手紙

    private Rigidbody tissueRigidbody; // 單張擦手紙的Rigidbody

    void Start()
    {
        singleTissue2.SetActive(false); // 初始隱藏單張擦手紙
        tissueRigidbody = singleTissue2.GetComponent<Rigidbody>(); // 獲取Rigidbody
        if (tissueRigidbody != null)
        {
            tissueRigidbody.isKinematic = true; // 禁用物理引擎影響
        }
    }

    void Update()
    {
        // 檢查左手是否接觸到整盒擦手紙
        if (leftHandPose.transform != null && IsHandTouchingTissueBox(leftHandPose.transform))
        {
            leftHandTouching = true;
        }
        else
        {
            leftHandTouching = false;
        }

        // 檢查右手是否接觸到整盒擦手紙
        if (rightHandPose.transform != null && IsHandTouchingTissueBox(rightHandPose.transform))
        {
            rightHandTouching = true;
        }
        else
        {
            rightHandTouching = false;
        }

        // 只有當兩隻手都觸碰到整盒擦手紙時，顯示單張擦手紙
        if (leftHandTouching && rightHandTouching && !tissueShown)
        {
            Debug.Log("Both hands are touching the tissue box.");
            singleTissue2.SetActive(true);
            tissueShown = true; // 記錄已經顯示過單張擦手紙

            // 設置單張擦手紙的位置，讓它在整盒擦手紙的上方
            singleTissue2.transform.position = tissueBox.transform.position + tissueOffset;

            // 禁用物理引擎影響，讓擦手紙與手同步移動
            if (tissueRigidbody != null)
            {
                tissueRigidbody.isKinematic = true; // 禁用物理引擎影響
            }
        }

        // 當手抓取時，讓擦手紙跟隨手移動
        if (leftHandTouching || rightHandTouching)
        {
            if (!isGrabbing)
            {
                isGrabbing = true;
                tissueRigidbody.isKinematic = true; // 禁用物理引擎影響
            }

            // 確保擦手紙位置跟隨手的位置
            Vector3 handPosition = leftHandTouching ? leftHandPose.transform.position : rightHandPose.transform.position;
            singleTissue2.transform.position = handPosition;

        }
        else if (isGrabbing) // 放開手時恢復物理影響
        {
            if (tissueRigidbody != null)
            {
                tissueRigidbody.isKinematic = false; // 恢復物理引擎影響
            }
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
