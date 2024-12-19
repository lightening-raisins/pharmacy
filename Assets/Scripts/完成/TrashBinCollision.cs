using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem; // �ޤJ SteamVR Interaction System

public class TrashBinCollision : MonoBehaviour
{
    public GameObject collisionObject; // ���ѳU
    public GameObject trashBin; // ��L����
    public GameObject appear; // ��ܮĪG����

    // ����o�͸I����Ĳ�o
    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I��������O�_�O�ؼЪ���
        if (collision.gameObject == collisionObject || collision.gameObject == trashBin)
        {
            // ���ը��o���� Interactable �ե�
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                // �p�G����Q����A�Ѱ����
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(collision.gameObject);
            }

            // ���øI������
            collision.gameObject.SetActive(false);

            // ��ܷs������ήĪG
            if (appear != null)
            {
                appear.SetActive(true);
            }
        }
    }
}
