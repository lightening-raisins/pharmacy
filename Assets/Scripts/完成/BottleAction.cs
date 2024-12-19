using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleAction : MonoBehaviour
{
    public GameObject bottleCap; // �~�\����
    public GameObject water; // �n��ܩ����ê����W����
    public Transform bottleBody; // �~������
    public Transform bottleTop; // �~�f��m
    public Transform waterTop; // ���W���ݪ���m�]�l����^
    public float minAngle = 50f; // ��ܤ����̤p����
    public float maxAngle = 100f; // ��ܤ����̤j����

    private bool isCapHidden = false; // �~�\�O�_����
    private Vector3 initialForward; // �~����l���e��V

    void Start()
    {
        // �O���~������l�e��V
        initialForward = bottleBody.forward;

        // �T�O water �w�]������
        if (water != null && water.activeSelf)
        {
            water.SetActive(false);
            Debug.Log("�N water �w�]������");
        }
    }

    void Update()
    {
        // �T�{�~�\�O�_����
        if (!isCapHidden && !bottleCap.activeSelf)
        {
            isCapHidden = true;
        }

        // �p�G�~�\�w���áA�ˬd���׽d�����ܩ����ä��W
        if (isCapHidden)
        {
            // �p��~����e��V�P��l��V�����׮t
            float angleDifference = Vector3.Angle(initialForward, bottleBody.forward);

            // �p�G���׮t�F��ؼШ��סA��ܪ���A�_�h����
            if (angleDifference >= minAngle && angleDifference <= maxAngle)
            {
                AdjustWaterFlowDirection();
                ShowWater();
            }
            else
            {
                HideWater();
            }
        }
    }

    // �վ���y��V�A�Ϩ�O���¤U�A�ù�����W��m
    void AdjustWaterFlowDirection()
    {
        if (water != null && bottleTop != null && waterTop != null)
        {
            // �p����W����m�����A�� waterTop �P�~�f���
            Vector3 offset = bottleTop.position - waterTop.position;
            water.transform.position += offset;

            // ���]���y����O�q�~�f�o�X�A�åB�ݭn�����y�l�׫��V�U��
            Vector3 bottleDownDirection = -bottleBody.up; // �T�O���W�¦V�~�l���U��
            water.transform.rotation = Quaternion.LookRotation(bottleDownDirection);
        }
    }

    // ��ܪ����޿�
    void ShowWater()
    {
        if (water != null && !water.activeSelf)
        {
            water.SetActive(true);
        }
    }

    // ���ê����޿�
    void HideWater()
    {
        if (water != null && water.activeSelf)
        {
            water.SetActive(false);
        }
    }
}