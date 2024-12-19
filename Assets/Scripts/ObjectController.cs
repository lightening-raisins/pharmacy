using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ObjectController : MonoBehaviour
{
    public GameObject thing;        // ���h���� (�p�c�l)
    public GameObject lid;          // �i�H������\�l
    public GameObject targetObject; // �I���ؼЪ���
    public float delay = 5f;        // ����ɶ�
    private bool lidThrowable = false; // �аO�\�l�O�_�i���

    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I��������O�_�O�ؼЪ���
        if (collision.gameObject == targetObject && !lidThrowable)
        {
            // �T�O lid �� Rigidbody �ե�ó]�m�� kinematic�A�קK������
            Rigidbody lidRigidbody = lid.GetComponent<Rigidbody>();
            if (lidRigidbody == null)
            {
                lidRigidbody = lid.AddComponent<Rigidbody>();
            }
            lidRigidbody.isKinematic = true; // �Ȯ��� lid �������z�v�T

            // �T�O lid �� Interactable �ե�
            Interactable lidInteractable = lid.GetComponent<Interactable>();
            if (lidInteractable == null)
            {
                lid.AddComponent<Interactable>();
            }

            // ���� thing �� Interactable �M Throwable �ե�A�קK�A���
            Interactable thingInteractable = thing.GetComponent<Interactable>();
            if (thingInteractable != null)
            {
                thingInteractable.enabled = false;  // �T�� Interactable�A�קK���
            }

            Throwable thingThrowable = thing.GetComponent<Throwable>();
            if (thingThrowable != null)
            {
                Destroy(thingThrowable);  // ���� Throwable �ե�
            }

            // �}�l��{�A������� lid �i�H�Q���
            StartCoroutine(AddThrowableToLidAfterDelay(delay));
        }
    }

    // ��{�A�����K�[ Throwable ��\�l
    private IEnumerator AddThrowableToLidAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ���ݫ��w���

        // ���� lid �� kinematic ���A�A���������z�v�T
        Rigidbody lidRigidbody = lid.GetComponent<Rigidbody>();
        if (lidRigidbody != null)
        {
            lidRigidbody.isKinematic = false;
        }

        // �K�[ Throwable �ե��\�l�A�����i�H�Q���
        lid.AddComponent<Throwable>();

        // �]�m�аO�A�קK���ƾާ@
        lidThrowable = true;
    }
}
