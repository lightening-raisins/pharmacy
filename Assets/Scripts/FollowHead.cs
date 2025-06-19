using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    public Transform vrCamera;   // 拖入你的 VRCamera
    public Vector3 offset = new Vector3(0f, -0.15f, 0.1f); // 根據頭的相對位置調整

    void LateUpdate()
    {
        if (vrCamera != null)
        {
            transform.position = vrCamera.position + vrCamera.TransformDirection(offset);
            transform.rotation = vrCamera.rotation;
        }
    }
}
