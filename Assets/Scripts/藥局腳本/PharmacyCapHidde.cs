using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PharmacyCapHidde : MonoBehaviour
{
    public GameObject bottle;
    public GameObject cap;
    public GameObject triggerObject; // �s�W�� triggerObject
    public GameObject hintDisplay;
    public GameObject tips1;
    public GameObject tips2;

    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;

    // �s�W���ܼ�
    public GameObject objectToHideAfterDustpan; // �Q�c�߸I��n���ê�����

    private bool isBottleHeld = false;       // �~�l�O�_�Q���
    private GameObject grabbingHand = null;  // ���~�l����]����Υk��^
    private bool isCapHidden = false;        // �~�\�O�_�w�g�Q����
    private bool hasTriggerObjectActivated = false; // triggerObject �O�_��ܹL�@��

    // �s�W�����A�l��
    private bool dustpanTriggered = false;
    private bool leftHandTouchingBottle = false;
    private bool rightHandTouchingBottle = false;

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
            hintDisplay.SetActive(true);
            if (tips1 != null) tips1.SetActive(true);
            isCapHidden = true; // �аO�~�\�w�g�Q����
        }

        // ��������k��O�_���I��~��
        if (other.CompareTag("hand"))
        {
            if (other.gameObject == leftHandPose.gameObject)
                leftHandTouchingBottle = true;
            if (other.gameObject == rightHandPose.gameObject)
                rightHandTouchingBottle = true;

            // Dustpan �w�I�L && ���ⳣ�I��~�� => �\�^�~�\�]�uĲ�o�@���^
            if (dustpanTriggered && leftHandTouchingBottle && rightHandTouchingBottle && !cap.activeSelf)
            {
                cap.SetActive(true);
                if (tips2 != null) tips2.SetActive(true);
            }
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

        // �M�������}�����A
        if (other.CompareTag("hand"))
        {
            if (other.gameObject == leftHandPose.gameObject)
                leftHandTouchingBottle = false;
            if (other.gameObject == rightHandPose.gameObject)
                rightHandTouchingBottle = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �~�\���}��J�� Dustpan => ���ë��w����
        if (isCapHidden && collision.gameObject.CompareTag("Dustpan") && !dustpanTriggered)
        {
            if (objectToHideAfterDustpan != null)
            {
                objectToHideAfterDustpan.SetActive(false);
                dustpanTriggered = true;
            }
        }
    }
}
