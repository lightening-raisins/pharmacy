using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.Mail;

public class ForgotPassword : MonoBehaviour
{
    public InputField emailInputField;
    public GameObject sendEmailObject;
    public InputField verificationCodeInputField;
    public GameObject verificationCodeObject;
    public InputField newPasswordInputField;
    public GameObject resetPasswordObject;
    public GameObject sendSucccessObject;
    public GameObject backToLoginObject;
    public Button sendEmailButton;
    public Button verificationCodeButton;
    public Button resetPasswordButton;
    public Button resendButton;

    public Text emailErrorMessageText;
    public GameObject emailErrorMessagePanel;
    public Text codeErrorMessageText;
    public GameObject codeErrorMessagePanel;
    public Text resendErrorMessageText;
    public GameObject resendErrorMessagePanel;
    public Text resetPWErrorMessageText;
    public GameObject resetPWErrorMessagePanel;

    private string verificationCode;

    void Start()
    {
        sendEmailButton.onClick.AddListener(SendVerificationEmail);
        verificationCodeButton.onClick.AddListener(VerifyCode);
        resetPasswordButton.onClick.AddListener(ResetPassword);
        resendButton.onClick.AddListener(ResendVerificationEmail);
    }

    void SendVerificationEmail()
    {
        string email = emailInputField.text;

        if (!IsValidEmail(email))
        {
            ShowEmailError("�п�J���Ī��q�l�l��C");
            return;
        }

        StartCoroutine(SendEmailRequest(email));
    }

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    IEnumerator SendEmailRequest(string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitymysql/resetPasswordRequest.php", form))
        {
            yield return www.SendWebRequest();

            Debug.Log("Response Code: " + www.responseCode);
            Debug.Log("Response Text: " + www.downloadHandler.text);

            if (www.result != UnityWebRequest.Result.Success || www.downloadHandler.text != "Verification email sent successfully.")
            {
                ShowEmailError("�z�ҿ�J���q�l�l�󤣦s�b�A�Э��s��J�C");
            }
            else
            {
                sendEmailObject.SetActive(false);
                sendSucccessObject.SetActive(true);
            }
        }
    }

    void ResendVerificationEmail()
    {
        // �����ϥΤw�g��J���q�l�l��A���ݭn�A���ˬd
        string email = emailInputField.text;

        // ���s�o�e���ҽX�ШD�A�������UI���A
        StartCoroutine(ResendEmailRequest(email));
    }

    IEnumerator ResendEmailRequest(string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitymysql/resetPasswordRequest.php", form))
        {
            yield return www.SendWebRequest();

            Debug.Log("Response Code: " + www.responseCode);
            Debug.Log("Response Text: " + www.downloadHandler.text);

            if (www.result != UnityWebRequest.Result.Success || www.downloadHandler.text != "Verification email sent successfully.")
            {
                // ��ܿ��~�T���������UI���A
                Debug.LogError("Error resending verification email: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Verification email resent successfully.");
                ShowResendSuccessMessage("���Ҷl��w���s�o�e�C");
            }
        }
    }


    void VerifyCode()
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning("ForgotPassword object is not active.");
            return;
        }

        string token = verificationCodeInputField.text;
        StartCoroutine(VerifyCodeRequest(token));
    }

    IEnumerator VerifyCodeRequest(string token)
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitymysql/verifyCode.php", form))
        {
            yield return www.SendWebRequest();

            Debug.Log("Response Code: " + www.responseCode);
            Debug.Log("Response Text: " + www.downloadHandler.text);

            if (www.result != UnityWebRequest.Result.Success || www.downloadHandler.text != "Code verified successfully.")
            {
                ShowCodeError("���ҽX��J���~�C");
            }
            else
            {
                yield return new WaitForSeconds(0.25f);
                verificationCodeObject.SetActive(false);
                resetPasswordObject.SetActive(true);
            }
        }
    }

    void ResetPassword()
    {
        string newPassword = newPasswordInputField.text;
        string token = verificationCodeInputField.text;
        StartCoroutine(ResetPasswordRequest(token, newPassword));
    }

    IEnumerator ResetPasswordRequest(string token, string newPassword)
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("password", newPassword);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unitymysql/resetPassword.php", form))
        {
            yield return www.SendWebRequest();

            Debug.Log("Response Code: " + www.responseCode);
            Debug.Log("Response Text: " + www.downloadHandler.text);

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error resetting password: " + www.error);
            }
            else
            {
                if (www.downloadHandler.text.Contains("Password reset successfully."))
                {
                    Debug.Log("Password reset successfully.");
                    resetPasswordObject.SetActive(false);
                    backToLoginObject.SetActive(true);
                }
                else
                {
                    ShowResetPWError(www.downloadHandler.text);
                }
            }
        }
    }

    void ShowEmailError(string message)
    {
        emailErrorMessageText.text = message;
        emailErrorMessagePanel.SetActive(true);
    }

    void ShowCodeError(string message)
    {
        codeErrorMessageText.text = message;
        codeErrorMessagePanel.SetActive(true);
    }

    void ShowResendSuccessMessage(string message)
    {
        resendErrorMessageText.text = message; 
        resendErrorMessagePanel.SetActive(true);
    }

    void ShowResetPWError(string message)
    {
        resetPWErrorMessageText.text = message;
        resetPWErrorMessagePanel.SetActive(true);
    }
}