using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class VRSceneManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return InitializeXR();

        // �o�̥i�H�� VR �����Ұʫ�n������
    }

    IEnumerator InitializeXR()
    {
        Debug.Log("[VRSceneManager] ��l�� XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("[VRSceneManager] �L�k��l�� XR Loader�I");
        }
        else
        {
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            Debug.Log("[VRSceneManager] XR �w�ҰʡI");
        }
    }

    private void OnDestroy()
    {
        // ���} VR �����ɰ��� XR
        StopXR();
    }

    public void StopXR()
    {
        Debug.Log("[VRSceneManager] ���� XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("[VRSceneManager] XR �w����I");
    }
}
