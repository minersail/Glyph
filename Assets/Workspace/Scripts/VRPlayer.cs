using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour
{
    public ParticleSystem ps;

    ParticleSystem.EmissionModule emission;

    // Start is called before the first frame update
    void Start()
    {
        emission = ps.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Draw") > 0)
        {
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }

        Debug.Log(Input.GetAxis("Draw"));
    }
}
