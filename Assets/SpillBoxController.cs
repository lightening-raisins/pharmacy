using UnityEngine;

public class SpillBoxManager : MonoBehaviour
{
    public GameObject[] tools; // �u�㪫��C��
    private Vector3 lastPosition; // �W�@�V����m
    private bool isMoving; // �P�_�c�l�O�_���b����

    private void Start()
    {
        lastPosition = transform.position;
        UpdateToolPhysics(false); // �T�Τu�㪺����ĪG�]��l���R�m���A�^
    }

    private void Update()
    {
        // �ˬd�c�l�O�_���b����
        isMoving = (transform.position != lastPosition);
        lastPosition = transform.position;

        // �ھڲ��ʪ��A�վ�u�㪺���l���Y�P���z���A
        foreach (GameObject tool in tools)
        {
            if (isMoving)
            {
                // �u�㦨���c�l���l����A���H�c�l���ʡA�T�έ���
                if (tool.transform.parent != transform)
                {
                    tool.transform.SetParent(transform);
                    UpdateToolPhysics(false);
                }
            }
            else
            {
                // �u���_�ۥѪ��A�A�����O���R��ä��\���
                if (tool.transform.parent == transform)
                {
                    tool.transform.SetParent(null);
                    UpdateToolPhysics(true); // �ҥέ���ëO���R��
                }
            }
        }
    }

    // ��s�u�㪺���骬�A
    private void UpdateToolPhysics(bool enable)
    {
        foreach (GameObject tool in tools)
        {
            Rigidbody rb = tool.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = !enable; // �p�G�T�έ���A�]�m�� kinematic�A���\�R��
                if (enable)
                {
                    rb.velocity = Vector3.zero; // ����u��b�ҥέ�����~�򲾰�
                    rb.angularVelocity = Vector3.zero; // ����u�����
                }
            }
        }
    }
}
