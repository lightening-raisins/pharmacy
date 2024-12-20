using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ZipBagController2 : MonoBehaviour
{
    [Header("����Ѧ�")]
    public GameObject wholeZiplockBag;   // ��U����U����
    public GameObject singleZiplockBag;  // ��ӧ���U����
    public GameObject triggerObject;     // ����O�_�}�l�ˬdĲ�I������]�Ҧp�G�@�ӯS�w������^

    [Header("�ⳡ Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;  // ���� Pose
    public SteamVR_Behaviour_Pose rightHandPose; // �k�� Pose

    [Header("��ӧ���U��m����")]
    public Vector3 ziplockOffset = new Vector3(0, 0.1f, 0); // ��ӧ���U�������q

    private bool leftHandTouching = false; // ����O�_��Ĳ
    private bool rightHandTouching = false; // �k��O�_��Ĳ
    private bool ziplockBagShown = false; // �O�_��ܳ�ӧ���U
    private bool isTriggerObjectActive = false; // ����O�_�}�l�ˬdĲ�I���ܼ�

    void Start()
    {
        singleZiplockBag.SetActive(false); // ��l���ó�ӧ���U
        isTriggerObjectActive = false;     // ��l���}�l�ˬdĲ�I
    }

    void Update()
    {
        // �u���btriggerObject�X�{��A�~�}�l�ˬd�ⳡ�O�_��Ĳ����U
        if (triggerObject.activeSelf && !isTriggerObjectActive)
        {
            isTriggerObjectActive = true; // �}�l�ˬdĲ�I
        }

        // �p�G�}�l�ˬdĲ�I
        if (isTriggerObjectActive)
        {
            // ��������O�_��Ĳ��U����U
            leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);

            // �����k��O�_��Ĳ��U����U
            rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

            // �u����Ⱖ�ⳣĲ�I���U����U�ɡA��ܳ�ӧ���U
            if (leftHandTouching && rightHandTouching && !ziplockBagShown)
            {
                ShowSingleZiplockBag();
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
    void ShowSingleZiplockBag()
    {
        singleZiplockBag.SetActive(true);
        singleZiplockBag.transform.position = wholeZiplockBag.transform.position + ziplockOffset;
        ziplockBagShown = true;

        Debug.Log("��ӧ���U�X�{�I");
    }
}
