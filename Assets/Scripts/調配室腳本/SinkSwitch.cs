using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkSwitch : MonoBehaviour
{
    public GameObject waterObject; // 水的物件

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("進入觸發區域的物件: " + other.name); // 確認觸發物件名稱

        // 檢查碰到的物件是否是手把
        if (other.CompareTag("hand"))
        {
            //Debug.Log("手把觸發成功！"); // 確認標籤判斷成功

            // 顯示水的物件
            if (waterObject != null)
            {
                waterObject.SetActive(true);
                Debug.Log("水的物件已顯示");
            }
            else
            {
                Debug.LogError("waterObject 尚未設置！");
            }
        }
    }
}
