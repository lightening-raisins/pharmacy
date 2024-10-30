using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TrashCanController : MonoBehaviour
{
    public GameObject lid; // 桶蓋
    public GameObject table; // 桌子
    private bool hasThrowable = false; // 是否已經添加了 Throwable

    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物件是否是桌子
        if (collision.gameObject == table && !hasThrowable)
        {
            // 確保桶蓋有 Rigidbody 組件，否則添加
            Rigidbody lidRigidbody = lid.GetComponent<Rigidbody>();
            if (lidRigidbody == null)
            {
                lidRigidbody = lid.AddComponent<Rigidbody>();
            }

            // 確保桶蓋有 Interactable 組件，否則添加
            Interactable lidInteractable = lid.GetComponent<Interactable>();
            if (lidInteractable == null)
            {
                lid.AddComponent<Interactable>();
            }

            // 開始協程，延遲 3 秒後添加 Throwable
            StartCoroutine(AddThrowableAfterDelay(3f));
        }
    }

    // 協程，延遲添加 Throwable
    private IEnumerator AddThrowableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的秒數

        // 添加 Throwable 組件
        lid.AddComponent<Throwable>();

        // 設置標記，避免重複添加
        hasThrowable = true;
    }
}
