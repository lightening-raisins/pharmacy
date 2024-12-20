using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentCollision : MonoBehaviour
{
    public GameObject hiddenObject1;
    public GameObject hiddenObject2;
    public GameObject displayObject; // 要顯示的物件

    private void OnCollisionEnter(Collision collision)
    {
        // 確認碰撞物件是否是掃帚
        if (collision.gameObject.CompareTag("Broom"))
        {
            //Debug.Log("Collided with Broom!");

            // 隱藏當前物件
            SetObjectVisibility(gameObject, false);

            // 隱藏 hiddenObject1
            if (hiddenObject1 != null)
            {
                SetObjectVisibility(hiddenObject1, false);
                //Debug.Log("Hidden Object 1 has been deactivated.");
            }
            else
            {
                //Debug.Log("Hidden Object 1 is null!");
            }

            // 隱藏 hiddenObject2
            if (hiddenObject2 != null)
            {
                SetObjectVisibility(hiddenObject2, false);
                //Debug.Log("Hidden Object 2 has been deactivated.");
            }
            else
            {
                //Debug.Log("Hidden Object 2 is null!");
            }

            // 顯示 displayObject
            if (displayObject != null)
            {
                SetObjectVisibility(displayObject, true);
                displayObject.transform.position = transform.position; // 確保新物件的位置與碎片相同
                //Debug.Log("Display Object has been activated.");
            }
            else
            {
                //Debug.Log("Display Object is null!");
            }
        }
    }

    private void SetObjectVisibility(GameObject obj, bool isVisible)
    {
        if (obj != null)
        {
            // 獲取所有 Renderer 組件
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = isVisible; // 設定顯示或隱藏
                }
            }

            // 確保整個物件的啟用狀態符合目標
            obj.SetActive(isVisible);
        }
    }
}
