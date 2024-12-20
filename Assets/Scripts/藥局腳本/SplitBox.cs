using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBox : MonoBehaviour
{
    public GameObject emptySprinklerBox;
    public GameObject fullSprinklerBox;

    private void Start()
    {
        // 確保開始時只有空潑灑箱可見
        if (emptySprinklerBox != null) SetObjectVisibility(emptySprinklerBox, true);
        if (fullSprinklerBox != null) SetObjectVisibility(fullSprinklerBox, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物件是否是椅子（根據 Tag 判斷）
        if (collision.gameObject.CompareTag("MedicineCabinet"))
        {
            if (emptySprinklerBox != null && fullSprinklerBox != null)
            {
                // 隱藏空潑灑箱並顯示滿潑灑箱
                SetObjectVisibility(emptySprinklerBox, false);

                SetObjectVisibility(fullSprinklerBox, true);

                Debug.Log("裝滿潑灑箱顯示！");
            }
        }
    }

    private void SetObjectVisibility(GameObject obj, bool isVisible)
    {
        // 如果物件有 Renderer 組件
        if (obj != null)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = isVisible;
                }
            }

            // 啟用或禁用碰撞
            Collider[] colliders = obj.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                if (collider != null)
                {
                    collider.enabled = isVisible;
                }
            }

            // 啟用或禁用物件本身
            obj.SetActive(isVisible);
        }
    }
}
