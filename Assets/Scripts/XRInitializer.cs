using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using Valve.VR;

public class XRInitializer : MonoBehaviour
{
    public GameObject steamVRObjectsRoot; // �]��Ҧ� SteamVR ������]�Ҧp�۾�Rig���^

    IEnumerator Start()
    {
        Debug.Log("[XRInitializer] �}�l��l�� XR...");

        // �T�O�@�}�l���n�� SteamVR �欰�Ұ�
        if (steamVRObjectsRoot != null)
            steamVRObjectsRoot.SetActive(false);

        // ��l�� XR Loader (���P OpenVR Loader)
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("[XRInitializer] XR ��l�ƥ��ѡI");
            SceneManager.LoadScene("ErrorScene");
            yield break;
        }

        XRGeneralSettings.Instance.Manager.StartSubsystems();
        Debug.Log("[XRInitializer] XR �Ұʧ���");

        // �� SteamVR �Ұʡ]�|�C�@�I�A����ӧֱҥ� SteamVR_Behaviour�^
        yield return new WaitUntil(() => SteamVR.active);

        // �Ұ� SteamVR Input �t��
        if (!SteamVR_Input.initialized)
            SteamVR_Input.Initialize();

        // �T�{��l�Ƨ���A���} SteamVR ���˸m����
        if (steamVRObjectsRoot != null)
            steamVRObjectsRoot.SetActive(true);

        Debug.Log("[XRInitializer] SteamVR �Ұʧ���");
    }

    void OnDisable()
    {
        if (XRGeneralSettings.Instance.Manager != null &&
            XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            Debug.Log("[XRInitializer] XR �����");
        }
    }
}
