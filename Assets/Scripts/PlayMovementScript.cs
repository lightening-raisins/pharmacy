using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public SteamVR_Action_Vector2 input; // 二維輸入：x 軸和 z 軸
    public SteamVR_Action_Boolean AButtonAction; // A 按鍵的 Action
    public SteamVR_Action_Boolean BButtonAction; // B 按鍵的 Action
    public float speed;
    public float verticalSpeed; // 垂直移動的速度

    void Update()
    {
        // 獲取水平移動的輸入
        var localMovement = new Vector3(input.axis.x, 0, input.axis.y);

        // 垂直移動控制
        if (AButtonAction.state) // 如果 A 按鍵被按下
        {
            localMovement.y -= verticalSpeed;
        }
        if (BButtonAction.state) // 如果 B 按鍵被按下
        {
            localMovement.y += verticalSpeed;
        }

        // 將本地移動方向轉換為世界坐標系中的方向
        var worldMovement = Player.instance.hmdTransform.TransformDirection(localMovement);

        // 更新玩家的位置
        transform.position += speed * Time.deltaTime * worldMovement;
    }
}
