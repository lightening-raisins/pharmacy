using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TissueManager : MonoBehaviour
{
    public GameObject[] wetTissues;  // 沾濕的擦手紙陣列
    public GameObject wetTissue;     // 單一濕擦手紙物件
    public GameObject[] wrinkledTissues;  // 儲存所有皺摺擦手紙物件
    public GameObject tips1;
    public GameObject tips2;

    private bool isTouched = false;  // 用來判斷是否已經碰到水
    private bool isWetEffectComplete = false; // 用來判斷沾濕效果是否完成
    private bool isRotationStarted = false;  // 用來判斷旋轉效果是否已經啟動
    private Coroutine displayCoroutine; // 儲存協程的引用，方便停止
    private Coroutine rotateCoroutine; // 儲存旋轉效果的協程引用

    void Start()
    {
        // 初始化時隱藏所有沾濕擦手紙
        UpdateWetTissueDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag("water") && !isTouched)  // 當擦手紙碰到水
        {
            Debug.Log("Water detected, starting absorbent pad animation.");
            isTouched = true;  // 設置已經碰到水

            other.gameObject.SetActive(false);  // 隱藏 "水" 物件

            // 開始顯示沾濕擦手紙的協程
            StartCoroutine(DisplayPads());
        }
        else if (other.CompareTag("hand") && isWetEffectComplete && !isRotationStarted)  // 如果是手觸發、沾濕效果完成且未開始旋轉
        {
            isRotationStarted = true;  // 標記旋轉效果已啟動

            // 開始顯示旋轉效果
            rotateCoroutine = StartCoroutine(RotateWrinkledTissues());
        }
    }

    private IEnumerator DisplayPads()
    {
        for (int i = 0; i < wetTissues.Length; i++)
        {
            wetTissues[i].SetActive(true);  
            //Debug.Log("Activated wet tissue index: " + i);

            yield return new WaitForSeconds(1f);  

            // 隱藏前一個擦手紙（在顯示下一個之前）
            if (i < wetTissues.Length - 1)
            {
                wetTissues[i].SetActive(false);  
            }
        }

        // 最後一個擦手紙保持顯示
        wetTissues[wetTissues.Length - 1].SetActive(true);
        if (tips1 != null) tips1.SetActive(true);
        isWetEffectComplete = true;  
    }

    private IEnumerator RotateWrinkledTissues()
    {
        wetTissue.SetActive(false);
        Debug.Log("WetTissue hidden.");


        for (int i = 0; i < wrinkledTissues.Length; i++)
        {
            if (!wrinkledTissues[i].activeSelf)
            {
                wrinkledTissues[i].SetActive(true);
            }
            //Debug.Log("Activated wrinkled tissue index: " + i);


            yield return new WaitForSeconds(1f);

            // 隱藏前一個物件（確保只有一個物件顯示）
            if (i < wrinkledTissues.Length - 1)
            {
                wrinkledTissues[i].SetActive(false);
            }
        }

        // 保證最後一個物件保持顯示
        wrinkledTissues[wrinkledTissues.Length - 1].SetActive(true);
        if (tips2 != null) tips2.SetActive(true);
    }

    // 更新目前顯示的擦手紙
    void UpdateWetTissueDisplay()
    {
        for (int i = 0; i < wetTissues.Length; i++)
        {
            wetTissues[i].SetActive(i == 0);  // 只顯示第一張沾濕的擦手紙
        }
    }
}
