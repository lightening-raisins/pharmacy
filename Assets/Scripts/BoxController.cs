using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BoxController : MonoBehaviour
{
    public List<GameObject> objectsInsideBox; // �c�l��������

    private void Start()
    {
        // �b�}�l�ɸT�νc�l�����󪺧���\��
        foreach (GameObject obj in objectsInsideBox)
        {
            DisableGrabbable(obj);
            DisableGravity(obj); // �T�έ��O
            // �N�����m�b�c�l����
            PositionObjectInsideBox(obj);
        }
    }

    private void PositionObjectInsideBox(GameObject obj)
    {
        // �N�����m�b�c�l����ɤ��A�o�̰��]�c�l�O�o�Ӹ}����������
        Vector3 boxPosition = transform.position;
        Vector3 boxSize = GetComponent<BoxCollider>().size;

        // �H���ͦ��@�Ӧ�m�A�T�O�b�c�l��
        Vector3 newPosition = new Vector3(
            Random.Range(boxPosition.x - boxSize.x / 2, boxPosition.x + boxSize.x / 2),
            Random.Range(boxPosition.y, boxPosition.y + boxSize.y / 2), // �T�O�b�c�l���W�b��
            Random.Range(boxPosition.z - boxSize.z / 2, boxPosition.z + boxSize.z / 2)
        );

        obj.transform.position = newPosition;
    }

    // �T�Ϊ��� Grabbable �ե�
    private void DisableGrabbable(GameObject obj)
    {
        Throwable throwable = obj.GetComponent<Throwable>();
        if (throwable != null)
        {
            throwable.enabled = false;
        }

        Interactable interactable = obj.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.enabled = false;
        }
    }

    // �ҥΪ��� Grabbable �ե�
    private void EnableGrabbable(GameObject obj)
    {
        Throwable throwable = obj.GetComponent<Throwable>();
        if (throwable != null)
        {
            throwable.enabled = true;
        }

        Interactable interactable = obj.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.enabled = true;
        }
    }

    // �T�Ϊ��󪺭��O
    private void DisableGravity(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    // �ҥΪ��󪺭��O
    private void EnableGravity(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }
    }
}
