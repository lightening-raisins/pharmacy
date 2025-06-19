using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbPadController : MonoBehaviour
{
    public GameObject[] absorbentPads; 
    private int currentPadIndex = 0;
    public GameObject tips;

    void Start()
    {
        UpdateAbsorbentPad();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag("SpillObject"))
        {
            //Debug.Log("SpillObject detected, switching to next absorbent pad.");

            other.gameObject.SetActive(false);
            if (tips != null) tips.SetActive(false);

            StartCoroutine(DisplayPads());
        }
        /*else
        {
            Debug.Log("Collision with non-SpillObject detected.");
        }*/
    }

    private IEnumerator DisplayPads()
    {
        for (int i = 0; i < absorbentPads.Length; i++)
        {
            absorbentPads[i].SetActive(true);  
            //Debug.Log("Activated absorbent pad index: " + i);

            yield return new WaitForSeconds(1f);

            if (i < absorbentPads.Length - 1)
            {
                absorbentPads[i].SetActive(false); 
            }
        }

        absorbentPads[absorbentPads.Length - 1].SetActive(true);
    }

    void UpdateAbsorbentPad()
    {
        for (int i = 0; i < absorbentPads.Length; i++)
        {
            absorbentPads[i].SetActive(i == currentPadIndex);
        }

        //Debug.Log("Activated absorbent pad index: " + currentPadIndex);
    }
}
