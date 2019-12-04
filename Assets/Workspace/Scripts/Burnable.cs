using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour {
    public float burnTime = 2;
    public ParticleSystem ps;
    public Light light;
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

        maxLight = light.intensity;
        light.gameObject.SetActive(false);
    }
	
	void Update ()
    {
        if (countdown) {
            timer += Time.deltaTime;
        
            _renderer.material.SetFloat(shaderProperty, Mathf.InverseLerp(0, burnTime, timer));
            light.intensity = (1 - Mathf.InverseLerp(0, burnTime, timer)) * maxLight;

            if (timer > burnTime) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        countdown = true;
        ps.Play();
        light.gameObject.SetActive(true);
    }
}
