using UnityEngine;
using Valve.VR.InteractionSystem;

public class ToolControllerNew : MonoBehaviour
{
    // �s�u�㪺Prefab
    public GameObject newToolPrefab;
    private bool isToolActive = true;
    private static int currentEquipIndex = 0; // �˳ƪ����ǯ��ޡA�q0�}�l

    public enum EquipmentType
    {
        MaskBox,
        ProtectiveSuit,
        GlovesBag,
        CapCoverBag,
        ShoeCoverBag
    }

    public EquipmentType equipmentType; // �˳ƪ�����

    void OnTriggerEnter(Collider other)
    {
        // �T�O��e�u��B��ҥΪ��A�A�B�I������H��"hand"
        if (isToolActive && other.CompareTag("hand"))
        {
            // �T�{��e�u��w�Q���A�åB�ˬd�˳ƶ���
            Interactable interactable = GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                Hand grabbingHand = interactable.attachedToHand;

                // �P�_�O�_�O�t�@����i�JĲ�o��
                if (other.gameObject != grabbingHand.gameObject)
                {
                    // �ˬd�O�_�i�H�������˳�
                    if (CanEquip())
                    {
                        // �Ѱ���e���
                        grabbingHand.DetachObject(gameObject);

                        // ���÷�e�u��
                        gameObject.SetActive(false);
                        isToolActive = false;

                        // �������s�u��
                        if (newToolPrefab != null)
                        {
                            // �b�⪺��m�ͦ��s�u��
                            GameObject newTool = Instantiate(newToolPrefab, grabbingHand.transform.position, grabbingHand.transform.rotation);

                            // �]�m�s�u�㬰������A
                            Interactable newToolInteractable = newTool.GetComponent<Interactable>();
                            if (newToolInteractable != null)
                            {
                                grabbingHand.AttachObject(newTool, GrabTypes.Grip);
                            }
                        }

                        // ��s�˳ƶ���
                        currentEquipIndex++;
                    }
                }
            }
        }
    }

    // �Ω��ˬd�O�_�i�H������e�˳�
    bool CanEquip()
    {
        // �T�O��e�˳ƬO�����Ǭ�����
        switch (equipmentType)
        {
            case EquipmentType.MaskBox:
                return currentEquipIndex == 0; // �f�n���O�Ĥ@�Ӹ˳�
            case EquipmentType.ProtectiveSuit:
                return currentEquipIndex == 1; // ���@��O�ĤG�Ӹ˳�
            case EquipmentType.GlovesBag:
                return currentEquipIndex == 2; // ��M����U�O�ĤT�Ӹ˳�
            case EquipmentType.CapCoverBag:
                return currentEquipIndex == 3; // �U�M����U�O�ĥ|�Ӹ˳�
            case EquipmentType.ShoeCoverBag:
                return currentEquipIndex == 4; // �c�M����U�O�Ĥ��Ӹ˳�
            default:
                return false;
        }
    }

    // �Ω󭫷s�ҥη�e�u��]�p�ݫO�d�u��b�������^
    public void ReactivateCurrentTool()
    {
        gameObject.SetActive(true);
        isToolActive = true;
    }
}
