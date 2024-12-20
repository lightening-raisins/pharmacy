using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharmacyBottleCollision : MonoBehaviour
{
    // �n��ܪ�����}�C
    public GameObject parentObject;  // ������

    private bool isTouched = false;  // �T�O���W�uĲ�o�@��

    private void Start()
    {
        if (parentObject != null)
        {
            parentObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �ˬd�O�_�O���WĲ�o�ƥ�A�åB�٨SĲ�o�L
        if (other.CompareTag("Plane") && gameObject.CompareTag("MixedStream") && !isTouched)
        {
            Debug.Log("���W���\Ĳ�o�I");
            isTouched = true;  // �T�O�uĲ�o�@��

            // ��ܤ�����]�p�G�ݭn��ܡ^
            if (parentObject != null)
            {
                parentObject.SetActive(true);
            }
        }
    }
}
