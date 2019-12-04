using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {            
            var main = GetComponent<ParticleSystem>().main;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            
            Launch();
        }

        UpdateProjectile();
    }
}
