using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SprayBox : MonoBehaviour
{
    public GameObject emptySprinklerBox;
    public GameObject fullSprinklerBox;

    private void Start()
    {
        // 確保開始時只有空潑灑箱可見
        if (emptySprinklerBox != null) emptySprinklerBox.SetActive(true);
        if (fullSprinklerBox != null) fullSprinklerBox.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物件是否是椅子（根據 Tag 判斷）
        if (collision.gameObject.CompareTag("Chair"))
        {
            if (emptySprinklerBox != null && fullSprinklerBox != null)
            {
                // 取得空潑灑箱的位置和旋轉
                Vector3 emptyBoxPosition = emptySprinklerBox.transform.position;
                Quaternion emptyBoxRotation = emptySprinklerBox.transform.rotation;

                // 隱藏空潑灑箱
                emptySprinklerBox.SetActive(false);

                fullSprinklerBox.SetActive(true);
                Debug.Log("裝滿潑灑箱顯示！");

            }
        }
    }
}
