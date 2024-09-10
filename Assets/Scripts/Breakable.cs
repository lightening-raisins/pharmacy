using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject intactbottleBody;
    [SerializeField] GameObject intactbottleCap;
    [SerializeField] GameObject brokebottle;
    [SerializeField] GameObject canvas;

    BoxCollider bc;

    private void Awake()
    {
        
        intactbottleBody.SetActive(true);
        intactbottleCap.SetActive(true);
        brokebottle.SetActive(false);
        canvas.SetActive(false);

        bc = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I������H�W�٬O�_�� "Plane"
        if (collision.gameObject.name == "Plane")
        {
            Debug.Log("Collision with plane detected.");
            Break();
            canvas.SetActive(true);
        }
    }

    private void Break()
    {
        intactbottleBody.SetActive(false);
        intactbottleCap.SetActive(false);
        brokebottle.SetActive(true);

        bc.enabled = false;

        // ����a�O����m
        Vector3 floorPosition = Vector3.zero; // �o�����ӳ]�m�A���a�O����m

        // �M���Ҧ��l����]�H���^
        foreach (Transform child in brokebottle.transform)
        {
            // �p��H���P�a�O�����I
            RaycastHit hit;
            if (Physics.Raycast(child.position, Vector3.down, out hit))
            {
                // �ϥΥ��I�@���H�����s��m
                child.position = hit.point;

                // �p��H����I���I���V�q
                Vector3 toHitPoint = hit.point - child.position;

                // �N�H����m�V�����ʤ@�w�Z���A�H����H�����ͦ��d��
                float moveDistance = 0.1f; // �i�H�վ�o�ӭȥH����d��
                child.position += toHitPoint.normalized * moveDistance;
            }
            else
            {
                // �p�G�S�����I�A�N�N�H����m�]�m���a�O��m
                child.position = floorPosition;
            }

            // �H���ͦ������q�A�H�����|�q�ĪG
            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection.y = 0f; // �N y �b�]�m�� 0�A�u�b������V�W�|�q
            Vector3 randomOffset = randomDirection * Random.Range(0.05f, 0.2f);

            // �N�����q�K�[��H����m
            child.localPosition += randomOffset;
        }
    }
}