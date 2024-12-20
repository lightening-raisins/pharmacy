using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharmacyBottleCollision : MonoBehaviour
{
    // 要顯示的物件陣列
    public GameObject parentObject;  // 父物件

    private bool isTouched = false;  // 確保水柱只觸發一次

    private void Start()
    {
        if (parentObject != null)
        {
            parentObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 檢查是否是水柱觸發事件，並且還沒觸發過
        if (other.CompareTag("Plane") && gameObject.CompareTag("MixedStream") && !isTouched)
        {
            Debug.Log("水柱成功觸發！");
            isTouched = true;  // 確保只觸發一次

            // 顯示父物件（如果需要顯示）
            if (parentObject != null)
            {
                parentObject.SetActive(true);
            }
        }
    }
}
