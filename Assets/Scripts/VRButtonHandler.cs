using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRButtonHandler : MonoBehaviour
{
    public GameObject popupWindow; // 拖入你的提示窗口
    public ScoreManager scoreManager; // 拖入 ScoreManager 物件

    private bool scoreAdded = false; // 追蹤是否已經加過分數

    // 當控制器的手碰到按鈕的時候
    private void OnHandHoverBegin(Hand hand)
    {
        // 顯示提示窗口
        popupWindow.SetActive(true);

        // 如果還沒加過分數，加分
        if (!scoreAdded && scoreManager != null)
        {
            scoreManager.AddScore(5);
            scoreAdded = true; // 記錄已經加過分數
        }
        else if (scoreManager == null)
        {
            Debug.LogError("ScoreManager is not assigned in VRButtonHandler.");
        }
    }

    private void OnHandHoverEnd(Hand hand)
    {
        // 隱藏提示窗口 (如果需要)
        // popupWindow.SetActive(false);
    }
}
