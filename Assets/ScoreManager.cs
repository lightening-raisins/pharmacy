using UnityEngine;
using UnityEngine.UI;  // 確保引入了這個命名空間來使用Text UI元件

public class ScoreManager : MonoBehaviour
{
    public GameObject intercomButton;  // 按對講機按鈕的物件
    public Text scoreText;  // 用來顯示分數的UI Text元件
    private bool step1Completed = false;
    private int score = 0;  // 儲存分數

    void Start()
    {
        UpdateScoreUI();  // 初始時更新一次分數顯示
    }

    void OnTriggerEnter(Collider other)
    {
        // 檢查碰撞的對象是否為VR手或控制器
        if (other.CompareTag("hand") && !step1Completed)
        {
            OnIntercomButtonPressed();
        }
    }


    // 當按下對講機按鈕時觸發
    void OnIntercomButtonPressed()
    {
        if (!step1Completed)
        {
            step1Completed = true;
            AddScore(5);  // 完成按鈕動作加5分
        }
    }

    // 添加分數並更新UI
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();  // 每次加分後都更新UI顯示
    }

    // 更新UI上顯示的分數
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
