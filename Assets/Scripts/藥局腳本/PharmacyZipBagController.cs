using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PharmacyZipBagController : MonoBehaviour
{
    [Header("����Ѧ�")]
    public GameObject wholeZiplockBag;   // ��U����U����
    public GameObject singleZiplockBag1;  // �Ĥ@�ӳ�ӧ���U����
    public GameObject singleZiplockBag2;  // �ĤG�ӳ�ӧ���U����

    [Header("���󪫥�")]
    public GameObject conditionObject1; // �Ĥ@�ӱ��󪫥�
    public GameObject conditionObject2; // �ĤG�ӱ��󪫥�


    [Header("�ⳡ Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;  // ���� Pose
    public SteamVR_Behaviour_Pose rightHandPose; // �k�� Pose

    [Header("��ӧ���U��m����")]
    public Vector3 ziplockOffset = new Vector3(0, 0.1f, 0); // ��ӧ���U�������q

    private bool leftHandTouching = false; // ����O�_��Ĳ
    private bool rightHandTouching = false; // �k��O�_��Ĳ
    private bool ziplockBag1Shown = false; // �Ĥ@�ӳ�ӧ���U�O�_���
    private bool ziplockBag2Shown = false; // �ĤG�ӳ�ӧ���U�O�_���

    void Start()
    {
        // ��l���éҦ���ӧ���U
        singleZiplockBag1.SetActive(false);
        singleZiplockBag2.SetActive(false);
    }

    void Update()
    {
        // ��������O�_��Ĳ��U����U
        leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);

        // �����k��O�_��Ĳ��U����U
        rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

        // �u����Ⱖ�ⳣĲ�I���U����U�ɡA��ܳ�ӧ���U
        if (leftHandTouching && rightHandTouching)
        {
            if (conditionObject1.activeSelf && !ziplockBag1Shown)
            {
                ShowSingleZiplockBag(singleZiplockBag1);
                ziplockBag1Shown = true;
            }
            else if (conditionObject2.activeSelf && !ziplockBag2Shown)
            {
                ShowSingleZiplockBag(singleZiplockBag2);
                ziplockBag2Shown = true;
            }
        }
    }

    /// <summary>
    /// �˴���O�_��Ĳ��U����U
    /// </summary>
    bool IsHandTouchingZipBag(Transform handTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(handTransform.position, 0.1f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == wholeZiplockBag)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// ��ܳ�ӧ���U�ó]�w��m
    /// </summary>
    void ShowSingleZiplockBag(GameObject ziplockBag)
    {
        ziplockBag.SetActive(true);
        ziplockBag.transform.position = wholeZiplockBag.transform.position + ziplockOffset;
        Debug.Log(ziplockBag.name + " �X�{�I");
    }
}
