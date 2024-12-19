using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject collision1; 
    public GameObject collision2; 
    public GameObject appear; 

    // 當物體發生碰撞時觸發
    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物體是物件A或B
        if (collision.gameObject == collision1 || collision.gameObject == collision2)
        {
            collision2.SetActive(false);

            // 顯示物件C
            appear.SetActive(true);
        }
    }
}
