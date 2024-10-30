using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject popupWindow;  // 提示視窗

    // 當手模型進入觸發區域時
    private void OnTriggerEnter(Collider other)
    {
        // 檢查是否是手模型碰觸了按鈕
        if (other.CompareTag("Hand"))
        {
            // 顯示提示視窗
            popupWindow.SetActive(true);
            Debug.Log("手碰到按鈕，顯示提示視窗");
        }
    }
}