using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SprayBoxLidController : MonoBehaviour
{
    public GameObject hiddenLid;  // 最初的蓋子 (不可抓取)
    public GameObject displayLid;  // 顯示的蓋子 (可抓取)

    private void Start()
    {
        // 確保開始時只有隱藏蓋子可見，顯示蓋子隱藏
        if (hiddenLid != null) hiddenLid.SetActive(true);
        if (displayLid != null) displayLid.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 檢查碰撞的物件是否是手部 (這裡使用Trigger而非Collider進行檢查)
        if (other.CompareTag("hand"))
        {
            if (hiddenLid != null && displayLid != null)
            {
                // 隱藏最初的蓋子
                hiddenLid.SetActive(false);

                // 顯示可以抓取的蓋子
                displayLid.SetActive(true);
                Debug.Log("顯示可抓取蓋子！");
            }
        }
    }
}
