using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1;
    public Camera playerCam;
    public Transform hand;

    public GameObject fireballPrefab;
    public GameObject lightPrefab;
    public GameObject teleportPrefab;
    public GameObject earthPrefab;
    public GameObject growthPrefab;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed);
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);        
        playerCam.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
    }

    public void Spawn(string item) {
        if (item == "fire") {
            Instantiate(fireballPrefab, hand);
        }
        else if (item == "light") {
            Instantiate(lightPrefab, hand);
        }
        else if (item == "teleport") {
            Instantiate(teleportPrefab, hand);
        }
        else if (item == "earth") {
            Instantiate(earthPrefab, hand);
        }
        else if (item == "growth") {
            Instantiate(growthPrefab, hand);           
        }
    }
}
