using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleAction : MonoBehaviour
{
    [Header("物件參考")]
    public GameObject bottleCap;       // 瓶蓋物件
    public GameObject water;           // 水柱物件
    public Transform bottleBody;       // 瓶身
    public Transform bottleMouth;      // 瓶口（生成水柱的位置）

    [Header("出水條件")]
    public float pourAngleThreshold = 60f; // 傾倒角度門檻

    [Header("水柱角度微調")]
    public Vector3 streamRotationOffset = new Vector3(25f, 0f, 0f); // 水柱旋轉微調

    private bool isPouring = false;
    private bool isCapHidden = false;

    void Start()
    {
        // 預設關閉水柱
        if (water != null)
        {
            water.SetActive(false);
        }
    }

    void Update()
    {
        // 瓶蓋是否已經打開
        if (!isCapHidden && !bottleCap.activeSelf)
        {
            isCapHidden = true;
        }

        if (!isCapHidden)
        {
            StopPouring();
            return;
        }

        // 使用世界空間的角度來判斷是否傾倒
        float tiltAngle = Vector3.Angle(bottleBody.up, Vector3.up);

        if (tiltAngle > pourAngleThreshold)
        {
            if (!isPouring)
            {
                StartPouring();
            }

            // 調整水柱的位置與旋轉
            if (bottleMouth != null && water != null)
            {
                water.transform.position = bottleMouth.position;

                Quaternion baseRotation = Quaternion.LookRotation(-bottleMouth.up);
                Quaternion offsetRotation = Quaternion.Euler(streamRotationOffset);
                water.transform.rotation = baseRotation * offsetRotation;
            }
        }
        else
        {
            if (isPouring)
            {
                StopPouring();
            }
        }
    }

    void StartPouring()
    {
        isPouring = true;
        if (water != null)
        {
            water.SetActive(true);
        }
    }

    void StopPouring()
    {
        isPouring = false;
        if (water != null)
        {
            water.SetActive(false);
        }
    }
}
