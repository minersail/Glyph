using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject flames;

    private void OnTriggerEnter(Collider other) {
        flames.SetActive(true);
    }
}
