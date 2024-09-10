using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class MutipleOption : MonoBehaviour
{
    public Text dialogText;
    public Button[] buttons;  // �ϥΫ��s���}�C�ӫD��@���s
    public TextAsset _inkAssets;
    Story story = null;
    public GameObject dialogSystemPanel;

    void Start()
    {
        TextAsset inkAssets = _inkAssets;
        bool success = StartDialog(inkAssets);

        if (!success)
        {
            Debug.LogError("��ܱҰʥ��ѡC");
        }

        if (dialogText == null)
        {
            Debug.LogError("dialogText �b�ˬd���������t�C");
        }

        if (buttons == null || buttons.Length == 0)
        {
            Debug.LogError("Buttons �b�ˬd���������t�ΰ}�C���šC");
        }

        // �}�l���
        NextDialog();
    }

    public bool StartDialog(TextAsset inkAssets)
    {
        if (story != null)
        {
            return false;
        }

        // �ϥ�Ink�겣����r��l��Story
        story = new Story(inkAssets.text);
        return true;
    }

    public void NextDialog()
    {
        if (story == null) return;
        if (!story.canContinue && story.currentChoices.Count == 0)
        { //�p�Gstory�����~�� && �S���ﶵ�A�h�N���ܵ���
            Debug.Log("Dialog End");
            story = null;
            return;
        }
        if (story.currentChoices.Count > 0) SetChoices(); // ���o�ثe��ܿﶵ�ƶq�A�p�G > 0 �h�]�w�ﶵ���s
        if (story.canContinue) dialogText.text = story.ContinueMaximally();
    }

    private void SetChoices()
    {
        Debug.Log("Number of choices: " + story.currentChoices.Count);
        // �M���i�Ϊ����s�M���
        for (int i = 0; i < buttons.Length && i < story.currentChoices.Count; i++)
        {
            Debug.Log("Processing button index: " + i);
            if (buttons[i] != null)
            {
                buttons[i].gameObject.SetActive(true);

                // ��� Text �ե�
                Text buttonText = buttons[i].GetComponentInChildren<Text>();

                if (buttonText != null)
                {
                    // �M�Ť��e���奻
                    buttonText.text = "";

                    // ��� Ink �ﶵ�奻
                    string choiceText = story.currentChoices[i].text.Trim();
                    Debug.Log("Choice Text for Button " + i + ": " + choiceText);

                    // �T�O�ﶵ�奻������
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

            // �ˬd�ҿ�ܪ��ﶵ�O�_���u�����оǡv
            if (selectedChoiceText == "�����о�")
            {
                // ���� DialogSystem ���O
                if (dialogSystemPanel != null)
                {
                    dialogSystemPanel.SetActive(false);
                }
            }

            story.ChooseChoiceIndex(index);

            // ��ܫ����ë��s
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }

            // ��ܤU�@�q��r
            NextDialog();

            // ���s�]�m���s���i����
            SetChoices();
        }
        else
        {
            Debug.LogError("�L�Ī���ܯ���: " + index);
        }
    }

}
