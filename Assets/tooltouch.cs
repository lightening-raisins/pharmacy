using UnityEngine;

public class WearableEquipment : MonoBehaviour
{
    public Transform attachPoint; // 裝備將要附加的位置（角色上的某個部位）

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody")) // 確保碰到的是玩家的身體
        {
            // 設置為身體的子物體
            transform.SetParent(attachPoint);
            transform.localPosition = Vector3.zero; // 調整位置
            transform.localRotation = Quaternion.identity; // 調整旋轉

            // 隱藏裝備的模型
            HideEquipment();
        }
    }

    private void HideEquipment()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false; // 隱藏所有渲染器
        }
    }
}
