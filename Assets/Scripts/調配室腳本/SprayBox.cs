using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SprayBox : MonoBehaviour
{
    public GameObject emptySprinklerBox;
    public GameObject fullSprinklerBox;

    private void Start()
    {
        // �T�O�}�l�ɥu���ż��x�c�i��
        if (emptySprinklerBox != null) emptySprinklerBox.SetActive(true);
        if (fullSprinklerBox != null) fullSprinklerBox.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I��������O�_�O�Ȥl�]�ھ� Tag �P�_�^
        if (collision.gameObject.CompareTag("Chair"))
        {
            if (emptySprinklerBox != null && fullSprinklerBox != null)
            {
                // ���o�ż��x�c����m�M����
                Vector3 emptyBoxPosition = emptySprinklerBox.transform.position;
                Quaternion emptyBoxRotation = emptySprinklerBox.transform.rotation;

                // ���êż��x�c
                emptySprinklerBox.SetActive(false);

                fullSprinklerBox.SetActive(true);
                Debug.Log("�˺����x�c��ܡI");

            }
        }
    }
}
