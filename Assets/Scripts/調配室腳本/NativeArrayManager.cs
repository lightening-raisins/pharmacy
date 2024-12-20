using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class NativeArrayManager : MonoBehaviour
{
    public static NativeArray<int> SharedArray { get; private set; }

    void Start()
    {
        GameObject managerObject = new GameObject("NativeArrayManager");
        managerObject.AddComponent<NativeArrayManager>();
    }


    void OnDestroy()
    {
        if (SharedArray.IsCreated)
        {
            SharedArray.Dispose();
            Debug.Log("Shared NativeArray disposed.");
        }
    }
}
