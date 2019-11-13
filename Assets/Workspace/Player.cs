using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime, 0, Input.GetAxis("Vertical") * Time.deltaTime);

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
        
        fireball.GetComponent<Fireball>().launched = true;
        fireball.transform.parent = null;
        fireball = null;
    }

    public void SpawnLight() {
        pointLight = Instantiate(lightPrefab, transform.Find("Hand"));
    }

    public void ThrowLight() {        
        pointLight.GetComponent<Fireball>().launched = true;
        pointLight.transform.parent = null;
        pointLight = null;
    }
}
