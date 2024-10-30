using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TestItem : MonoBehaviour
{
    private GrabItemsManager grabItemsManager;

    void Start()
    {
        // ��� GrabItemsManager �����
        grabItemsManager = FindObjectOfType<GrabItemsManager>();
        if (grabItemsManager == null)
        {
            Debug.LogError("����� GrabItemsManager�I");
        }
    }

    void OnAttachedToHand(Hand hand)
    {
        // ���~�Q����ɽե� GrabItem ��k
        grabItemsManager.GrabItem(gameObject);
    }
}
