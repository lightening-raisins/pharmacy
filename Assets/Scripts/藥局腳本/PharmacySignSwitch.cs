using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PharmacySignSwitch : MonoBehaviour
{
    public GameObject foldedSign; // 摺疊的警示牌
    public GameObject unfoldedSign; // 展開的警示牌
    public GameObject collisionObject; // 預期碰撞的摺疊警示牌

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected with: " + other.gameObject.name);
        // 檢查碰撞的物體是否是目標物件
        if (other.gameObject == collisionObject)
        {
            // 嘗試取得物件的 Interactable 組件
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                // 如果物件被抓取，解除抓取
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(other.gameObject);
            }

            // 確保是地板觸發點
            if (other.CompareTag("PlaneTrigger"))
            {
                // 隱藏摺疊警示牌，保留抓取的物件可見
                SetObjectVisibility(foldedSign, false);

                // 顯示展開警示牌
                unfoldedSign.SetActive(true);
            }
        }
    }

    // 控制物件的顯示狀態，保留抓取的物件顯示
    private void SetObjectVisibility(GameObject obj, bool isVisible)
    {
        // 如果物件被抓取，不隱藏物件
        if (obj != null)
        {
            // 如果物件有 Renderer 組件
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    // 如果物件被抓取，手把不會影響物件的顯示
                    if (obj.GetComponent<Interactable>() != null)
                    {
                        if (obj.GetComponent<Interactable>().attachedToHand != null)
                        {
                            continue; // 物件被抓取時，跳過隱藏
                        }
                    }
                    renderer.enabled = isVisible;
                }
            }
        }
    }
}
