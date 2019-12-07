using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthOrb : Projectile
{
    public GameObject[] vegetationPrefabs;

    float growTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && Input.GetAxis("Fire2") > 0) {            
            Launch();
        }

        UpdateProjectile();

        growTimer += Time.deltaTime;

        if (launched) {
            if (growTimer > 0.33f) {
                RaycastHit hit;
                Physics.Raycast(transform.position, Vector3.down, out hit);

                Vector3 randomOffset = Random.insideUnitSphere;
                randomOffset.y = 0;
                Instantiate(vegetationPrefabs[Random.Range(0, vegetationPrefabs.Length)], hit.point + randomOffset, Random.rotation);

                growTimer = 0;
            }
        }
    }
}
