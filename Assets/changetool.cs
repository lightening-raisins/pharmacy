using UnityEngine;
using Valve.VR.InteractionSystem;

public class ToolControllerNew : MonoBehaviour
{
    // �s�u�㪺Prefab
    public GameObject newToolPrefab;
    private bool isToolActive = true;

    void OnTriggerEnter(Collider other)
    {
        // �T�O��e�u��B��ҥΪ��A
        if (isToolActive && other.CompareTag("hand"))
        {
            // ���ը��o�u�㪺Interactable�ե�
            Interactable interactable = GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                // �p�G�u��Q���A�Ѱ����
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(gameObject);

                // ���÷�e�u��
                gameObject.SetActive(false);
                isToolActive = false;

                // �������s�u��
                if (newToolPrefab != null)
                {
                    // �b�⪺��m�ͦ��s�u��
                    GameObject newTool = Instantiate(newToolPrefab, hand.transform.position, hand.transform.rotation);

                    // �]�m�s�u�㬰������A
                    Interactable newToolInteractable = newTool.GetComponent<Interactable>();
                    if (newToolInteractable != null)
                    {
                        hand.AttachObject(newTool, GrabTypes.Grip);
                    }
                }
            }
        }
    }

    // �Ω󭫷s�ҥη�e�u��]�p�ݫO�d�u��b�������^
    public void ReactivateCurrentTool()
    {
        gameObject.SetActive(true);
        isToolActive = true;
    }
}
