using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1;
    public Camera playerCam;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed);
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);        
        playerCam.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
    }
}
