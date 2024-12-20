using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem; // 引入 SteamVR Interaction System

public class TrashBinCollision : MonoBehaviour
{
    public GameObject collisionObject; // 任由袋
    public GameObject trashBin; // 其他物件
    public GameObject appear; // 顯示效果物件
    public ScoreManager scoreManager; // 拖入 ScoreManager 物件

    // 當物體發生碰撞時觸發
    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物體是否是目標物件
        if (collision.gameObject == collisionObject || collision.gameObject == trashBin)
        {
            // 嘗試取得物件的 Interactable 組件
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                // 如果物件被抓取，解除抓取
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(collision.gameObject);
            }

            // 隱藏碰撞物件
            collision.gameObject.SetActive(false);

            // 顯示新的物件或效果
            if (appear != null)
            {
                appear.SetActive(true);

                if (scoreManager != null)
                {
                    scoreManager.AddScore(5);
                }
                else
                {
                    Debug.LogError("ScoreManager is not assigned in VRButtonHandler.");
                }
            }
        }
    }
}
