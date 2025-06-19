using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CBRNSuitController : MonoBehaviour
{
    [Header("����Ѧ�")]
    public GameObject wholeCBRNSuit; 
    public GameObject suit;
    public GameObject tips;

    [Header("�ⳡ Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;  // ���� Pose
    public SteamVR_Behaviour_Pose rightHandPose; // �k�� Pose

    private bool leftHandTouching = false;  // ����O�_��Ĳ
    private bool rightHandTouching = false; // �k��O�_��Ĳ
    private bool suitShown = false;         // �f�n�O�_�w���

    void Start()
    {
        // �@�}�l���äf�n
        suit.SetActive(false);
    }

    void Update()
    {
        // �ˬd����O�_��Ĳ
        leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);

        // �ˬd�k��O�_��Ĳ
        rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

        // ��Ⱖ�ⳣĲ�I�짨��U�B�f�n�٨S�X�{�L
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
        Vector3 spawnPosition = wholeCBRNSuit.transform.position + new Vector3(0, 0.25f, 0.3f); // �i�̻ݨD�վ㰾���q
        suit.transform.position = spawnPosition;

        suit.SetActive(true);
        if (tips != null) tips.SetActive(true);
        suitShown = true;
        Debug.Log("���@�A�X�{�I");
    }

}
