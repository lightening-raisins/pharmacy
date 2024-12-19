using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AbsorbPadDisplay : MonoBehaviour
{
    public GameObject wholeMat;  // ��U�l����
    public GameObject singleMat; // ��i�l����
    public SteamVR_Behaviour_Pose leftHandPose; // ���⪺Pose
    public SteamVR_Behaviour_Pose rightHandPose; // �k�⪺Pose
    public Vector3 matOffset = new Vector3(0, 0.1f, 0); // ��i�l���Ԫ������q

    private bool leftHandTouching = false; // ����O�_��Ĳ
    private bool rightHandTouching = false; // �k��O�_��Ĳ
    private bool matShown = false; // ��i�l���ԬO�_���
    private Rigidbody matRigidbody; // ��i�l���Ԫ�Rigidbody

    void Start()
    {
        // ��l���ó�i�l����
        singleMat.SetActive(false);

        matRigidbody = singleMat.GetComponent<Rigidbody>();
        if (matRigidbody != null)
        {
            matRigidbody.isKinematic = true; // �T�Ϊ��z�v�T
        }

        // �ϥ� TransformPoint �]�w��l�@�ɮy��
        Vector3 initialPosition = wholeMat.transform.TransformPoint(matOffset);
        singleMat.transform.position = initialPosition;

        Debug.Log("WholeMat world position: " + wholeMat.transform.position);
        Debug.Log("Initial SingleMat world position: " + singleMat.transform.position);
    }

    void Update()
    {
        // �ˬd����O�_��Ĳ��U�l����
        leftHandTouching = leftHandPose.transform != null && IsHandTouchingMat(leftHandPose.transform);
        rightHandTouching = rightHandPose.transform != null && IsHandTouchingMat(rightHandPose.transform);

        // �p�G�Ⱖ�ⳣ�IĲ��B��i�l���ԩ|�����
        if (leftHandTouching && rightHandTouching && !matShown)
        {
            Debug.Log("Both hands are touching the mat.");
            matShown = true; // �аO��i�l���Ԥw���

            // ��ܳ�i�l����
            singleMat.SetActive(true);

            // �A���]�m���T���@�ɦ�m
            Vector3 correctPosition = wholeMat.transform.TransformPoint(matOffset);
            singleMat.transform.position = correctPosition;

            // �T�Ϊ��z�����v�T
            if (matRigidbody != null)
            {
                matRigidbody.isKinematic = true;
            }

            // ��X�ոո�T
            Debug.Log("SingleMat world position after activation: " + singleMat.transform.position);
            Debug.Log("WholeMat rotation: " + wholeMat.transform.rotation.eulerAngles);
            Debug.Log("SingleMat parent: " + singleMat.transform.parent.name);
        }
    }

    // �P�_��O�_��Ĳ��"��U�l����"
    bool IsHandTouchingMat(Transform handTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(handTransform.position, 0.1f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == wholeMat)
            {
                return true;
            }
        }
        return false;
    }
}
