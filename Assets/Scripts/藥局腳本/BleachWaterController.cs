using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleachWaterController : MonoBehaviour
{
    [Header("�}�դ�����")]
    public GameObject cap;               // �~�\
    public GameObject bottleBody;        // �~��
    public GameObject waterStream;       // ���W����
    public Transform mouthPoint;         // �~�f�]���W�ͦ���m�^

    [Header("�ɱקP�w")]
    public float pourAngleThreshold = 60f; // �W�L�o�Ө��״N�X��

    [Header("���W�L�ը���")]
    public Vector3 streamRotationOffset = new Vector3(25f, 0f, 0f); // �i�b Inspector �վ�L�ը���

    private bool isPouring = false;
    private bool hasRecordedInitialUp = false;
    private Vector3 initialUpDirection;

    void Update()
    {
        // �p�G�~�\�٦b�A����X���í��m��V����
        if (cap.activeSelf)
        {
            StopPouring();
            hasRecordedInitialUp = false;
            return;
        }

        // �Ĥ@���}�\�ɬ�����l�~����V�]�i���L�o�q�A�]���ڭ̲{�b�Υ@�ɤ�V�P�_�^
        if (!hasRecordedInitialUp)
        {
            initialUpDirection = bottleBody.transform.up;
            hasRecordedInitialUp = true;
        }

        // �p��~���P�@�ɦV�W��V������
        float tiltAngle = Vector3.Angle(bottleBody.transform.up, Vector3.up);

        if (tiltAngle > pourAngleThreshold)
        {
            if (!isPouring)
            {
                StartPouring();
            }

            // ���W��m�P�¦V
            waterStream.transform.position = mouthPoint.position;

            Quaternion baseRotation = Quaternion.LookRotation(-mouthPoint.up);
            Quaternion offsetRotation = Quaternion.Euler(streamRotationOffset);
            waterStream.transform.rotation = baseRotation * offsetRotation;
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
        waterStream.SetActive(true);
    }

    void StopPouring()
    {
        isPouring = false;
        waterStream.SetActive(false);
    }
}


