using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject popupWindow;  // ���ܵ���

    // ���ҫ��i�JĲ�o�ϰ��
    private void OnTriggerEnter(Collider other)
    {
        // �ˬd�O�_�O��ҫ��IĲ�F���s
        if (other.CompareTag("Hand"))
        {
            // ��ܴ��ܵ���
            popupWindow.SetActive(true);
            Debug.Log("��I����s�A��ܴ��ܵ���");
        }
    }
}