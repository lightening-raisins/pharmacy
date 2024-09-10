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
    public Text ErrorMessageText;  // 新增的錯誤信息Text
    public GameObject ErrorMessagePanel; // 父層Image

    void Start()
    {
        RegisterButton.onClick.AddListener(() =>
        {
            StartCoroutine(RegisterUser(UsernameInput.text, EnterPasswordInput.text, ConfirmPasswordInput.text));
        });

        LoginSuccess.SetActive(false);
        ErrorMessagePanel.SetActive(false);  // 開始時隱藏錯誤信息
    }

    IEnumerator RegisterUser(string username, string enterPassword, string confirmPassword)
    {
        Debug.Log("RegisterUser coroutine started.");

        // 檢查密碼是否為空
        if (string.IsNullOrEmpty(enterPassword) || string.IsNullOrEmpty(confirmPassword))
        {
            Debug.Log("Password is empty.");
            ErrorMessageText.text = "Password cannot be empty. Please enter the password.";
            ErrorMessagePanel.SetActive(true);  // 顯示錯誤信息和父層Image
            yield break;
        }

        // 檢查兩次輸入的密碼是否一致
        if (enterPassword != confirmPassword)
        {
            Debug.Log("Passwords do not match.");
            ErrorMessageText.text = "Passwords do not match. Please re-enter.";
            ErrorMessagePanel.SetActive(true);  // 顯示錯誤信息和父層Image
            yield break;
        }

        ErrorMessagePanel.SetActive(false);  // 隱藏錯誤信息和父層Image

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
