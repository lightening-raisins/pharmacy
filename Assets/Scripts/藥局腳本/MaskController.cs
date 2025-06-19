using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MaskController : MonoBehaviour
{
    [Header("����Ѧ�")]
    public GameObject wholeMaskBox; // ��U����U�]���h�^
    public GameObject mask;            // �f�n�ҫ��]�l����^
    public GameObject tips;

    [Header("�ⳡ Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;  // ���� Pose
    public SteamVR_Behaviour_Pose rightHandPose; // �k�� Pose

    private bool leftHandTouching = false;  // ����O�_��Ĳ
    private bool rightHandTouching = false; // �k��O�_��Ĳ
    private bool maskShown = false;         // �f�n�O�_�w���

    void Start()
    {
        // �@�}�l���äf�n
        mask.SetActive(false);
    }

    void Update()
    {
        // �ˬd����O�_��Ĳ
        leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);

        // �ˬd�k��O�_��Ĳ
        rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

        // ��Ⱖ�ⳣĲ�I�짨��U�B�f�n�٨S�X�{�L
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
        Vector3 spawnPosition = wholeMaskBox.transform.position + new Vector3(0, 0.2f, 0); // �i�վ㰾���q
        mask.transform.position = spawnPosition;

        mask.SetActive(true);
        tips.SetActive(true);
        maskShown = true;
        Debug.Log("�f�n�X�{�I");
    }

}
