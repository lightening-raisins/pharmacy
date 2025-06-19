using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundingBoxLimiter : MonoBehaviour
{
    [Header("拖入你的 VRCamera（頭盔相機）")]
    public Transform vrCamera;

    [Header("世界座標下允許的最小 Y（通常是地板高度）")]
    public float minY = 0.0f;

    void LateUpdate()
    {
        if (vrCamera == null) return;

        Vector3 worldPos = vrCamera.position;

        if (worldPos.y < minY)
        {
            float offset = minY - worldPos.y;

            // 把整個 Player 向上推 offset
            transform.position += new Vector3(0, offset, 0);
        }
    }
}
