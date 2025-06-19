using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnTouch : MonoBehaviour
{
    [Header("Ĳ�o���󪫥�]��ӳ�����ܡ^")]
    public GameObject prerequisiteObjectA;
    public GameObject prerequisiteObjectB;

    [Header("�n�ͦ����ؼЪ���")]
    public GameObject targetObject;

    [Header("VR �۾��]VRCamera�^")]
    public Transform vrCamera;

    [Header("�ͦ���m�������q")]
    public Vector3 offset = new Vector3(0, 0f, 0.5f); // �ۭq����

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("�I�쪺����W�١G" + other.name + ", Tag: " + other.tag);
        if (hasTriggered) return;

        // �T�{��ӱ��󪫥���ܤ�
        if (prerequisiteObjectA != null && prerequisiteObjectA.activeSelf &&
            prerequisiteObjectB != null && prerequisiteObjectB.activeSelf)
        {
            // �T�{�I�쪺�O����
            if (other.CompareTag("MainCamera"))
            {
                // �p�� VR �e�谾����m
                Vector3 spawnPosition = vrCamera.position + vrCamera.forward * offset.z + vrCamera.up * offset.y + vrCamera.right * offset.x;

                targetObject.transform.position = spawnPosition;
                targetObject.SetActive(true);
                hasTriggered = true; // �קK����Ĳ�o
            }
        }
    }
}
