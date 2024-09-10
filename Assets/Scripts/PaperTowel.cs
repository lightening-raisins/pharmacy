using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperTowel : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    private void OnCollisionEnter(Collision collision)
    {
        canvas.SetActive(false);

        if (collision.gameObject.name == "¯}¸H")
        {
            Debug.Log("Paper towel collided with breakable object.");
            canvas.SetActive(true);
        }
    }
}
