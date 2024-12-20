using UnityEngine;
using Valve.VR.InteractionSystem;

public class ToolControllerNew : MonoBehaviour
{
    // 新工具的Prefab
    public GameObject newToolPrefab;
    private bool isToolActive = true;
    private static int currentEquipIndex = 0; // 裝備的順序索引，從0開始

    public enum EquipmentType
    {
        MaskBox,
        ProtectiveSuit,
        GlovesBag,
        CapCoverBag,
        ShoeCoverBag
    }

    public EquipmentType equipmentType; // 裝備的類型

    void OnTriggerEnter(Collider other)
    {
        // 確保當前工具處於啟用狀態，且碰撞的對象為"hand"
        if (isToolActive && other.CompareTag("hand"))
        {
            // 確認當前工具已被抓住，並且檢查裝備順序
            Interactable interactable = GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                Hand grabbingHand = interactable.attachedToHand;

                // 判斷是否是另一隻手進入觸發區
                if (other.gameObject != grabbingHand.gameObject)
                {
                    // 檢查是否可以穿戴此裝備
                    if (CanEquip())
                    {
                        // 解除當前抓取
                        grabbingHand.DetachObject(gameObject);

                        // 隱藏當前工具
                        gameObject.SetActive(false);
                        isToolActive = false;

                        // 替換成新工具
                        if (newToolPrefab != null)
                        {
                            // 在手的位置生成新工具
                            GameObject newTool = Instantiate(newToolPrefab, grabbingHand.transform.position, grabbingHand.transform.rotation);

                            // 設置新工具為抓取狀態
                            Interactable newToolInteractable = newTool.GetComponent<Interactable>();
                            if (newToolInteractable != null)
                            {
                                grabbingHand.AttachObject(newTool, GrabTypes.Grip);
                            }
                        }

                        // 更新裝備順序
                        currentEquipIndex++;
                    }
                }
            }
        }
    }

    // 用於檢查是否可以穿戴當前裝備
    bool CanEquip()
    {
        // 確保當前裝備是按順序穿戴的
        switch (equipmentType)
        {
            case EquipmentType.MaskBox:
                return currentEquipIndex == 0; // 口罩盒是第一個裝備
            case EquipmentType.ProtectiveSuit:
                return currentEquipIndex == 1; // 防護衣是第二個裝備
            case EquipmentType.GlovesBag:
                return currentEquipIndex == 2; // 手套夾鏈袋是第三個裝備
            case EquipmentType.CapCoverBag:
                return currentEquipIndex == 3; // 帽套夾鏈袋是第四個裝備
            case EquipmentType.ShoeCoverBag:
                return currentEquipIndex == 4; // 鞋套夾鏈袋是第五個裝備
            default:
                return false;
        }
    }

    // 用於重新啟用當前工具（如需保留工具在場景中）
    public void ReactivateCurrentTool()
    {
        gameObject.SetActive(true);
        isToolActive = true;
    }
}
