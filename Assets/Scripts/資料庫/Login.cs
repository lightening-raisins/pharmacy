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

    void Start()
    {
        LoginButton.onClick.AddListener(() =>
        {
            StartCoroutine(Log(UsernameInput.text, PasswordInput.text));
        });
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
            }
            else
            {
                // �ѪRJSON�T��
                string jsonResponse = www.downloadHandler.text;
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);

                // ��ܵ��G
                Debug.Log(response.loginResult);

                // �ھ��T�����G��������
                if (response.loginResult == "Login Success.")
                {
                    yield return new WaitForSeconds(0.5f);
                    SceneManager.LoadScene("ModeChoose");
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
