using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRButtonHandler : MonoBehaviour
{
    public GameObject popupWindow; // 拖入你的提示窗口
    public ScoreManager scoreManager; // 拖入 ScoreManager 實例

    // 當控制器的手碰到按鈕的時候
    private void OnHandHoverBegin(Hand hand)
    {
        // 顯示提示窗口
        popupWindow.SetActive(true);
    }

    private void OnHandHoverEnd(Hand hand)
    {
        // 隱藏提示窗口 (如果需要)
        // popupWindow.SetActive(false);
    }

    // 當手碰到按鈕並按下時
    private void OnHandClick(Hand hand)
    {
        // 加分
        scoreManager.AddScore(5); // 假設你已經在 ScoreManager 中定義了 AddScore 方法

        // 可以在這裡顯示提示窗口的內容
        popupWindow.SetActive(true);
    }
}
    