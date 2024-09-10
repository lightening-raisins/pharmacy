using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject intactbottleBody;
    [SerializeField] GameObject intactbottleCap;
    [SerializeField] GameObject brokebottle;
    [SerializeField] GameObject canvas;

    BoxCollider bc;

    private void Awake()
    {
        
        intactbottleBody.SetActive(true);
        intactbottleCap.SetActive(true);
        brokebottle.SetActive(false);
        canvas.SetActive(false);

        bc = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的對象名稱是否為 "Plane"
        if (collision.gameObject.name == "Plane")
        {
            Debug.Log("Collision with plane detected.");
            Break();
            canvas.SetActive(true);
        }
    }

    private void Break()
    {
        intactbottleBody.SetActive(false);
        intactbottleCap.SetActive(false);
        brokebottle.SetActive(true);

        bc.enabled = false;

        // 獲取地板的位置
        Vector3 floorPosition = Vector3.zero; // 這裡應該設置你的地板的位置

        // 遍歷所有子物體（碎片）
        foreach (Transform child in brokebottle.transform)
        {
            // 計算碎片與地板的交點
            RaycastHit hit;
            if (Physics.Raycast(child.position, Vector3.down, out hit))
            {
                // 使用交點作為碎片的新位置
                child.position = hit.point;

                // 計算碎片到碰撞點的向量
                Vector3 toHitPoint = hit.point - child.position;

                // 將碎片位置向內移動一定距離，以限制碎片的生成範圍
                float moveDistance = 0.1f; // 可以調整這個值以限制範圍
                child.position += toHitPoint.normalized * moveDistance;
            }
            else
            {
                // 如果沒有交點，就將碎片位置設置為地板位置
                child.position = floorPosition;
            }

            // 隨機生成偏移量，以模擬四濺效果
            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection.y = 0f; // 將 y 軸設置為 0，只在水平方向上四濺
            Vector3 randomOffset = randomDirection * Random.Range(0.05f, 0.2f);

            // 將偏移量添加到碎片位置
            child.localPosition += randomOffset;
        }
    }
}