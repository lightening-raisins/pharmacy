using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SterileGloveController : MonoBehaviour
{
    [Header("����Ѧ�")]
    public GameObject wholeGloveBag;   // ��U���ߤ�M
    public GameObject glove;           // ��@��M
    public GameObject tips;            // ���ܪ���]�i��^

    [Header("Ĳ�o���󪫥�")]
    public GameObject triggerConditionObject; // �u���o�Ӫ���Ұʫ�~���\Ĳ�o

    [Header("�ⳡ Pose")]
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
        // ���󪫥󥼱ҥδN���~��
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

        Debug.Log("���ߤ�M�X�{�I");
    }
}
