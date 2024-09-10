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

        // �ϥ� Ink �겣����r��l�� Story
        story = new Story(inkAsset.text);
    }

    public void DisplayAllText()
    {
        if (story == null)
        {
            Debug.LogError("Story is null.");
            return;
        }

        // ��ܩҦ���ܤ�r
        while (story.canContinue)
        {
            dialogText.text += story.Continue();
            dialogText.text += "\n"; // ����H�Ϥ����P��ܬq��
        }
        dialogText.horizontalOverflow = HorizontalWrapMode.Wrap;
    }
}
