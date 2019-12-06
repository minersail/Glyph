using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthOrb : MonoBehaviour
{
    public float explodeTime = 0.5f;
    public float explodeSize = 9;
    public float fadeTime = 1;

    float originalSize;
    bool growing;
    float growTimer;
    
    Renderer _renderer;
    int shaderProperty;

    void Start() {
        originalSize = transform.localScale.x;
        _renderer = GetComponent<Renderer>();
        shaderProperty = Shader.PropertyToID("_Direction");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {            
            growing = true;
        }

        if (growing) {
            growTimer += Time.deltaTime;

            if (growTimer < explodeTime) {
                // Exponentially animate to explodeSize at explodeTime
                transform.localScale = Vector3.one * (Mathf.Pow(Mathf.Pow(explodeSize + 1, 1 / explodeTime), growTimer) - 1 + 0.4f);
            }
            else if (growTimer < explodeTime + fadeTime) {
                // x^4 for curved animation
                float distortStrength = Mathf.Pow(1 - ((growTimer - explodeTime) / fadeTime), 4);
                _renderer.material.SetVector(shaderProperty, new Vector4(distortStrength, -distortStrength, 0, 0));
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}
