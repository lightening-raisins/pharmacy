using UnityEngine;

public class SpillBoxManager : MonoBehaviour
{
    public GameObject[] tools; // 工具物件列表
    private Vector3 lastPosition; // 上一幀的位置
    private bool isMoving; // 判斷箱子是否正在移動

    private void Start()
    {
        lastPosition = transform.position;
        UpdateToolPhysics(false); // 禁用工具的剛體效果（初始化靜置狀態）
    }

    private void Update()
    {
        // 檢查箱子是否正在移動
        isMoving = (transform.position != lastPosition);
        lastPosition = transform.position;

        // 根據移動狀態調整工具的父子關係與物理狀態
        foreach (GameObject tool in tools)
        {
            if (isMoving)
            {
                // 工具成為箱子的子物件，跟隨箱子移動，禁用剛體
                if (tool.transform.parent != transform)
                {
                    tool.transform.SetParent(transform);
                    UpdateToolPhysics(false);
                }
            }
            else
            {
                // 工具恢復自由狀態，讓它保持靜止並允許抓取
                if (tool.transform.parent == transform)
                {
                    tool.transform.SetParent(null);
                    UpdateToolPhysics(true); // 啟用剛體並保持靜止
                }
            }
        }
    }

    // 更新工具的剛體狀態
    private void UpdateToolPhysics(bool enable)
    {
        foreach (GameObject tool in tools)
        {
            Rigidbody rb = tool.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = !enable; // 如果禁用剛體，設置為 kinematic，允許靜止
                if (enable)
                {
                    rb.velocity = Vector3.zero; // 防止工具在啟用剛體後繼續移動
                    rb.angularVelocity = Vector3.zero; // 防止工具旋轉
                }
            }
        }
    }
}
