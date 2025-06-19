using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnTouch : MonoBehaviour
{
    [Header("觸發條件物件（兩個都需顯示）")]
    public GameObject prerequisiteObjectA;
    public GameObject prerequisiteObjectB;

    [Header("要生成的目標物件")]
    public GameObject targetObject;

    [Header("VR 相機（VRCamera）")]
    public Transform vrCamera;

    [Header("生成位置的偏移量")]
    public Vector3 offset = new Vector3(0, 0f, 0.5f); // 自訂偏移

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("碰到的物件名稱：" + other.name + ", Tag: " + other.tag);
        if (hasTriggered) return;

        // 確認兩個條件物件都顯示中
        if (prerequisiteObjectA != null && prerequisiteObjectA.activeSelf &&
            prerequisiteObjectB != null && prerequisiteObjectB.activeSelf)
        {
            // 確認碰到的是身體
            if (other.CompareTag("MainCamera"))
            {
                // 計算 VR 前方偏移位置
                Vector3 spawnPosition = vrCamera.position + vrCamera.forward * offset.z + vrCamera.up * offset.y + vrCamera.right * offset.x;

                targetObject.transform.position = spawnPosition;
                targetObject.SetActive(true);
                hasTriggered = true; // 避免重複觸發
            }
        }
    }
}
