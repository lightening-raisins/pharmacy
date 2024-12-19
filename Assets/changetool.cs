using UnityEngine;
using Valve.VR.InteractionSystem;

public class ToolControllerNew : MonoBehaviour
{
    // 新工具的Prefab
    public GameObject newToolPrefab;
    private bool isToolActive = true;

    void OnTriggerEnter(Collider other)
    {
        // 確保當前工具處於啟用狀態
        if (isToolActive && other.CompareTag("hand"))
        {
            // 嘗試取得工具的Interactable組件
            Interactable interactable = GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                // 如果工具被抓住，解除抓取
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(gameObject);

                // 隱藏當前工具
                gameObject.SetActive(false);
                isToolActive = false;

                // 替換成新工具
                if (newToolPrefab != null)
                {
                    // 在手的位置生成新工具
                    GameObject newTool = Instantiate(newToolPrefab, hand.transform.position, hand.transform.rotation);

                    // 設置新工具為抓取狀態
                    Interactable newToolInteractable = newTool.GetComponent<Interactable>();
                    if (newToolInteractable != null)
                    {
                        hand.AttachObject(newTool, GrabTypes.Grip);
                    }
                }
            }
        }
    }

    // 用於重新啟用當前工具（如需保留工具在場景中）
    public void ReactivateCurrentTool()
    {
        gameObject.SetActive(true);
        isToolActive = true;
    }
}
