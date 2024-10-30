using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField EnterPasswordInput;
    public InputField ConfirmPasswordInput;
    public InputField EmailInput;  // �s�W�H�c��J���
    public Button RegisterButton;
    public GameObject LoginSuccess;
    public Text ErrorMessageText;
    public GameObject ErrorMessagePanel;

    void Start()
    {
        RegisterButton.onClick.AddListener(() =>
        {
            StartCoroutine(RegisterUser(UsernameInput.text, EnterPasswordInput.text, ConfirmPasswordInput.text, EmailInput.text));
        });

        LoginSuccess.SetActive(false);
        ErrorMessagePanel.SetActive(false);
    }

    IEnumerator RegisterUser(string username, string enterPassword, string confirmPassword, string email)
    {
        //Debug.Log("RegisterUser coroutine started.");
        //Debug.Log("Email input value: " + email);  // �T�{ EmailInput ����

        // �ˬd�K�X�O�_����
        if (string.IsNullOrEmpty(enterPassword) || string.IsNullOrEmpty(confirmPassword))
        {
            Debug.Log("Password is empty.");
            ErrorMessageText.text = "�K�X���ର�šA�п�J�K�X�C";
            ErrorMessagePanel.SetActive(true);
            yield break;
        }

        // �ˬd�⦸��J���K�X�O�_�@�P
        if (enterPassword != confirmPassword)
        {
            //Debug.Log("Passwords do not match.");
            ErrorMessageText.text = "�K�X���@�P�A�Э��s��J�C";
            ErrorMessagePanel.SetActive(true);
            yield break;
        }

        // �ˬd�H�c�O�_����
        if (string.IsNullOrEmpty(email))
        {
            //Debug.Log("Email is empty.");
            ErrorMessageText.text = "�q�l�l�󤣯ର�šA�п�J�q�l�l��C";
            ErrorMessagePanel.SetActive(true);
            yield break;
        }

        ErrorMessagePanel.SetActive(false);

        WWWForm form = new WWWForm();
        form.AddField("registerUser", username);
        form.AddField("registerPass", enterPassword);
        form.AddField("registerEmail", email);  // �K�[�H�c����

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitymysql/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log(responseText);

                if (responseText.Contains("�ϥΪ̤w���\�Ы�"))
                {
                    yield return new WaitForSeconds(0.25f);
                    LoginSuccess.SetActive(true);
                }
                else
                {
                    ErrorMessageText.text = responseText;
                    ErrorMessagePanel.SetActive(true);
                }
            }
        }
    }

}