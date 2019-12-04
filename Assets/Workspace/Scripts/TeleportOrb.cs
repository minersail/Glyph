using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOrb : MonoBehaviour
{
    GameObject[] points;

    int shaderProperty;
    Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.FindGameObjectsWithTag("TeleportPoint");

        shaderProperty = Shader.PropertyToID("_Strength");
        _renderer = GetComponent<Renderer>();        

        _renderer.material.GetFloat(shaderProperty);
    }

    // Update is called once per frame
    void Update()
    {
        float proximityIntensity = 0;
        float chargeIntensity = 0;

        foreach (GameObject pointObject in points) {
            TeleportPoint point = pointObject.GetComponent<TeleportPoint>();

            float angle = Vector3.Angle(transform.forward, point.transform.position - transform.position);

            proximityIntensity = Mathf.Max((180 - angle) / 720, proximityIntensity);

            if (angle < 15) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    point.StartTeleport();
                }
                else if (Input.GetKeyUp(KeyCode.Space)) {
                    point.InterruptTeleport();
                }
                else if (Input.GetKey(KeyCode.Space)) {
                    point.ChannelTeleport();

                    if (point.GetChannelProgress() > 0.8) {
                        Vector3 pointPos = point.transform.position;
                        
                        point.transform.position = transform.root.position;
                        transform.root.position = pointPos;

                        Vector3 targetPoint = point.transform.position;
                        targetPoint.y = transform.root.position.y;
                        transform.root.LookAt(targetPoint, Vector3.up);

                        Destroy(gameObject);
                    }

                    chargeIntensity = (point.GetChannelProgress() / point.spawnEffectTime) * .75f;
                }
            }
        }

        _renderer.material.SetFloat(shaderProperty, proximityIntensity + chargeIntensity);
    }
}
