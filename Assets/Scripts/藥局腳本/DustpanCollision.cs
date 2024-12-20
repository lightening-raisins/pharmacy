using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DustpanCollision : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // �n���ê�����
    [SerializeField] private GameObject displayObject;
    [SerializeField] private string glassJarTag = "GlassJar"; // ������������

    public GameObject bottleCap; // �~�\����

    public SteamVR_Input_Sources leftHand; // ����
    public SteamVR_Input_Sources rightHand; // �k��
    public SteamVR_Action_Boolean grabAction; // �ΨӰ���������欰

    private bool leftHandTouching = false;
    private bool rightHandTouching = false;
    private bool targetObjectHidden = false; // �аO�ؼЪ���O�_�w����

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�I���쪺����O: " + collision.gameObject.name); // �L�X�I�����󪺦W��
        // �T�{�I���쪺����O�_�O������
        if (collision.gameObject.CompareTag(glassJarTag))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(false); // ���åؼЪ���
                targetObjectHidden = true; // �]�m�ؼЪ���w����
            }
            else
            {
                Debug.LogWarning("�����w�n���ê�����I");
            }
        }
    }

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

        // �����ⳣĲ�I�B�ؼЪ���w���î���ܲ~�\
        if (leftHandTouching && rightHandTouching && targetObjectHidden)
        {
            bottleCap.SetActive(true);
            displayObject.SetActive(true);
        }
    }
}
