using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShowObjectOnGrab : MonoBehaviour
{
    public GameObject objectToShow; // �n��ܪ�����

    private Interactable interactable; // ���ʨt�Ϊ��}��

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
        // ����Q���
        if (interactable != null && hand.GetGrabStarting() != GrabTypes.None)
        {
            if (objectToShow != null)
            {
                objectToShow.SetActive(true); // ��ܥؼЪ���
            }
        }
    }
}
