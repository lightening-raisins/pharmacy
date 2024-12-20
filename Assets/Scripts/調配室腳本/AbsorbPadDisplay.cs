using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AbsorbPadDisplay : MonoBehaviour
{
    public GameObject wholeMat;  // 整袋吸附墊
    public GameObject singleMat; // 單張吸附墊
    public SteamVR_Behaviour_Pose leftHandPose; // 左手的Pose
    public SteamVR_Behaviour_Pose rightHandPose; // 右手的Pose
    public Vector3 matOffset = new Vector3(0, 0.1f, 0); // 單張吸附墊的偏移量

    private bool leftHandTouching = false; // 左手是否接觸
    private bool rightHandTouching = false; // 右手是否接觸
    private bool matShown = false; // 單張吸附墊是否顯示
    private Rigidbody matRigidbody; // 單張吸附墊的Rigidbody

    void Start()
    {
        // 初始隱藏單張吸附墊
        singleMat.SetActive(false);

        matRigidbody = singleMat.GetComponent<Rigidbody>();
        if (matRigidbody != null)
        {
            matRigidbody.isKinematic = true; // 禁用物理影響
        }

        // 使用 TransformPoint 設定初始世界座標
        Vector3 initialPosition = wholeMat.transform.TransformPoint(matOffset);
        singleMat.transform.position = initialPosition;

        Debug.Log("WholeMat world position: " + wholeMat.transform.position);
        Debug.Log("Initial SingleMat world position: " + singleMat.transform.position);
    }

    void Update()
    {
        // 檢查雙手是否接觸整袋吸附墊
        leftHandTouching = leftHandPose.transform != null && IsHandTouchingMat(leftHandPose.transform);
        rightHandTouching = rightHandPose.transform != null && IsHandTouchingMat(rightHandPose.transform);

        // 如果兩隻手都碰觸到且單張吸附墊尚未顯示
        if (leftHandTouching && rightHandTouching && !matShown)
        {
            Debug.Log("Both hands are touching the mat.");
            matShown = true; // 標記單張吸附墊已顯示

            // 顯示單張吸附墊
            singleMat.SetActive(true);

            // 再次設置正確的世界位置
            Vector3 correctPosition = wholeMat.transform.TransformPoint(matOffset);
            singleMat.transform.position = correctPosition;

            // 禁用物理引擎影響
            if (matRigidbody != null)
            {
                matRigidbody.isKinematic = true;
            }

            // 輸出調試資訊
            Debug.Log("SingleMat world position after activation: " + singleMat.transform.position);
            Debug.Log("WholeMat rotation: " + wholeMat.transform.rotation.eulerAngles);
            Debug.Log("SingleMat parent: " + singleMat.transform.parent.name);
        }
    }

    // 判斷手是否接觸到"整袋吸附墊"
    bool IsHandTouchingMat(Transform handTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(handTransform.position, 0.1f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == wholeMat)
            {
                return true;
            }
        }
        return false;
    }
}
