using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRTriggerHandler : MonoBehaviour
{
    public GameObject objectToShow; // ��J�A�Q��ܪ�����
    public GameObject objectToHide; // ��J�A�Q���ê�����

    public GameObject targetObject1; // ���w���Ĥ@�ӸIĲ����
    public GameObject targetObject2; // ���w���ĤG�ӸIĲ����

    private bool isTarget1Touched = false; // �O�� targetObject1 �O�_�QĲ�I
    private bool isTarget2Touched = false; // �O�� targetObject2 �O�_�QĲ�I

    // �������I�쪫�󪺮ɭ�
    private void OnHandHoverBegin(Hand hand)
    {
        Debug.Log("Hand hovered over: " + hand.gameObject.name);

        if (hand.gameObject == targetObject1)
        {
            isTarget1Touched = true;
            Debug.Log("Target 1 touched");
        }
        else if (hand.gameObject == targetObject2)
        {
            isTarget2Touched = true;
            Debug.Log("Target 2 touched");
        }

        if (isTarget1Touched && isTarget2Touched)
        {
            ShowObject();
            HideObject();
            Debug.Log("Both targets touched. Showing object and hiding another.");
        }
    }

    private void OnHandHoverEnd(Hand hand)
    {
        Debug.Log("Hand stopped hovering over: " + hand.gameObject.name);

        if (hand.gameObject == targetObject1)
        {
            isTarget1Touched = false;
            Debug.Log("Target 1 released");
        }
        else if (hand.gameObject == targetObject2)
        {
            isTarget2Touched = false;
            Debug.Log("Target 2 released");
        }
    }

    private void ShowObject()
    {
        // ��ܪ���
        objectToShow.SetActive(true);
    }

    private void HideObject()
    {
        // ���ê���
        objectToHide.SetActive(false);
    }
}
