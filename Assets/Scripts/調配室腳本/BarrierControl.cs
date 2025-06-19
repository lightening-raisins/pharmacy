using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BarrierControl : MonoBehaviour
{
    public GameObject foldingBarricade;       // 摺疊封鎖線物件
    public GameObject unfoldedBarricade;      // 展開的封鎖線物件
    public GameObject button1;                // 第一個按鈕
    public GameObject button2;                // 第二個按鈕
    public GameObject tips1;
    public GameObject tips2;

    private bool button1Touched = false;
    private bool button2Touched = false;
    private bool hasUnfoldedShown = false;

    void Start()
    {
        unfoldedBarricade.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);

        if (other.gameObject == button1 && !button1Touched)
        {
            button1Touched = true;
            button1.SetActive(false);
            Debug.Log("Button 1 touched");
        }

        if (other.gameObject == button2 && !button2Touched)
        {
            button2Touched = true;
            button2.SetActive(false);
            Debug.Log("Button 2 touched");
        }

        // 如果兩個按鈕都被碰過，才顯示 unfoldedBarricade
        if (button1Touched && button2Touched && !hasUnfoldedShown)
        {
            SetObjectVisibility(foldingBarricade, false);
            unfoldedBarricade.SetActive(true);
            if (tips1 != null) tips1.SetActive(true);
            if (tips2 != null) tips2.SetActive(true);
            hasUnfoldedShown = true;
            Debug.Log("Unfolded barricade is now visible");
        }
    }

    // 控制物件的顯示狀態
    private void SetObjectVisibility(GameObject obj, bool isVisible)
    {
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
