using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CapDisplay : MonoBehaviour
{
    public GameObject bottleCap; // �~�\����
    public GameObject lowWater; // �C�������
    public GameObject highWater; // ���������

    public SteamVR_Input_Sources leftHand; // ����
    public SteamVR_Input_Sources rightHand; // �k��
    public SteamVR_Action_Boolean grabAction; // �ΨӰ���������欰

    private bool leftHandTouching = false;
    private bool rightHandTouching = false;

    // ��e������O�_�ŦX��ܲ~�\������
    private bool waterLevelOk = false;

    public WaterLevelControl waterLevelControl; // �b���ޥ� WaterLevelControl �}��

    void Update()
    {
        // �ˬd����O�_���
        if (grabAction.GetStateDown(leftHand))
        {
            leftHandTouching = true;
        }
        if (grabAction.GetStateDown(rightHand))
        {
            rightHandTouching = true;
        }

        // �ˬd����O�_��}
        if (grabAction.GetStateUp(leftHand))
        {
            leftHandTouching = false;
        }
        if (grabAction.GetStateUp(rightHand))
        {
            rightHandTouching = false;
        }

        // �ˬd������ҬO�_�s�b
        waterLevelOk = lowWater.activeInHierarchy && highWater.activeInHierarchy;

        // �����ⳣĲ�I�B����ŦX�������ܲ~�\
        if (leftHandTouching && rightHandTouching && waterLevelOk)
        {
            bottleCap.SetActive(true);
            waterLevelControl.isCapVisible = true; // ��~�\��ܫ�A���\�n�̰ʧ@
        }
    }
}
