using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PharmacySignSwitch : MonoBehaviour
{
    public GameObject foldedSign; // �P�|��ĵ�ܵP
    public GameObject unfoldedSign; // �i�}��ĵ�ܵP
    public GameObject collisionObject; // �w���I�����P�|ĵ�ܵP

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected with: " + other.gameObject.name);
        // �ˬd�I��������O�_�O�ؼЪ���
        if (other.gameObject == collisionObject)
        {
            // ���ը��o���� Interactable �ե�
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                // �p�G����Q����A�Ѱ����
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(other.gameObject);
            }

            // �T�O�O�a�OĲ�o�I
            if (other.CompareTag("PlaneTrigger"))
            {
                // ���úP�|ĵ�ܵP�A�O�d���������i��
                SetObjectVisibility(foldedSign, false);

                // ��ܮi�}ĵ�ܵP
                unfoldedSign.SetActive(true);
            }
        }
    }

    // �������ܪ��A�A�O�d������������
    private void SetObjectVisibility(GameObject obj, bool isVisible)
    {
        // �p�G����Q����A�����ê���
        if (obj != null)
        {
            // �p�G���� Renderer �ե�
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    // �p�G����Q����A��⤣�|�v�T�������
                    if (obj.GetComponent<Interactable>() != null)
                    {
                        if (obj.GetComponent<Interactable>().attachedToHand != null)
                        {
                            continue; // ����Q����ɡA���L����
                        }
                    }
                    renderer.enabled = isVisible;
                }
            }
        }
    }
}
