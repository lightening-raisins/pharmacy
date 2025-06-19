using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WearableItem : MonoBehaviour
{
    public GameObject correspondingWornObject; // 身體上對應顯示的物件，例如頭上的帽子、臉上的口罩

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            // 解除抓取
            Interactable interactable = GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(gameObject);
            }

            // 隱藏目前拿的物件
            gameObject.SetActive(false);

            // 顯示身上對應的物件
            if (correspondingWornObject != null)
            {
                correspondingWornObject.SetActive(true);
            }
        }
    }
}
