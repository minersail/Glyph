using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && Input.GetAxis("Fire2") > 0) {            
            var main = GetComponent<ParticleSystem>().main;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            
            Launch();
        }

        UpdateProjectile();
    }
}
