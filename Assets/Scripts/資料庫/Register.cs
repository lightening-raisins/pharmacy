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
    public Button RegisterButton;
    public GameObject LoginSuccess;
    public Text ErrorMessageText;  // �s�W�����~�H��Text
    public GameObject ErrorMessagePanel; // ���hImage

    void Start()
    {
        RegisterButton.onClick.AddListener(() =>
        {
            StartCoroutine(RegisterUser(UsernameInput.text, EnterPasswordInput.text, ConfirmPasswordInput.text));
        });

        LoginSuccess.SetActive(false);
        ErrorMessagePanel.SetActive(false);  // �}�l�����ÿ��~�H��
    }

    IEnumerator RegisterUser(string username, string enterPassword, string confirmPassword)
    {
        Debug.Log("RegisterUser coroutine started.");

        // �ˬd�K�X�O�_����
        if (string.IsNullOrEmpty(enterPassword) || string.IsNullOrEmpty(confirmPassword))
        {
            Debug.Log("Password is empty.");
            ErrorMessageText.text = "Password cannot be empty. Please enter the password.";
            ErrorMessagePanel.SetActive(true);  // ��ܿ��~�H���M���hImage
            yield break;
        }

        // �ˬd�⦸��J���K�X�O�_�@�P
        if (enterPassword != confirmPassword)
        {
            Debug.Log("Passwords do not match.");
            ErrorMessageText.text = "Passwords do not match. Please re-enter.";
            ErrorMessagePanel.SetActive(true);  // ��ܿ��~�H���M���hImage
            yield break;
        }

        ErrorMessagePanel.SetActive(false);  // ���ÿ��~�H���M���hImage

        WWWForm form = new WWWForm();
        form.AddField("registerUser", username);
        form.AddField("registerPass", enterPassword);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitymysql/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("New record created successfully."))
                {
                    yield return new WaitForSeconds(0.25f);
                    LoginSuccess.SetActive(true);
                }
            }
        }
    }
}
