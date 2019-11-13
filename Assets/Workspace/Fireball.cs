using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public float lifetime = 2;

    [HideInInspector]
    public bool launched;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (launched) {
            transform.Translate(transform.forward * Time.deltaTime * speed);

            timer += Time.deltaTime;

            if (timer > lifetime) {
                Destroy(gameObject);
            }
        }
    }
}
