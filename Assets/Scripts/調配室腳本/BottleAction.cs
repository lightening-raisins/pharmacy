using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleAction : MonoBehaviour
{
    [Header("����Ѧ�")]
    public GameObject bottleCap;       // �~�\����
    public GameObject water;           // ���W����
    public Transform bottleBody;       // �~��
    public Transform bottleMouth;      // �~�f�]�ͦ����W����m�^

    [Header("�X������")]
    public float pourAngleThreshold = 60f; // �ɭ˨��ת��e

    [Header("���W���׷L��")]
    public Vector3 streamRotationOffset = new Vector3(25f, 0f, 0f); // ���W����L��

    private bool isPouring = false;
    private bool isCapHidden = false;

    void Start()
    {
        // �w�]�������W
        if (water != null)
        {
            water.SetActive(false);
        }
    }

    void Update()
    {
        // �~�\�O�_�w�g���}
        if (!isCapHidden && !bottleCap.activeSelf)
        {
            isCapHidden = true;
        }

        if (!isCapHidden)
        {
            StopPouring();
            return;
        }

        // �ϥΥ@�ɪŶ������רӧP�_�O�_�ɭ�
        float tiltAngle = Vector3.Angle(bottleBody.up, Vector3.up);

        if (tiltAngle > pourAngleThreshold)
        {
            if (!isPouring)
            {
                StartPouring();
            }

            // �վ���W����m�P����
            if (bottleMouth != null && water != null)
            {
                water.transform.position = bottleMouth.position;

                Quaternion baseRotation = Quaternion.LookRotation(-bottleMouth.up);
                Quaternion offsetRotation = Quaternion.Euler(streamRotationOffset);
                water.transform.rotation = baseRotation * offsetRotation;
            }
        }
        else
        {
            if (isPouring)
            {
                StopPouring();
            }
        }
    }

    void StartPouring()
    {
        isPouring = true;
        if (water != null)
        {
            water.SetActive(true);
        }
    }

    void StopPouring()
    {
        isPouring = false;
        if (water != null)
        {
            water.SetActive(false);
        }
    }
}
