using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TissueBoxController2 : MonoBehaviour
{
    public GameObject tissueBox; // �㲰�����
    public GameObject singleTissue2; // ��i�����
    public SteamVR_Behaviour_Pose leftHandPose; // ���⪺Pose
    public SteamVR_Behaviour_Pose rightHandPose; // �k�⪺Pose
    public Vector3 tissueOffset = new Vector3(0, 0.1f, 0); // ��i����Ȭ۹��㲰����Ȫ�����

    private bool leftHandTouching = false; // ����O�_��Ĳ
    private bool rightHandTouching = false; // �k��O�_��Ĳ
    private bool tissueShown = false; // ��i����ȬO�_���
    private bool isGrabbing = false; // �O�_��������

    private Rigidbody tissueRigidbody; // ��i����Ȫ�Rigidbody

    void Start()
    {
        singleTissue2.SetActive(false); // ��l���ó�i�����
        tissueRigidbody = singleTissue2.GetComponent<Rigidbody>(); // ���Rigidbody
        if (tissueRigidbody != null)
        {
            tissueRigidbody.isKinematic = true; // �T�Ϊ��z�����v�T
        }
    }

    void Update()
    {
        // �ˬd����O�_��Ĳ��㲰�����
        if (leftHandPose.transform != null && IsHandTouchingTissueBox(leftHandPose.transform))
        {
            leftHandTouching = true;
        }
        else
        {
            leftHandTouching = false;
        }

        // �ˬd�k��O�_��Ĳ��㲰�����
        if (rightHandPose.transform != null && IsHandTouchingTissueBox(rightHandPose.transform))
        {
            rightHandTouching = true;
        }
        else
        {
            rightHandTouching = false;
        }

        // �u����Ⱖ�ⳣĲ�I��㲰����ȮɡA��ܳ�i�����
        if (leftHandTouching && rightHandTouching && !tissueShown)
        {
            Debug.Log("Both hands are touching the tissue box.");
            singleTissue2.SetActive(true);
            tissueShown = true; // �O���w�g��ܹL��i�����

            // �]�m��i����Ȫ���m�A�����b�㲰����Ȫ��W��
            singleTissue2.transform.position = tissueBox.transform.position + tissueOffset;

            // �T�Ϊ��z�����v�T�A������ȻP��P�B����
            if (tissueRigidbody != null)
            {
                tissueRigidbody.isKinematic = true; // �T�Ϊ��z�����v�T
            }
        }

        // ������ɡA������ȸ��H�Ⲿ��
        if (leftHandTouching || rightHandTouching)
        {
            if (!isGrabbing)
            {
                isGrabbing = true;
                tissueRigidbody.isKinematic = true; // �T�Ϊ��z�����v�T
            }

            // �T�O����Ȧ�m���H�⪺��m
            Vector3 handPosition = leftHandTouching ? leftHandPose.transform.position : rightHandPose.transform.position;
            singleTissue2.transform.position = handPosition;

        }
        else if (isGrabbing) // ��}��ɫ�_���z�v�T
        {
            if (tissueRigidbody != null)
            {
                tissueRigidbody.isKinematic = false; // ��_���z�����v�T
            }
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
