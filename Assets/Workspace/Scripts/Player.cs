using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1;
    public Camera playerCam;

    public GameObject fireballPrefab;
    public GameObject lightPrefab;
    private GameObject fireball;
    private GameObject pointLight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed);
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);        
        playerCam.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (fireball) {
                ThrowFireball();
            }

            if (pointLight) {
                ThrowLight();
            }
        }
    }

    public void SpawnFireball() {
        fireball = Instantiate(fireballPrefab, transform.Find("Hand"));
        var main = fireball.GetComponent<ParticleSystem>().main;
        main.simulationSpace = ParticleSystemSimulationSpace.Local;
    }

    public void ThrowFireball() {
        var main = fireball.GetComponent<ParticleSystem>().main;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        
        fireball.GetComponent<Fireball>().Launch();
        fireball = null;
    }

    public void SpawnLight() {
        pointLight = Instantiate(lightPrefab, transform.Find("Hand"));
    }

    public void ThrowLight() {        
        pointLight.GetComponent<Lightball>().Launch();
        pointLight = null;
    }
}
