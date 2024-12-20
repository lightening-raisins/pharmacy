using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentCollision : MonoBehaviour
{
    public GameObject hiddenObject1;
    public GameObject hiddenObject2;
    public GameObject displayObject; // �n��ܪ�����

    private void OnCollisionEnter(Collision collision)
    {
        // �T�{�I������O�_�O����
        if (collision.gameObject.CompareTag("Broom"))
        {
            //Debug.Log("Collided with Broom!");

            // ���÷�e����
            SetObjectVisibility(gameObject, false);

            // ���� hiddenObject1
            if (hiddenObject1 != null)
            {
                SetObjectVisibility(hiddenObject1, false);
                //Debug.Log("Hidden Object 1 has been deactivated.");
            }
            else
            {
                //Debug.Log("Hidden Object 1 is null!");
            }

            // ���� hiddenObject2
            if (hiddenObject2 != null)
            {
                SetObjectVisibility(hiddenObject2, false);
                //Debug.Log("Hidden Object 2 has been deactivated.");
            }
            else
            {
                //Debug.Log("Hidden Object 2 is null!");
            }

            // ��� displayObject
            if (displayObject != null)
            {
                SetObjectVisibility(displayObject, true);
                displayObject.transform.position = transform.position; // �T�O�s���󪺦�m�P�H���ۦP
                //Debug.Log("Display Object has been activated.");
            }
            else
            {
                //Debug.Log("Display Object is null!");
            }
        }
    }

    private void SetObjectVisibility(GameObject obj, bool isVisible)
    {
        if (obj != null)
        {
            // ����Ҧ� Renderer �ե�
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = isVisible; // �]�w��ܩ�����
                }
            }

            // �T�O��Ӫ��󪺱ҥΪ��A�ŦX�ؼ�
            obj.SetActive(isVisible);
        }
    }
}
