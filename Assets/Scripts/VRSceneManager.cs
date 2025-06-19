using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class VRSceneManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return InitializeXR();

        // 這裡可以放 VR 場景啟動後要做的事
    }

    IEnumerator InitializeXR()
    {
        Debug.Log("[VRSceneManager] 初始化 XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("[VRSceneManager] 無法初始化 XR Loader！");
        }
        else
        {
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            Debug.Log("[VRSceneManager] XR 已啟動！");
        }
    }

    private void OnDestroy()
    {
        // 離開 VR 場景時停止 XR
        StopXR();
    }

    public void StopXR()
    {
        Debug.Log("[VRSceneManager] 停止 XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("[VRSceneManager] XR 已停止！");
    }
}
