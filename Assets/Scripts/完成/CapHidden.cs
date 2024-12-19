using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CapHidden : MonoBehaviour
{
    public GameObject bottle;
    public GameObject cap;
    public GameObject water;

    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;

    private bool isBottleHeld = false;       // �~�l�O�_�Q���
    private GameObject grabbingHand = null;  // ���~�l����]����Υk��^
    private bool isCapHidden = false;        // �~�\�O�_�w�g�Q����

    private void Update()
    {
        // �˴�������s���A�]����Υk��^
        bool isLeftHandGrabbing = grabAction.GetState(leftHandPose.inputSource);
        bool isRightHandGrabbing = grabAction.GetState(rightHandPose.inputSource);

        // �T�{�~�l�O�_�Q���ðO�������
        if (isLeftHandGrabbing)
        {
            isBottleHeld = true;
            grabbingHand = leftHandPose.gameObject; // �]�w���⬰�����
        }
        else if (isRightHandGrabbing)
        {
            isBottleHeld = true;
            grabbingHand = rightHandPose.gameObject; // �]�w�k�⬰�����
        }
        else
        {
            isBottleHeld = false;
            grabbingHand = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ��~�l�Q���A�BĲ�I�~�\�����O���~�l����A���ò~�\
        if (isBottleHeld && !isCapHidden && other.CompareTag("hand") && other.gameObject != grabbingHand)
        {
            cap.SetActive(false);
            isCapHidden = true; // �аO�~�\�w�g�Q����
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �p�G�~�\�w�g���áA���A��_�~�\
        if (isCapHidden)
        {
            return;
        }

        // ������}�~�\���_�~�\�]�u���b�~�\���Q�ä[���ê����p�U�^
        if (other.CompareTag("hand"))
        {
            cap.SetActive(true);
        }
    }
}