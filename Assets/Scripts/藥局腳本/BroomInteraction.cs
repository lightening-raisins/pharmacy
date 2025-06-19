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
            Debug.Log("�p����IĲ��H��");
        }

        if (collision.gameObject.CompareTag("Dustpan") && sweptByBroom)
        {
            Debug.Log("�p����B�H���B�c�ߦ��\�I��");
            if (objectToShow != null)
                objectToShow.SetActive(true);
                if (tips != null) tips.SetActive(true);

            if (objectToHide1 != null)
                objectToHide1.SetActive(false);

            // �����Ĳ�o�]�i��^
            sweptByBroom = false;
        }
    }
}
