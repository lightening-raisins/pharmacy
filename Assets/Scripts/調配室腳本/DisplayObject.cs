using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayObject : MonoBehaviour
{
    public GameObject objectToShow; // �n��ܪ�����
    void Start()
    {
        // �T�O�ؼЪ���@�}�l����
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }
}
