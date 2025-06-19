using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class gogglesWearableItem : MonoBehaviour
{
    [Header("身體上對應的顯示物件")]
    public GameObject correspondingWornObject;

    [Header("穿戴前需要先啟用的觸發條件物件")]
    public GameObject triggerConditionObject; // 這個物件啟用後才可穿戴

    void OnTriggerEnter(Collider other)
    {
        // 加入條件限制：需要先觸發某個物件才能穿戴
        if (!IsConditionMet())
        {
            Debug.Log("條件未達成，無法穿戴護目鏡！");
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
