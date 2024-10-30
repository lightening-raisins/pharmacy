using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class GrabItemsManager : MonoBehaviour
{
    public List<GameObject> requiredItems;  
    public GameObject messagePanel;          
    private HashSet<GameObject> grabbedItems = new HashSet<GameObject>();  // �l�ܤw��������~

    void Start()
    {
        messagePanel.SetActive(false);  
    }

    // ������~����k
    public void GrabItem(GameObject item)
    {
        Debug.Log("GrabItem �Q�եΡA���~�W��: " + item.name); 

        // �ˬd�O�_�O�ݭn�����~
        if (requiredItems.Contains(item) && !grabbedItems.Contains(item))
        {
            grabbedItems.Add(item);  
            CheckAllItemsGrabbed(); 
        }
        else
        {
            Debug.Log("�����~���b�ݭn��������~�C���Τw�Q���: " + item.name);
        }
    }

    private void CheckAllItemsGrabbed()
    {

        if (grabbedItems.Count == requiredItems.Count)
        {
            ShowMessage();  
        }
        else
        {
            Debug.Log("�|������Ҧ��ݭn�����~");
        }
    }

    private void ShowMessage()
    {
        messagePanel.SetActive(true);
        Debug.Log("�Ҧ����~�w����A��ܴ��ܮ�");
    }
}
