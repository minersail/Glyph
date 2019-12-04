using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dim : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            GetComponent<Light>().intensity = 5f;
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            GetComponent<Light>().intensity = 0.1f;
        }
    }
}
