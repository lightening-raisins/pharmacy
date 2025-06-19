using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SterileGloveController : MonoBehaviour
{
    [Header("物件參考")]
    public GameObject wholeGloveBag;   // 整袋滅菌手套
    public GameObject glove;           // 單一手套
    public GameObject tips;            // 提示物件（可選）

    [Header("觸發條件物件")]
    public GameObject triggerConditionObject; // 只有這個物件啟動後才允許觸發

    [Header("手部 Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;

    private bool leftHandTouching = false;
    private bool rightHandTouching = false;
    private bool gloveShown = false;

    void Start()
    {
        glove.SetActive(false);
        if (tips != null) tips.SetActive(false);
    }

    void Update()
    {
        // 條件物件未啟用就不繼續
        if (triggerConditionObject == null || !triggerConditionObject.activeSelf)
            return;

        leftHandTouching = IsHandTouchingGloveBag(leftHandPose.transform);
        rightHandTouching = IsHandTouchingGloveBag(rightHandPose.transform);

        if (leftHandTouching && rightHandTouching && !gloveShown)
        {
            ShowGlove();
        }
    }

    bool IsHandTouchingGloveBag(Transform handTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(handTransform.position, 0.1f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == wholeGloveBag)
                return true;
        }
        return false;
    }

    void ShowGlove()
    {
        Vector3 spawnPosition = wholeGloveBag.transform.position + new Vector3(0, 0.2f, 0);
        glove.transform.position = spawnPosition;

        glove.SetActive(true);
        if (tips != null) tips.SetActive(true);
        gloveShown = true;

        Debug.Log("滅菌手套出現！");
    }
}
