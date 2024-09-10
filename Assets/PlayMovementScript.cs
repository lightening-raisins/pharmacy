using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public SteamVR_Action_Vector2 input; // �G����J�Gx �b�M z �b
    public SteamVR_Action_Boolean AButtonAction; // A ���䪺 Action
    public SteamVR_Action_Boolean BButtonAction; // B ���䪺 Action
    public float speed;
    public float verticalSpeed; // �������ʪ��t��

    void Update()
    {
        // ����������ʪ���J
        var localMovement = new Vector3(input.axis.x, 0, input.axis.y);

        // �������ʱ���
        if (AButtonAction.state) // �p�G A ����Q���U
        {
            localMovement.y -= verticalSpeed;
        }
        if (BButtonAction.state) // �p�G B ����Q���U
        {
            localMovement.y += verticalSpeed;
        }

        // �N���a���ʤ�V�ഫ���@�ɧ��Шt������V
        var worldMovement = Player.instance.hmdTransform.TransformDirection(localMovement);

        // ��s���a����m
        transform.position += speed * Time.deltaTime * worldMovement;
    }
}
