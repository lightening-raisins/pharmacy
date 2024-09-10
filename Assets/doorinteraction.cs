using UnityEngine;
using Valve.VR;

public class DoorInteraction : MonoBehaviour
{
    public Animator doorAnimator;  // �s���A��Animator
    public string openDoorAnimation = "door1open";  // �ʵe�W��
    public SteamVR_Action_Boolean triggerAction;  // �O���䪺��J�欰
    public SteamVR_Input_Sources handType;  // �ϥΪ���`�]����Υk��^

    private bool isHandNearDoor = false;  // �O�_�a���

    private void OnTriggerEnter(Collider other)
    {
        // �˴��O�_�O��`�I���
        if (other.CompareTag("Hand"))
        {
            isHandNearDoor = true;
            Debug.Log("��`�w�a���");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���`���}��
        if (other.CompareTag("Hand"))
        {
            isHandNearDoor = false;
            Debug.Log("��`�w���}��");
        }
    }

    void Update()
    {
        // �p�G��`�a����åB���U�O����
        if (isHandNearDoor && triggerAction.GetStateDown(handType))
        {
            doorAnimator.Play(openDoorAnimation);  // ��������}�ʵe
            Debug.Log("�����b���}");
        }
    }
}
