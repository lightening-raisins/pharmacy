using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GarmentBagController : MonoBehaviour
{
    [Header("夾鏈袋本體")]
    public GameObject wholeZiplockBag;

    [Header("需要出現的物件")]
    public GameObject headCover;
    public List<GameObject> glovesList;   // 多副手套
    public GameObject shoeCover;

    [Header("各自對應的條件物件")]
    public GameObject headCoverCondition;
    public List<GameObject> glovesConditions; // 多個手套對應條件
    public GameObject shoeCoverCondition;

    [Header("提示語")]
    public GameObject tips1;
    public List<GameObject> tips2List; // 多個手套提示
    public GameObject tips3;

    [Header("手部 Pose")]
    public SteamVR_Behaviour_Pose leftHandPose;
    public SteamVR_Behaviour_Pose rightHandPose;

    private bool leftHandTouching = false;
    private bool rightHandTouching = false;

    private bool headCoverShown = false;
    private List<bool> glovesShownList = new List<bool>();
    private bool shoeCoverShown = false;

    private bool hasTouched = false;

    void Start()
    {
        // 初始化物件狀態
        headCover.SetActive(false);
        shoeCover.SetActive(false);

        foreach (var glove in glovesList)
        {
            glove.SetActive(false);
            glovesShownList.Add(false); // 初始化每副手套的顯示狀態
        }
    }

    void Update()
    {
        leftHandTouching = IsHandTouchingZipBag(leftHandPose.transform);
        rightHandTouching = IsHandTouchingZipBag(rightHandPose.transform);

        if (leftHandTouching && rightHandTouching)
        {
            if (!hasTouched)
            {
                TryShowAccessories();
                hasTouched = true;
            }
        }
        else
        {
            hasTouched = false;
        }
    }

    bool IsHandTouchingZipBag(Transform handTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(handTransform.position, 0.1f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == wholeZiplockBag)
                return true;
        }
        return false;
    }

    void TryShowAccessories()
    {
        Vector3 baseSpawnPosition = wholeZiplockBag.transform.position + new Vector3(0, 0.25f, 0.25f);

        // 帽套
        if (headCoverCondition != null && headCoverCondition.activeSelf && !headCoverShown)
        {
            headCover.SetActive(true);
            if (tips1 != null) tips1.SetActive(true);
            headCover.transform.position = baseSpawnPosition;
            headCoverShown = true;
            Debug.Log("帽套出現！");
        }

        // 多副手套
        for (int i = 0; i < glovesList.Count; i++)
        {
            if (i < glovesConditions.Count && i < glovesShownList.Count && !glovesShownList[i])
            {
                if (glovesConditions[i] != null && glovesConditions[i].activeSelf)
                {
                    Vector3 spawnOffset = new Vector3(0.2f * i, 0, 0); // 每副手套稍微錯開擺放
                    glovesList[i].SetActive(true);
                    glovesList[i].transform.position = baseSpawnPosition + spawnOffset;

                    if (i < tips2List.Count && tips2List[i] != null)
                        tips2List[i].SetActive(true);

                    glovesShownList[i] = true;
                    Debug.Log($"第 {i + 1} 副手套出現！");
                }
            }
        }

        // 鞋套
        if (shoeCoverCondition != null && shoeCoverCondition.activeSelf && !shoeCoverShown)
        {
            shoeCover.SetActive(true);
            if (tips3 != null) tips3.SetActive(true);
            shoeCover.transform.position = baseSpawnPosition + new Vector3(0, 0, -0.2f);
            shoeCoverShown = true;
            Debug.Log("鞋套出現！");
        }
    }
}
