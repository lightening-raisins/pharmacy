using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ObjectController : MonoBehaviour
{
    public GameObject thing;        // 父層物件 (如箱子)
    public GameObject lid;          // 可以抓取的蓋子
    public GameObject targetObject; // 碰撞目標物件
    public float delay = 5f;        // 延遲時間
    private bool lidThrowable = false; // 標記蓋子是否可抓取

    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物件是否是目標物件
        if (collision.gameObject == targetObject && !lidThrowable)
        {
            // 確保 lid 有 Rigidbody 組件並設置為 kinematic，避免它飛走
            Rigidbody lidRigidbody = lid.GetComponent<Rigidbody>();
            if (lidRigidbody == null)
            {
                lidRigidbody = lid.AddComponent<Rigidbody>();
            }
            lidRigidbody.isKinematic = true; // 暫時讓 lid 不受物理影響

            // 確保 lid 有 Interactable 組件
            Interactable lidInteractable = lid.GetComponent<Interactable>();
            if (lidInteractable == null)
            {
                lid.AddComponent<Interactable>();
            }

            // 移除 thing 的 Interactable 和 Throwable 組件，避免再抓取
            Interactable thingInteractable = thing.GetComponent<Interactable>();
            if (thingInteractable != null)
            {
                thingInteractable.enabled = false;  // 禁用 Interactable，避免抓取
            }

            Throwable thingThrowable = thing.GetComponent<Throwable>();
            if (thingThrowable != null)
            {
                Destroy(thingThrowable);  // 移除 Throwable 組件
            }

            // 開始協程，延遲後讓 lid 可以被抓取
            StartCoroutine(AddThrowableToLidAfterDelay(delay));
        }
    }

    // 協程，延遲後添加 Throwable 到蓋子
    private IEnumerator AddThrowableToLidAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定秒數

        // 取消 lid 的 kinematic 狀態，讓它受物理影響
        Rigidbody lidRigidbody = lid.GetComponent<Rigidbody>();
        if (lidRigidbody != null)
        {
            lidRigidbody.isKinematic = false;
        }

        // 添加 Throwable 組件到蓋子，讓它可以被抓取
        lid.AddComponent<Throwable>();

        // 設置標記，避免重複操作
        lidThrowable = true;
    }
}
