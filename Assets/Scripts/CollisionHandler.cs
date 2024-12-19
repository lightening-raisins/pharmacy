using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject collision1; 
    public GameObject collision2; 
    public GameObject appear; 

    // ����o�͸I����Ĳ�o
    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I��������O����A��B
        if (collision.gameObject == collision1 || collision.gameObject == collision2)
        {
            collision2.SetActive(false);

            // ��ܪ���C
            appear.SetActive(true);
        }
    }
}
