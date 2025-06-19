using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomInteraction : MonoBehaviour
{
    private bool sweptByBroom = false;

    public GameObject objectToShow;
    public GameObject objectToHide1;
    public GameObject tips;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Broom"))
        {
            sweptByBroom = true;
            Debug.Log("小掃把碰觸到碎片");
        }

        if (collision.gameObject.CompareTag("Dustpan") && sweptByBroom)
        {
            Debug.Log("小掃把、碎片、畚箕成功碰撞");
            if (objectToShow != null)
                objectToShow.SetActive(true);
                if (tips != null) tips.SetActive(true);

            if (objectToHide1 != null)
                objectToHide1.SetActive(false);

            // 防止重複觸發（可選）
            sweptByBroom = false;
        }
    }
}
