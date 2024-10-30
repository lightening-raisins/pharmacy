using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TrashCanController : MonoBehaviour
{
    public GameObject lid; // ��\
    public GameObject table; // ��l
    private bool hasThrowable = false; // �O�_�w�g�K�[�F Throwable

    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I��������O�_�O��l
        if (collision.gameObject == table && !hasThrowable)
        {
            // �T�O��\�� Rigidbody �ե�A�_�h�K�[
            Rigidbody lidRigidbody = lid.GetComponent<Rigidbody>();
            if (lidRigidbody == null)
            {
                lidRigidbody = lid.AddComponent<Rigidbody>();
            }

            // �T�O��\�� Interactable �ե�A�_�h�K�[
            Interactable lidInteractable = lid.GetComponent<Interactable>();
            if (lidInteractable == null)
            {
                lid.AddComponent<Interactable>();
            }

            // �}�l��{�A���� 3 ���K�[ Throwable
            StartCoroutine(AddThrowableAfterDelay(3f));
        }
    }

    // ��{�A����K�[ Throwable
    private IEnumerator AddThrowableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ���ݫ��w�����

        // �K�[ Throwable �ե�
        lid.AddComponent<Throwable>();

        // �]�m�аO�A�קK���ƲK�[
        hasThrowable = true;
    }
}
