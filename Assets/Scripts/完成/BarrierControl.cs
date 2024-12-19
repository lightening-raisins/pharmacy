using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BarrierControl : MonoBehaviour
{
    public GameObject foldingBarricade; // �P�|����u����
    public GameObject button1; // �Ĥ@�ӫ��s
    public GameObject button2; // �ĤG�ӫ��s
    public GameObject objectToShow; // �n��ܪ�����

    private bool isBarricadeHidden = false; // �ΨӧP�_�P�|����u�O�_����

    void Start()
    {
        // ��l�Ʈ����ín��ܪ�����
        objectToShow.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name); // ���Ĳ�o������W��

        // ��P�|����u�IĲ�Ĥ@�ӫ��s
        if (other.gameObject == button1 && !isBarricadeHidden)
        {
            SetObjectVisibility(foldingBarricade, false); // ���úP�|����u
            isBarricadeHidden = true;
            Debug.Log("Folding barricade hidden");
        }

        // ����N����IĲ��ĤG�ӫ��s�A��ܪ���
        else if (other.gameObject == button2 && isBarricadeHidden)
        {
            objectToShow.SetActive(true); // ��ܪ���
            button1.SetActive(false); // ���òĤ@�ӫ��s
            button2.SetActive(false); // ���òĤG�ӫ��s
            Debug.Log("objectToShow is now visible");
        }
    }

    // �������ܪ��A
    private void SetObjectVisibility(GameObject obj, bool isVisible)
    {
        // �p�G���� Renderer �ե�
        if (obj != null)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = isVisible;
                }
            }
        }
    }
}
