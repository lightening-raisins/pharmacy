using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayObject : MonoBehaviour
{
    public GameObject objectToShow; // 要顯示的物件
    void Start()
    {
        // 確保目標物件一開始隱藏
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }
}
