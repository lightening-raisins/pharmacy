using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public web web;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        web = GetComponent<web>();
    }

}
