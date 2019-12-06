using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour {
    public float burnTime = 2;
    public ParticleSystem ps;
    public Light burnLight;
    Renderer _renderer;

    int shaderProperty;
    float timer;
    bool countdown;
    float maxLight;

	void Start ()
    {
        shaderProperty = Shader.PropertyToID("_Progress");
        _renderer = GetComponent<Renderer>();
        var main = ps.main;
        main.duration = burnTime;

        maxLight = burnLight.intensity;
        burnLight.gameObject.SetActive(false);
    }
	
	void Update ()
    {
        if (countdown) {
            timer += Time.deltaTime;
        
            _renderer.material.SetFloat(shaderProperty, Mathf.InverseLerp(0, burnTime, timer));
            burnLight.intensity = (1 - Mathf.InverseLerp(0, burnTime, timer)) * maxLight;

            if (timer > burnTime + 2.0f) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        countdown = true;
        ps.Play();
        burnLight.gameObject.SetActive(true);
    }
}
