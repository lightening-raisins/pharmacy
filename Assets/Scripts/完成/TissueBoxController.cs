using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TissueBoxController : MonoBehaviour
{
    public GameObject tissueBox; // �㲰�����
    public GameObject singleTissue1; // �Ĥ@�ӳ�i�����
    public GameObject singleTissue2; // �ĤG�ӳ�i�����
    public GameObject conditionObject1; // �Ĥ@�ӱ��󪫥�
    public GameObject conditionObject2; // �ĤG�ӱ��󪫥�
    public SteamVR_Behaviour_Pose leftHandPose; // ���⪺Pose
    public SteamVR_Behaviour_Pose rightHandPose; // �k�⪺Pose
    public Vector3 tissueOffset = new Vector3(0, 0.1f, 0); // ��i����Ȭ۹��㲰����Ȫ�����

    private bool leftHandTouching = false; // ����O�_��Ĳ
    private bool rightHandTouching = false; // �k��O�_��Ĳ
    private bool tissue1Shown = false; // �Ĥ@�ӳ�i����ȬO�_���
    private bool tissue2Shown = false; // �ĤG�ӳ�i����ȬO�_���
    private bool isGrabbing = false; // �O�_��������

    private Rigidbody tissueRigidbody1; // �Ĥ@�ӳ�i����Ȫ�Rigidbody
    private Rigidbody tissueRigidbody2; // �ĤG�ӳ�i����Ȫ�Rigidbody

    void Start()
    {
        singleTissue1.SetActive(false); // ��l���òĤ@�ӳ�i�����
        singleTissue2.SetActive(false); // ��l���òĤG�ӳ�i�����
        tissueRigidbody1 = singleTissue1.GetComponent<Rigidbody>(); // ����Ĥ@�ӳ�i����Ȫ�Rigidbody
        tissueRigidbody2 = singleTissue2.GetComponent<Rigidbody>(); // ����ĤG�ӳ�i����Ȫ�Rigidbody

        if (tissueRigidbody1 != null)
        {
            tissueRigidbody1.isKinematic = true; // �T�Ϊ��z�����v�T
        }

        if (tissueRigidbody2 != null)
        {
            tissueRigidbody2.isKinematic = true; // �T�Ϊ��z�����v�T
        }
    }

    void Update()
    {
        // �ˬdconditionObject1�MconditionObject2�O�_�X�{
        if (conditionObject1.activeSelf && !tissue1Shown)
        {
            // �ˬd����O�_Ĳ�I��㲰�����
            if (leftHandPose.transform != null && IsHandTouchingTissueBox(leftHandPose.transform) &&
                rightHandPose.transform != null && IsHandTouchingTissueBox(rightHandPose.transform))
            {
                Debug.Log("Both hands are touching the tissue box for the first tissue.");
                singleTissue1.SetActive(true); // ��ܲĤ@�ӳ�i�����
                tissue1Shown = true; // �O���w�g��ܹL�Ĥ@�ӳ�i�����

                // �]�m�Ĥ@�ӳ�i����Ȫ���m�A�����b�㲰����Ȫ��W��
                singleTissue1.transform.position = tissueBox.transform.position + tissueOffset;

                // �T�Ϊ��z�����v�T�A������ȻP��P�B����
                if (tissueRigidbody1 != null)
                {
                    tissueRigidbody1.isKinematic = true;
                }
            }
        }

        if (conditionObject2.activeSelf && !tissue2Shown)
        {
            // �ˬd����O�_Ĳ�I��㲰�����
            if (leftHandPose.transform != null && IsHandTouchingTissueBox(leftHandPose.transform) &&
                rightHandPose.transform != null && IsHandTouchingTissueBox(rightHandPose.transform))
            {
                Debug.Log("Both hands are touching the tissue box for the second tissue.");
                singleTissue2.SetActive(true); // ��ܲĤG�ӳ�i�����
                tissue2Shown = true; // �O���w�g��ܹL�ĤG�ӳ�i�����

                // �]�m�ĤG�ӳ�i����Ȫ���m
                singleTissue2.transform.position = tissueBox.transform.position + tissueOffset;

                // �T�Ϊ��z�����v�T�A������ȻP��P�B����
                if (tissueRigidbody2 != null)
                {
                    tissueRigidbody2.isKinematic = true;
                }
            }
        }

        // ������ɡA������ȸ��H�Ⲿ��
        if (leftHandTouching || rightHandTouching)
        {
            if (!isGrabbing)
            {
                isGrabbing = true;
                if (tissueRigidbody1 != null)
                    tissueRigidbody1.isKinematic = true; // �T�Ϊ��z�����v�T
                if (tissueRigidbody2 != null)
                    tissueRigidbody2.isKinematic = true; // �T�Ϊ��z�����v�T
            }

            // �T�O����Ȧ�m���H�⪺��m
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
        else if (isGrabbing) // ��}��ɫ�_���z�v�T
        {
            if (tissueRigidbody1 != null)
                tissueRigidbody1.isKinematic = false; // ��_���z�����v�T
            if (tissueRigidbody2 != null)
                tissueRigidbody2.isKinematic = false; // ��_���z�����v�T

            isGrabbing = false;
        }
    }

    // �P�_��O�_��Ĳ��"�㲰�����"
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
