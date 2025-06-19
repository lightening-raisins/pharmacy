using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WearableItem : MonoBehaviour
{
    public GameObject correspondingWornObject; // ����W������ܪ�����A�Ҧp�Y�W���U�l�B�y�W���f�n

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            // �Ѱ����
            Interactable interactable = GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(gameObject);
            }

            // ���åثe��������
            gameObject.SetActive(false);

            // ��ܨ��W����������
            if (correspondingWornObject != null)
            {
                correspondingWornObject.SetActive(true);
            }
        }
    }
}
