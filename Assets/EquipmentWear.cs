using UnityEngine;
using Valve.VR.InteractionSystem; // �ޤJ SteamVR Interaction System

public class ToolController : MonoBehaviour
{
    // ���ܩM��ܮت�UI����
    public GameObject dialogBox;
    private bool isToolActive = true;

    void OnTriggerEnter(Collider other)
    {
        // ��u��I�� BodyCollider ��Ĳ�o
        if (isToolActive && other.CompareTag("Body"))
        {
            // ���ը��o�u�㪺 Interactable �ե�
            Interactable interactable = GetComponent<Interactable>();
            if (interactable != null && interactable.attachedToHand != null)
            {
                // �p�G�u��Q����A�Ѱ����
                Hand hand = interactable.attachedToHand;
                hand.DetachObject(gameObject);
            }

            // ���äu��
            gameObject.SetActive(false);
            isToolActive = false;

            // ��ܴ��ܮ�
            if (dialogBox != null)
            {
                dialogBox.SetActive(true);
            }
        }
    }

    // �Ω󭫷s�ҥΤu��
    public void ReactivateTool()
    {
        gameObject.SetActive(true);
        isToolActive = true;

        // ���ù�ܮ�
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
        }
    }
}
