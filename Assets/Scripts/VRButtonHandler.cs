using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRButtonHandler : MonoBehaviour
{
    public GameObject popupWindow; // 拖入你的提示窗口
    public ScoreManager scoreManager; // 拖入 ScoreManager 物件

    // 當控制器的手碰到按鈕的時候
    private void OnHandHoverBegin(Hand hand)
    {
        // 顯示提示窗口
        popupWindow.SetActive(true);

        // 加 5 分
        if (scoreManager != null)
        {
            scoreManager.AddScore(5);
        }
        else
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
