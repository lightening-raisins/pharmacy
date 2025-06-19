using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class gogglesWearableItem : MonoBehaviour
{
    [Header("����W��������ܪ���")]
    public GameObject correspondingWornObject;

    [Header("�����e�ݭn���ҥΪ�Ĳ�o���󪫥�")]
    public GameObject triggerConditionObject; // �o�Ӫ���ҥΫ�~�i����

    void OnTriggerEnter(Collider other)
    {
        // �[�J���󭭨�G�ݭn��Ĳ�o�Y�Ӫ���~�����
        if (!IsConditionMet())
        {
            Debug.Log("���󥼹F���A�L�k�����@����I");
            return;
        }

        if (other.CompareTag("Body"))
        {
            Interactable interactable = GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(gameObject);
            }

            gameObject.SetActive(false);

            if (correspondingWornObject != null)
            {
                correspondingWornObject.SetActive(true);
            }
        }

        bool IsConditionMet()
        {
            return triggerConditionObject == null || triggerConditionObject.activeSelf;
        }
    }
}
