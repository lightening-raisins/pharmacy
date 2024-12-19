using UnityEngine;
using Valve.VR.InteractionSystem; // 引入 SteamVR Interaction System

public class ToolController : MonoBehaviour
{
    // 提示和對話框的UI物件
    public GameObject dialogBox;
    private bool isToolActive = true;

    void OnTriggerEnter(Collider other)
    {
        // 當工具碰到 BodyCollider 時觸發
        if (isToolActive && other.CompareTag("Body"))
        {
            // 嘗試取得工具的 Interactable 組件
            Interactable interactable = GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                // 如果工具被手抓住，解除抓取
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(gameObject);
            }

            // 隱藏工具
            gameObject.SetActive(false);
            isToolActive = false;

            // 顯示提示框
            if (dialogBox != null)
            {
                dialogBox.SetActive(true);
            }
        }
    }

    // 用於重新啟用工具
    public void ReactivateTool()
    {
        gameObject.SetActive(true);
        isToolActive = true;

        // 隱藏對話框
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
        }
    }
}
