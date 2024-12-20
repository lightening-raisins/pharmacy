using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SprayBoxLidController : MonoBehaviour
{
    public GameObject hiddenLid;  // �̪쪺�\�l (���i���)
    public GameObject displayLid;  // ��ܪ��\�l (�i���)

    private void Start()
    {
        // �T�O�}�l�ɥu�����û\�l�i���A��ܻ\�l����
        if (hiddenLid != null) hiddenLid.SetActive(true);
        if (displayLid != null) displayLid.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �ˬd�I��������O�_�O�ⳡ (�o�̨ϥ�Trigger�ӫDCollider�i���ˬd)
        if (other.CompareTag("hand"))
        {
            if (hiddenLid != null && displayLid != null)
            {
                // ���ó̪쪺�\�l
                hiddenLid.SetActive(false);

                // ��ܥi�H������\�l
                displayLid.SetActive(true);
                Debug.Log("��ܥi����\�l�I");
            }
        }
    }
}
