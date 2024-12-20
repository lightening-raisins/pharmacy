using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleAction : MonoBehaviour
{
    public GameObject bottleCap; // 瓶蓋物件
    public GameObject water; // 要顯示或隱藏的水柱物件
    public Transform bottleBody; // 瓶身物件
    public Transform bottleTop; // 瓶口位置
    public Transform waterTop; // 水柱頂端的位置（子物件）
    public float minAngle = 50f; // 顯示水的最小角度
    public float maxAngle = 100f; // 顯示水的最大角度

    private bool isCapHidden = false; // 瓶蓋是否隱藏
    private Vector3 initialForward; // 瓶身初始的前方向

    void Start()
    {
        // 記錄瓶身的初始前方向
        initialForward = bottleBody.forward;

        // 確保 water 預設為隱藏
        if (water != null && water.activeSelf)
        {
            water.SetActive(false);
            Debug.Log("將 water 預設為隱藏");
        }
    }

    void Update()
    {
        // 確認瓶蓋是否隱藏
        if (!isCapHidden && !bottleCap.activeSelf)
        {
            isCapHidden = true;
        }

        // 如果瓶蓋已隱藏，檢查角度範圍並顯示或隱藏水柱
        if (isCapHidden)
        {
            // 計算瓶身當前方向與初始方向的角度差
            float angleDifference = Vector3.Angle(initialForward, bottleBody.forward);

            // 如果角度差達到目標角度，顯示物件，否則隱藏
            if (angleDifference >= minAngle && angleDifference <= maxAngle)
            {
                AdjustWaterFlowDirection();
                ShowWater();
            }
            else
            {
                HideWater();
            }
        }
    }

    // 調整水流方向，使其保持朝下，並對齊水柱位置
    void AdjustWaterFlowDirection()
    {
        if (water != null && bottleTop != null && waterTop != null)
        {
            // 計算水柱的位置偏移，讓 waterTop 與瓶口對齊
            Vector3 offset = bottleTop.position - waterTop.position;
            water.transform.position += offset;

            // 假設水流物件是從瓶口發出，並且需要讓水流始終指向下方
            Vector3 bottleDownDirection = -bottleBody.up; // 確保水柱朝向瓶子的下方
            water.transform.rotation = Quaternion.LookRotation(bottleDownDirection);
        }
    }

    // 顯示物件的邏輯
    void ShowWater()
    {
        if (water != null && !water.activeSelf)
        {
            water.SetActive(true);
        }
    }

    // 隱藏物件的邏輯
    void HideWater()
    {
        if (water != null && water.activeSelf)
        {
            water.SetActive(false);
        }
    }
}