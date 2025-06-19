using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TissueManager : MonoBehaviour
{
    public GameObject[] wetTissues;  // �g�㪺����Ȱ}�C
    public GameObject wetTissue;     // ��@������Ȫ���
    public GameObject[] wrinkledTissues;  // �x�s�Ҧ��K�P����Ȫ���
    public GameObject tips1;
    public GameObject tips2;

    private bool isTouched = false;  // �ΨӧP�_�O�_�w�g�I���
    private bool isWetEffectComplete = false; // �ΨӧP�_�g��ĪG�O�_����
    private bool isRotationStarted = false;  // �ΨӧP�_����ĪG�O�_�w�g�Ұ�
    private Coroutine displayCoroutine; // �x�s��{���ޥΡA��K����
    private Coroutine rotateCoroutine; // �x�s����ĪG����{�ޥ�

    void Start()
    {
        // ��l�Ʈ����éҦ��g�������
        UpdateWetTissueDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag("water") && !isTouched)  // ������ȸI���
        {
            Debug.Log("Water detected, starting absorbent pad animation.");
            isTouched = true;  // �]�m�w�g�I���

            other.gameObject.SetActive(false);  // ���� "��" ����

            // �}�l��ܪg������Ȫ���{
            StartCoroutine(DisplayPads());
        }
        else if (other.CompareTag("hand") && isWetEffectComplete && !isRotationStarted)  // �p�G�O��Ĳ�o�B�g��ĪG�����B���}�l����
        {
            isRotationStarted = true;  // �аO����ĪG�w�Ұ�

            // �}�l��ܱ���ĪG
            rotateCoroutine = StartCoroutine(RotateWrinkledTissues());
        }
    }

    private IEnumerator DisplayPads()
    {
        for (int i = 0; i < wetTissues.Length; i++)
        {
            wetTissues[i].SetActive(true);  
            //Debug.Log("Activated wet tissue index: " + i);

            yield return new WaitForSeconds(1f);  

            // ���ëe�@������ȡ]�b��ܤU�@�Ӥ��e�^
            if (i < wetTissues.Length - 1)
            {
                wetTissues[i].SetActive(false);  
            }
        }

        // �̫�@������ȫO�����
        wetTissues[wetTissues.Length - 1].SetActive(true);
        if (tips1 != null) tips1.SetActive(true);
        isWetEffectComplete = true;  
    }

    private IEnumerator RotateWrinkledTissues()
    {
        wetTissue.SetActive(false);
        Debug.Log("WetTissue hidden.");


        for (int i = 0; i < wrinkledTissues.Length; i++)
        {
            if (!wrinkledTissues[i].activeSelf)
            {
                wrinkledTissues[i].SetActive(true);
            }
            //Debug.Log("Activated wrinkled tissue index: " + i);


            yield return new WaitForSeconds(1f);

            // ���ëe�@�Ӫ���]�T�O�u���@�Ӫ�����ܡ^
            if (i < wrinkledTissues.Length - 1)
            {
                wrinkledTissues[i].SetActive(false);
            }
        }

        // �O�ҳ̫�@�Ӫ���O�����
        wrinkledTissues[wrinkledTissues.Length - 1].SetActive(true);
        if (tips2 != null) tips2.SetActive(true);
    }

    // ��s�ثe��ܪ������
    void UpdateWetTissueDisplay()
    {
        for (int i = 0; i < wetTissues.Length; i++)
        {
            wetTissues[i].SetActive(i == 0);  // �u��ܲĤ@�i�g�㪺�����
        }
    }
}
