using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1;
    public float lifetime = 2;

    protected bool launched;
    protected float timer;

    // Update is called once per frame
    protected void UpdateProjectile()
    {
        if (launched) {
            transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);

            timer += Time.deltaTime;

            if (timer > lifetime) {
                Destroy(gameObject);
            }
        }
    }

    public void Launch() {
        launched = true;
        transform.parent = null;
    }
}
