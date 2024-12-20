using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BarrierControl : MonoBehaviour
{
    public GameObject foldingBarricade; // 摺疊封鎖線物件
    public GameObject unfoldedBarricade;
    public GameObject button1; // 第一個按鈕
    public GameObject button2; // 第二個按鈕

    private bool isBarricadeHidden = false; // 用來判斷摺疊封鎖線是否隱藏

    void Start()
    {
        // 初始化時隱藏要顯示的物件
        unfoldedBarricade.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name); // 顯示觸發的物件名稱

        // 當摺疊封鎖線碰觸第一個按鈕
        if (other.gameObject == button1 && !isBarricadeHidden)
        {
            button1.SetActive(false); // 隱藏第一個按鈕
            isBarricadeHidden = true;
            Debug.Log("Folding barricade hidden");
        }

        // 當任意物體碰觸到第二個按鈕，顯示物件
        else if (other.gameObject == button2 && isBarricadeHidden)
        {
            SetObjectVisibility(foldingBarricade, false); // 隱藏摺疊封鎖線
            unfoldedBarricade.SetActive(true);
            button2.SetActive(false); // 隱藏第二個按鈕
            Debug.Log("objectToShow is now visible");
        }
    }

    // 控制物件的顯示狀態
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
        }
    }
}
