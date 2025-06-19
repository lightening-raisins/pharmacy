using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GarmentBagController : MonoBehaviour
{
    [Header("����U����")]
    public GameObject wholeZiplockBag;

    [Header("�ݭn�X�{������")]
    public GameObject headCover;
    public List<GameObject> glovesList;   // �h�Ƥ�M
    public GameObject shoeCover;

    [Header("�U�۹��������󪫥�")]
    public GameObject headCoverCondition;
    public List<GameObject> glovesConditions; // �h�Ӥ�M��������
    public GameObject shoeCoverCondition;

    [Header("���ܻy")]
    public GameObject tips1;
    public List<GameObject> tips2List; // �h�Ӥ�M����
    public GameObject tips3;

    [Header("�ⳡ Pose")]
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
        // ��l�ƪ��󪬺A
        headCover.SetActive(false);
        shoeCover.SetActive(false);

        foreach (var glove in glovesList)
        {
            glove.SetActive(false);
            glovesShownList.Add(false); // ��l�ƨC�Ƥ�M����ܪ��A
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

        // �U�M
        if (headCoverCondition != null && headCoverCondition.activeSelf && !headCoverShown)
        {
            headCover.SetActive(true);
            if (tips1 != null) tips1.SetActive(true);
            headCover.transform.position = baseSpawnPosition;
            headCoverShown = true;
            Debug.Log("�U�M�X�{�I");
        }

        // �h�Ƥ�M
        for (int i = 0; i < glovesList.Count; i++)
        {
            if (i < glovesConditions.Count && i < glovesShownList.Count && !glovesShownList[i])
            {
                if (glovesConditions[i] != null && glovesConditions[i].activeSelf)
                {
                    Vector3 spawnOffset = new Vector3(0.2f * i, 0, 0); // �C�Ƥ�M�y�L���}�\��
                    glovesList[i].SetActive(true);
                    glovesList[i].transform.position = baseSpawnPosition + spawnOffset;

                    if (i < tips2List.Count && tips2List[i] != null)
                        tips2List[i].SetActive(true);

                    glovesShownList[i] = true;
                    Debug.Log($"�� {i + 1} �Ƥ�M�X�{�I");
                }
            }
        }

        // �c�M
        if (shoeCoverCondition != null && shoeCoverCondition.activeSelf && !shoeCoverShown)
        {
            shoeCover.SetActive(true);
            if (tips3 != null) tips3.SetActive(true);
            shoeCover.transform.position = baseSpawnPosition + new Vector3(0, 0, -0.2f);
            shoeCoverShown = true;
            Debug.Log("�c�M�X�{�I");
        }
    }
}
