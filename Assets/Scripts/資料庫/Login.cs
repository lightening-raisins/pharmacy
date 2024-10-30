using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Button ForgotPasswordButton;
    public GameObject ForgotPasswordPanel;
    public InputField EmailInput;
    public Button SearchEmailButton;
    public Text ErrorMessageText;
    public GameObject ErrorMessagePanel;


    void Start()
    {
        if (gameObject.activeInHierarchy)
        {
            LoginButton.onClick.AddListener(() =>
            {
                StartCoroutine(Log(UsernameInput.text, PasswordInput.text));
            });

            ForgotPasswordButton.onClick.AddListener(() =>
            {
                ForgotPasswordPanel.SetActive(true);
            });

            ErrorMessagePanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Login GameObject is not active in hierarchy.");
        }
    }

    public IEnumerator Log(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitymysql/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                ErrorMessageText.text = "Network error. Please try again.";
                ErrorMessagePanel.SetActive(true);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);

                Debug.Log(response.loginResult);

                if (response.loginResult == "登入成功")
                {
                    yield return new WaitForSeconds(0.5f);
                    SceneManager.LoadScene("ModeChoose");
                }
                else if (response.loginResult == "帳號或密碼錯誤")
                {
                    ErrorMessageText.text = "帳號或密碼錯誤，請重新輸入。";
                    ErrorMessagePanel.SetActive(true);
                }
                else
                {
                    ErrorMessageText.text = "使用者名稱不存在，請重新輸入。";
                    ErrorMessagePanel.SetActive(true);
                }
            }
        }
    }

    [System.Serializable]
    public class LoginResponse
    {
        public string storedPassword;
        public string loginResult;
    }
}
