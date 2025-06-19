using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CapHidden : MonoBehaviour
{
    public GameObject bottle;
    public GameObject cap;
    public GameObject water;
    public GameObject triggerObject; // �s�W�� triggerObject
    public GameObject tips;

    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;

    private bool isBottleHeld = false;       // �~�l�O�_�Q���
    private GameObject grabbingHand = null;  // ���~�l����]����Υk��^
    private bool isCapHidden = false;        // �~�\�O�_�w�g�Q����
    private bool hasTriggerObjectActivated = false; // triggerObject �O�_��ܹL�@��

    private void Update()
    {
        // �p�G triggerObject ��ܹL�@���A�аO���w�Ұ�
        if (!hasTriggerObjectActivated && triggerObject.activeSelf)
        {
            hasTriggerObjectActivated = true;
        }

        // �u���� triggerObject ��ܹL�@����A�~�i��~�l����޿�
        if (hasTriggerObjectActivated)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        // �u���b triggerObject ��ܹL�@���B�~�l�Q������p�U�~�ˬd�����޿�
        if (hasTriggerObjectActivated && isBottleHeld && !isCapHidden && other.CompareTag("hand") && other.gameObject != grabbingHand)
        {
            cap.SetActive(false);
            if (tips != null) tips.SetActive(true);
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
