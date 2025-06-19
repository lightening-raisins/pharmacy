using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundingBoxLimiter : MonoBehaviour
{
    [Header("��J�A�� VRCamera�]�Y���۾��^")]
    public Transform vrCamera;

    [Header("�@�ɮy�ФU���\���̤p Y�]�q�`�O�a�O���ס^")]
    public float minY = 0.0f;

    void LateUpdate()
    {
        if (vrCamera == null) return;

        Vector3 worldPos = vrCamera.position;

        if (worldPos.y < minY)
        {
            float offset = minY - worldPos.y;

            // ���� Player �V�W�� offset
            transform.position += new Vector3(0, offset, 0);
        }
    }
}
