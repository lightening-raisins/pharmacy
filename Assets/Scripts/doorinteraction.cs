using UnityEngine;
using Valve.VR;

public class DoorInteraction : MonoBehaviour
{
    public Animator doorAnimator;  // 連接你的Animator
    public string openDoorAnimation = "door1open";  // 動畫名稱
    public SteamVR_Action_Boolean triggerAction;  // 板機鍵的輸入行為
    public SteamVR_Input_Sources handType;  // 使用的手柄（左手或右手）

    private bool isHandNearDoor = false;  // 是否靠近門

    private void OnTriggerEnter(Collider other)
    {
        // 檢測是否是手柄碰到門
        if (other.CompareTag("Hand"))
        {
            isHandNearDoor = true;
            Debug.Log("手柄已靠近門");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 當手柄離開門
        if (other.CompareTag("Hand"))
        {
            isHandNearDoor = false;
            Debug.Log("手柄已離開門");
        }
    }

    void Update()
    {
        // 如果手柄靠近門並且按下板機鍵
        if (isHandNearDoor && triggerAction.GetStateDown(handType))
        {
            doorAnimator.Play(openDoorAnimation);  // 播放門打開動畫
            Debug.Log("門正在打開");
        }
    }
}
