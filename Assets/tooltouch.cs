using UnityEngine;

public class WearableEquipment : MonoBehaviour
{
    public Transform attachPoint; // �˳ƱN�n���[����m�]����W���Y�ӳ���^

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody")) // �T�O�I�쪺�O���a������
        {
            // �]�m�����骺�l����
            transform.SetParent(attachPoint);
            transform.localPosition = Vector3.zero; // �վ��m
            transform.localRotation = Quaternion.identity; // �վ����

            // ���ø˳ƪ��ҫ�
            HideEquipment();
        }
    }

    private void HideEquipment()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false; // ���éҦ���V��
        }
    }
}
