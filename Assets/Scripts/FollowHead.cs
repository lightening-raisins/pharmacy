using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    public Transform vrCamera;   // ��J�A�� VRCamera
    public Vector3 offset = new Vector3(0f, -0.15f, 0.1f); // �ھ��Y���۹��m�վ�

    void LateUpdate()
    {
        if (vrCamera != null)
        {
            transform.position = vrCamera.position + vrCamera.TransformDirection(offset);
            transform.rotation = vrCamera.rotation;
        }
    }
}
