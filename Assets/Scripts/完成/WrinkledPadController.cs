using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrinkledPadController : MonoBehaviour
{
    public GameObject absorbentPad6; 
    public GameObject wrinkledPad;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hand"))
        {
            StartCoroutine(HidePadAndShowWrinkledPad());
        }
    }

    private IEnumerator HidePadAndShowWrinkledPad()
    {
        yield return new WaitForSeconds(0.5f);

        absorbentPad6.SetActive(false);
        Debug.Log("Absorbent Pad 6 hidden.");

        wrinkledPad.transform.position = absorbentPad6.transform.position;
        wrinkledPad.transform.rotation = absorbentPad6.transform.rotation;

        wrinkledPad.transform.SetParent(null);

        wrinkledPad.SetActive(true);
        //Debug.Log("Wrinkled absorbent pad shown at absorbent pad 6's position.");
    }
}
