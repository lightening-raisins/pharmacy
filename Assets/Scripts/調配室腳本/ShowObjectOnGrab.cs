using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShowObjectOnGrab : MonoBehaviour
{
    public GameObject objectToShow; // �n��ܪ�����
    public GameObject requiredObject; // ��������ܪ�����
    private Interactable interactable; // ���ʨt�Ϊ��}��
    private bool hasShown = false; // �O���O�_�w��ܹL

    void Start()
    {
        // �T�O�ؼЪ���@�}�l����
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }

        // �����e���� Interactable �ե�
        interactable = GetComponent<Interactable>();
        if (interactable == null)
        {
            Debug.LogError("������ݭn�� Interactable �ե�I");
        }
    }

    void HandHoverUpdate(Hand hand)
    {
        // �ˬd��������ܪ�����O�_�w���
        if (requiredObject != null && !requiredObject.activeSelf)
        {
            return; // �p�G����������|����ܡA������^
        }

        // ����Q���
        if (!hasShown && interactable != null && hand.GetGrabStarting() != GrabTypes.None)
        {
            if (objectToShow != null)
            {
                objectToShow.SetActive(true); // ��ܥؼЪ���
                hasShown = true; // ��s���A���w���
            }
        }
    }
}
