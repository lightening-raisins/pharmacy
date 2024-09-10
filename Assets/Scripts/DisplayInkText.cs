using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DisplayInkText : MonoBehaviour
{
    public Text dialogText;
    public TextAsset inkAssets;
    Story story = null;

    void Start()
    {
        StartDialog(inkAssets);
        DisplayAllText();
    }

    public void StartDialog(TextAsset inkAsset)
    {
        if (story != null)
        {
            return;
        }

        // 使用 Ink 資產的文字初始化 Story
        story = new Story(inkAsset.text);
    }

    public void DisplayAllText()
    {
        if (story == null)
        {
            Debug.LogError("Story is null.");
            return;
        }

        // 顯示所有對話文字
        while (story.canContinue)
        {
            dialogText.text += story.Continue();
            dialogText.text += "\n"; // 換行以區分不同對話段落
        }
        dialogText.horizontalOverflow = HorizontalWrapMode.Wrap;
    }
}
