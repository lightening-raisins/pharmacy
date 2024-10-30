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
    public InputField EmailInput;  // 新增信箱輸入欄位
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
        //Debug.Log("Email input value: " + email);  // 確認 EmailInput 的值

        // 檢查密碼是否為空
        if (string.IsNullOrEmpty(enterPassword) || string.IsNullOrEmpty(confirmPassword))
        {
            Debug.Log("Password is empty.");
            ErrorMessageText.text = "密碼不能為空，請輸入密碼。";
            ErrorMessagePanel.SetActive(true);
            yield break;
        }

        // 檢查兩次輸入的密碼是否一致
        if (enterPassword != confirmPassword)
        {
            //Debug.Log("Passwords do not match.");
            ErrorMessageText.text = "密碼不一致，請重新輸入。";
            ErrorMessagePanel.SetActive(true);
            yield break;
        }

        // 檢查信箱是否為空
        if (string.IsNullOrEmpty(email))
        {
            //Debug.Log("Email is empty.");
            ErrorMessageText.text = "電子郵件不能為空，請輸入電子郵件。";
            ErrorMessagePanel.SetActive(true);
            yield break;
        }

        ErrorMessagePanel.SetActive(false);

        WWWForm form = new WWWForm();
        form.AddField("registerUser", username);
        form.AddField("registerPass", enterPassword);
        form.AddField("registerEmail", email);  // 添加信箱到表單

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

                if (responseText.Contains("使用者已成功創建"))
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