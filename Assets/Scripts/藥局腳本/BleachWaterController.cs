using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleachWaterController : MonoBehaviour
{
    [Header("漂白水物件")]
    public GameObject cap;               // 瓶蓋
    public GameObject bottleBody;        // 瓶身
    public GameObject waterStream;       // 水柱物件
    public Transform mouthPoint;         // 瓶口（水柱生成位置）

    [Header("傾斜判定")]
    public float pourAngleThreshold = 60f; // 超過這個角度就出水

    [Header("水柱微調角度")]
    public Vector3 streamRotationOffset = new Vector3(25f, 0f, 0f); // 可在 Inspector 調整微調角度

    private bool isPouring = false;
    private bool hasRecordedInitialUp = false;
    private Vector3 initialUpDirection;

    void Update()
    {
        // 如果瓶蓋還在，停止出水並重置方向紀錄
        if (cap.activeSelf)
        {
            StopPouring();
            hasRecordedInitialUp = false;
            return;
        }

        // 第一次開蓋時紀錄初始瓶身方向（可略過這段，因為我們現在用世界方向判斷）
        if (!hasRecordedInitialUp)
        {
            initialUpDirection = bottleBody.transform.up;
            hasRecordedInitialUp = true;
        }

        // 計算瓶身與世界向上方向的角度
        float tiltAngle = Vector3.Angle(bottleBody.transform.up, Vector3.up);

        if (tiltAngle > pourAngleThreshold)
        {
            if (!isPouring)
            {
                StartPouring();
            }

            // 水柱位置與朝向
            waterStream.transform.position = mouthPoint.position;

            Quaternion baseRotation = Quaternion.LookRotation(-mouthPoint.up);
            Quaternion offsetRotation = Quaternion.Euler(streamRotationOffset);
            waterStream.transform.rotation = baseRotation * offsetRotation;
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
        waterStream.SetActive(true);
    }

    void StopPouring()
    {
        isPouring = false;
        waterStream.SetActive(false);
    }
}


