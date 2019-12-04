using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour {

    public float spawnEffectTime = 2;
    public AnimationCurve fadeIn;

    ParticleSystem ps;
    Renderer _renderer;

    int shaderProperty;
    float progress;

	void Start ()
    {
        shaderProperty = Shader.PropertyToID("_Progress");
        _renderer = GetComponent<Renderer>();
        ps = GetComponentInChildren <ParticleSystem>();

        var main = ps.main;
        main.duration = spawnEffectTime;
    }
	
	void Update ()
    {
        progress -= Time.deltaTime;
        
        progress = Mathf.Clamp(progress, 0, spawnEffectTime);

        _renderer.material.SetFloat(shaderProperty, fadeIn.Evaluate( Mathf.InverseLerp(0, spawnEffectTime, progress)));  
    }

    public void StartTeleport() {
        ps.Play();
    }

    public void InterruptTeleport() {
        ps.Stop();
    }

    public void ChannelTeleport() {
        progress += 2 * Time.deltaTime;
    }

    public float GetChannelProgress() {
        return progress / spawnEffectTime;
    }
}
