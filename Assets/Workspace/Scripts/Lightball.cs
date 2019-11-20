using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightball : Projectile
{
    private float startIntensity;
    private float startSpeed;

    void Start() {
        startIntensity = GetComponent<Light>().intensity;
        startSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProjectile();
        
        float fadeaway = Mathf.Min(1, (1 - (timer / lifetime)) * 3);

        GetComponent<Light>().intensity = startIntensity * fadeaway;
        speed = startSpeed * fadeaway;
    }
}
