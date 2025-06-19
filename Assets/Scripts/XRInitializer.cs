using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using Valve.VR;

public class XRInitializer : MonoBehaviour
{
    public GameObject steamVRObjectsRoot; // 包住所有 SteamVR 的物件（例如相機Rig等）

    IEnumerator Start()
    {
        Debug.Log("[XRInitializer] 開始初始化 XR...");

        // 確保一開始不要讓 SteamVR 行為啟動
        if (steamVRObjectsRoot != null)
            steamVRObjectsRoot.SetActive(false);

        // 初始化 XR Loader (等同 OpenVR Loader)
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("[XRInitializer] XR 初始化失敗！");
            SceneManager.LoadScene("ErrorScene");
            yield break;
        }

        XRGeneralSettings.Instance.Manager.StartSubsystems();
        Debug.Log("[XRInitializer] XR 啟動完成");

        // 等 SteamVR 啟動（會慢一點，不能太快啟用 SteamVR_Behaviour）
        yield return new WaitUntil(() => SteamVR.active);

        // 啟動 SteamVR Input 系統
        if (!SteamVR_Input.initialized)
            SteamVR_Input.Initialize();

        // 確認初始化完後再打開 SteamVR 的裝置物件
        if (steamVRObjectsRoot != null)
            steamVRObjectsRoot.SetActive(true);

        Debug.Log("[XRInitializer] SteamVR 啟動完成");
    }

    void OnDisable()
    {
        if (XRGeneralSettings.Instance.Manager != null &&
            XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            Debug.Log("[XRInitializer] XR 停止完成");
        }
    }
}
