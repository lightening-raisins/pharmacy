using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BoxController : MonoBehaviour
{
    public List<GameObject> objectsInsideBox; // 箱子內的物件

    private void Start()
    {
        // 在開始時禁用箱子內物件的抓取功能
        foreach (GameObject obj in objectsInsideBox)
        {
            DisableGrabbable(obj);
            DisableGravity(obj); // 禁用重力
            // 將物件放置在箱子內部
            PositionObjectInsideBox(obj);
        }
    }

    private void PositionObjectInsideBox(GameObject obj)
    {
        // 將物件放置在箱子的邊界內，這裡假設箱子是這個腳本的父物件
        Vector3 boxPosition = transform.position;
        Vector3 boxSize = GetComponent<BoxCollider>().size;

        // 隨機生成一個位置，確保在箱子內
        Vector3 newPosition = new Vector3(
            Random.Range(boxPosition.x - boxSize.x / 2, boxPosition.x + boxSize.x / 2),
            Random.Range(boxPosition.y, boxPosition.y + boxSize.y / 2), // 確保在箱子的上半部
            Random.Range(boxPosition.z - boxSize.z / 2, boxPosition.z + boxSize.z / 2)
        );

        obj.transform.position = newPosition;
    }

    // 禁用物件的 Grabbable 組件
    private void DisableGrabbable(GameObject obj)
    {
        Throwable throwable = obj.GetComponent<Throwable>();
        if (throwable != null)
        {
            throwable.enabled = false;
        }

        Interactable interactable = obj.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.enabled = false;
        }
    }

    // 啟用物件的 Grabbable 組件
    private void EnableGrabbable(GameObject obj)
    {
        Throwable throwable = obj.GetComponent<Throwable>();
        if (throwable != null)
        {
            throwable.enabled = true;
        }

        Interactable interactable = obj.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.enabled = true;
        }
    }

    // 禁用物件的重力
    private void DisableGravity(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    // 啟用物件的重力
    private void EnableGravity(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }
    }
}
