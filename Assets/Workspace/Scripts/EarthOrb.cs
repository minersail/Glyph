using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthOrb : Projectile
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {            
            Launch();
        }

        UpdateProjectile();
    }
}
