using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WaterLevelControl : MonoBehaviour
{
    // �C���쪫��
    public GameObject lowWater;
    // �����쪫��
    public GameObject highWater;
    // �V�X���쪫��
    public GameObject mixWaterLevel;
    public GameObject tips1;
    public GameObject tips2;
    public GameObject tips3;    

    private bool isLowWaterTriggered = false; // �O�_�wĲ�o�C����
    private bool isHighWaterTriggered = false; // �O�_�wĲ�o������
    private bool isHideCoroutineStarted = false;
    private string firstTriggeredTag = ""; // �O���Ĥ@�ӸI�쪺���W����

    public SteamVR_Input_Sources leftHandInputSource;
    public SteamVR_Input_Sources rightHandInputSource;

    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;
    public SteamVR_Action_Boolean grabAction;

    private Quaternion lastLeftHandRotation;
    private Quaternion lastRightHandRotation;
    private float shakeThreshold = 15.0f;
    private bool isCapHidden = false;

    public GameObject cap; // �~�\����
    public GameObject waterStream;

    void Start()
    {
        // �T�O�C����M�����쪫��}�l������
        if (lowWater != null)
        {
            lowWater.SetActive(false);
        }
        if (highWater != null)
        {
            highWater.SetActive(false);
        }
        if (mixWaterLevel != null)
        {
            mixWaterLevel.SetActive(false);
        }

        // �O����l���ਤ��
        lastLeftHandRotation = leftHandPose.transform.rotation;
        lastRightHandRotation = rightHandPose.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �p�G�C�����٨SĲ�o�A�åB�I�쪺�O���N���W
        if (!isLowWaterTriggered && (other.CompareTag("WaterStream") || other.CompareTag("BleachStream")))
        {
            // ��ܧC���쪫��
            if (lowWater != null)
            {
                lowWater.SetActive(true);
                if (tips1 != null) tips1.SetActive(true);
                isLowWaterTriggered = true; // �аO�C����wĲ�o
                firstTriggeredTag = other.tag; // �O���Ĥ@�ӸI�쪺���W����
            }
        }
        // �p�G�C����wĲ�o���������٨SĲ�o�A�åB�I�쪺�O�t�@�ؤ��W
        else if (isLowWaterTriggered && !isHighWaterTriggered)
        {
            // �P�_�I�쪺�O�_���P�Ĥ@�Ӥ��P�����W
            if ((other.CompareTag("WaterStream") || other.CompareTag("BleachStream")) && other.tag != firstTriggeredTag)
            {
                // ��ܰ����쪫��
                if (highWater != null)
                {
                    highWater.SetActive(true);
                    if (tips2 != null) tips2.SetActive(true);
                    isHighWaterTriggered = true; // �аO������wĲ�o
                }
            }
        }
    }

    public bool isCapVisible = false; // �Ψ��ˬd�~�\�O�_�w���
    private bool isShakeComplete = false; // �Ψ��ˬd�n�̬O�_����

    void Update()
    {
        // ���o���k�ⱱ�����e����
        Quaternion currentLeftHandRotation = leftHandPose.transform.rotation;
        Quaternion currentRightHandRotation = rightHandPose.transform.rotation;

        // �p����ਤ���ܤƶq
        float leftHandRotationChange = Quaternion.Angle(currentLeftHandRotation, lastLeftHandRotation);
        float rightHandRotationChange = Quaternion.Angle(currentRightHandRotation, lastRightHandRotation);

        // �ˬd�n�̱���
        if (isCapVisible && !isShakeComplete)
        {
            if (leftHandRotationChange > shakeThreshold || rightHandRotationChange > shakeThreshold)
            {
                if (isLowWaterTriggered && isHighWaterTriggered)
                {
                    lowWater.SetActive(false);
                    highWater.SetActive(false);
                    mixWaterLevel.SetActive(true);
                    if (tips3 != null) tips3.SetActive(true);
                    isShakeComplete = true;
                }
            }
        }

        if (waterStream.activeSelf && (!mixWaterLevel.activeSelf || !isCapHidden))
        {
            waterStream.SetActive(false); // �קK�~Ĳ�ɭP���W���e�X�{
        }

        lastLeftHandRotation = currentLeftHandRotation;
        lastRightHandRotation = currentRightHandRotation;

        // �ˬd�~�\���ñ���
        if (isShakeComplete && isCapVisible && !isCapHidden && !isHideCoroutineStarted)
        {
            if (BothHandsInteractingWithCap())
            {
                isHideCoroutineStarted = true; // ����ƶi�J��{
                StartCoroutine(HideCapWithDelay());
            }
        }
    }

    // ����3�����ò~�\
    private IEnumerator HideCapWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // ����
        cap.SetActive(false); // ���ò~�\
        isCapHidden = true;  // ���������

        // �u���b mixWaterLevel �w�g��ܪ����A�U�A�~�ҥΤ��W
        if (mixWaterLevel != null && mixWaterLevel.activeSelf)
        {
            waterStream.SetActive(true); // �ҥΤ��y
        }
    }

    // �ˬd�O�_�Ⱖ�ⱵĲ�~�\�å��T�ާ@
    private bool BothHandsInteractingWithCap()
    {
        bool isLeftHandTouching = IsHandTouchingCap(leftHandPose);
        bool isRightHandTouching = IsHandTouchingCap(rightHandPose);

        // �T�O�Ⱖ�⪺���A�ŦX����
        bool isLeftHandGrabbing = grabAction.GetState(leftHandInputSource);
        bool isRightHandGrabbing = grabAction.GetState(rightHandInputSource);

        // �@�������~�l�A�t�@���ⱵĲ�~�\
        return (isLeftHandGrabbing && isRightHandTouching) || (isRightHandGrabbing && isLeftHandTouching);
    }

    // �ˬd���w����O�_��Ĳ�~�\
    private bool IsHandTouchingCap(SteamVR_Behaviour_Pose handPose)
    {
        Collider[] colliders = Physics.OverlapSphere(cap.transform.position, 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("hand") && collider.gameObject == handPose.gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
