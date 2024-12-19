using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkSwitch : MonoBehaviour
{
    public GameObject waterObject; // ��������

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("�i�JĲ�o�ϰ쪺����: " + other.name); // �T�{Ĳ�o����W��

        // �ˬd�I�쪺����O�_�O���
        if (other.CompareTag("hand"))
        {
            //Debug.Log("���Ĳ�o���\�I"); // �T�{���ҧP�_���\

            // ��ܤ�������
            if (waterObject != null)
            {
                waterObject.SetActive(true);
                Debug.Log("��������w���");
            }
            else
            {
                Debug.LogError("waterObject �|���]�m�I");
            }
        }
    }
}
