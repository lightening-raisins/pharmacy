using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class MutipleOption : MonoBehaviour
{
    public Text dialogText;
    public Button[] buttons;  // 使用按鈕的陣列而非單一按鈕
    public TextAsset _inkAssets;
    Story story = null;
    public GameObject dialogSystemPanel;

    void Start()
    {
        TextAsset inkAssets = _inkAssets;
        bool success = StartDialog(inkAssets);

        if (!success)
        {
            Debug.LogError("對話啟動失敗。");
        }

        if (dialogText == null)
        {
            Debug.LogError("dialogText 在檢查器中未分配。");
        }

        if (buttons == null || buttons.Length == 0)
        {
            Debug.LogError("Buttons 在檢查器中未分配或陣列為空。");
        }

        // 開始對話
        NextDialog();
    }

    public bool StartDialog(TextAsset inkAssets)
    {
        if (story != null)
        {
            return false;
        }

        // 使用Ink資產的文字初始化Story
        story = new Story(inkAssets.text);
        return true;
    }

    public void NextDialog()
    {
        if (story == null) return;
        if (!story.canContinue && story.currentChoices.Count == 0)
        { //如果story不能繼續 && 沒有選項，則代表對話結束
            Debug.Log("Dialog End");
            story = null;
            return;
        }
        if (story.currentChoices.Count > 0) SetChoices(); // 取得目前對話選項數量，如果 > 0 則設定選項按鈕
        if (story.canContinue) dialogText.text = story.ContinueMaximally();
    }

    private void SetChoices()
    {
        Debug.Log("Number of choices: " + story.currentChoices.Count);
        // 遍歷可用的按鈕和選擇
        for (int i = 0; i < buttons.Length && i < story.currentChoices.Count; i++)
        {
            Debug.Log("Processing button index: " + i);
            if (buttons[i] != null)
            {
                buttons[i].gameObject.SetActive(true);

                // 獲取 Text 組件
                Text buttonText = buttons[i].GetComponentInChildren<Text>();

                if (buttonText != null)
                {
                    // 清空之前的文本
                    buttonText.text = "";

                    // 獲取 Ink 選項文本
                    string choiceText = story.currentChoices[i].text.Trim();
                    Debug.Log("Choice Text for Button " + i + ": " + choiceText);

                    // 確保選項文本不為空
                    if (!string.IsNullOrEmpty(choiceText))
                    {
                        buttonText.text = choiceText;
                    }
                    else
                    {
                        Debug.LogError("Choice text is null or empty for button at index: " + i);
                    }
                }
                else
                {
                    Debug.LogError("Text component not found in the button at index: " + i);
                }
            }
            else
            {
                Debug.LogError("Button not found at index: " + i);
            }
        }
    }


    public void MakeChoice(int index)
    {
        if (story != null && index >= 0 && index < story.currentChoices.Count)
        {
            string selectedChoiceText = story.currentChoices[index].text.Trim();

            // 檢查所選擇的選項是否為「結束教學」
            if (selectedChoiceText == "結束教學")
            {
                // 隱藏 DialogSystem 面板
                if (dialogSystemPanel != null)
                {
                    dialogSystemPanel.SetActive(false);
                }
            }

            story.ChooseChoiceIndex(index);

            // 選擇後隱藏按鈕
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }

            // 顯示下一段文字
            NextDialog();

            // 重新設置按鈕的可見性
            SetChoices();
        }
        else
        {
            Debug.LogError("無效的選擇索引: " + index);
        }
    }

}
