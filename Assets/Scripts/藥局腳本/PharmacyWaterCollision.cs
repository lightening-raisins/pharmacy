using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharmacyWaterCollision : MonoBehaviour
{
    // �n��ܪ�����}�C
    public GameObject parentObject;  // ������
    public GameObject tips;

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
        if (other.CompareTag("Plane") && gameObject.CompareTag("WaterStream") && !isTouched)
        {
            Debug.Log("���W���\Ĳ�o�I");
            isTouched = true;  // �T�O�uĲ�o�@��

            // ��ܤ�����]�p�G�ݭn��ܡ^
            if (parentObject != null)
            {
                parentObject.SetActive(true);
                if (tips != null) tips.SetActive(true);
            }
        }
    }

}
