using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearInputFields : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField EnterPasswordInput;
    public InputField ConfirmPasswordInput;
    public InputField EmailInput;
    public GameObject LoginSuccess;

    public void ClearFields()
    {
        UsernameInput.text = "";
        EnterPasswordInput.text = "";
        ConfirmPasswordInput.text = "";
        EmailInput.text = "";
    }

    public void OnNoButtonClicked()
    {
        ClearFields();
        LoginSuccess.SetActive(false);
    }
}
