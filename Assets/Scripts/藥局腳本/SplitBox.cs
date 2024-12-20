using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBox : MonoBehaviour
{
    public GameObject emptySprinklerBox;
    public GameObject fullSprinklerBox;

    private void Start()
    {
        // �T�O�}�l�ɥu���ż��x�c�i��
        if (emptySprinklerBox != null) SetObjectVisibility(emptySprinklerBox, true);
        if (fullSprinklerBox != null) SetObjectVisibility(fullSprinklerBox, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I��������O�_�O�Ȥl�]�ھ� Tag �P�_�^
        if (collision.gameObject.CompareTag("MedicineCabinet"))
        {
            if (emptySprinklerBox != null && fullSprinklerBox != null)
            {
                // ���êż��x�c����ܺ����x�c
                SetObjectVisibility(emptySprinklerBox, false);

                SetObjectVisibility(fullSprinklerBox, true);

                Debug.Log("�˺����x�c��ܡI");
            }
        }
    }

    private void SetObjectVisibility(GameObject obj, bool isVisible)
    {
        // �p�G���� Renderer �ե�
        if (obj != null)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = isVisible;
                }
            }

            // �ҥΩθT�θI��
            Collider[] colliders = obj.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                if (collider != null)
                {
                    collider.enabled = isVisible;
                }
            }

            // �ҥΩθT�Ϊ��󥻨�
            obj.SetActive(isVisible);
        }
    }
}
