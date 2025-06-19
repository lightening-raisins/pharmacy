using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PharmacyZipBagController2 : MonoBehaviour
{
    [Header("����Ѧ�")]
    public GameObject wholeZiplockBag;     // ��U����U
    public GameObject singleZiplockBag1;   // �Ĥ@�ӳ�ӧ���U
    public GameObject singleZiplockBag2;   // �ĤG�ӳ�ӧ���U
    public GameObject singleZiplockBag3;   // �ĤT�ӳ�ӧ���U

    [Header("���󪫥�")]
    public GameObject conditionObject1;
    public GameObject conditionObject2;
    public GameObject conditionObject3;    // �ĤT�ӱ��󪫥�

    [Header("���ܤ�r")]
    public GameObject tips1;
    public GameObject tips2;
    public GameObject tips3;               // �ĤT�Ӵ���

    [Header("�ⳡ Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;

    [Header("��ӧ���U��m����")]
    public Vector3 ziplockOffset = new Vector3(0, 0.1f, 0);

    private bool leftHandTouching = false;
    private bool rightHandTouching = false;

    private bool ziplockBag1Shown = false;
    private bool ziplockBag2Shown = false;
    private bool ziplockBag3Shown = false; // �ĤT����ܧP�_

    void Start()
    {
        singleZiplockBag1.SetActive(false);
        singleZiplockBag2.SetActive(false);
        singleZiplockBag3.SetActive(false); // �ĤT�Ӫ�l������
    }

    void Update()
    {
        leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);
        rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

        if (leftHandTouching && rightHandTouching)
        {
            if (conditionObject1.activeSelf && !ziplockBag1Shown)
            {
                ShowSingleZiplockBag(singleZiplockBag1);
                ziplockBag1Shown = true;
                if (tips1 != null) tips1.SetActive(true);
            }
            else if (conditionObject2.activeSelf && !ziplockBag2Shown)
            {
                ShowSingleZiplockBag(singleZiplockBag2);
                ziplockBag2Shown = true;
                if (tips2 != null) tips2.SetActive(true);
            }
            else if (conditionObject3.activeSelf && !ziplockBag3Shown)
            {
                ShowSingleZiplockBag(singleZiplockBag3);
                ziplockBag3Shown = true;
                if (tips3 != null) tips3.SetActive(true);
            }
        }
    }

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

    void ShowSingleZiplockBag(GameObject ziplockBag)
    {
        ziplockBag.SetActive(true);
        ziplockBag.transform.position = wholeZiplockBag.transform.position + ziplockOffset;
        Debug.Log(ziplockBag.name + " �X�{�I");
    }
}
