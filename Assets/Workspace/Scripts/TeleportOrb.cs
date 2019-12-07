using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOrb : MonoBehaviour
{
    GameObject[] points;

    int shaderProperty;
    Renderer _renderer;

    bool charging;

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
            TeleportWaypoint point = pointObject.GetComponent<TeleportWaypoint>();

            float angle = Vector3.Angle(transform.forward, point.transform.position - transform.position);

            proximityIntensity = Mathf.Max((180 - angle) / 720, proximityIntensity);

            if (angle < 15) {
                if (Input.GetAxis("Fire1") > 0 && Input.GetAxis("Fire2") > 0) {
                    if (!charging) {
                        point.StartTeleport();
                        charging = true;
                    }

                    point.ChannelTeleport();

                    if (point.GetChannelProgress() > 0.8)
                    {
                        Vector3 pointPos = point.transform.position;
                        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

                        point.transform.position = player.position;
                        player.position = pointPos;

                        Vector3 targetPoint = point.transform.position;
                        targetPoint.y = player.position.y;
                        player.LookAt(targetPoint, Vector3.up);

                        Destroy(gameObject);
                    }

                    chargeIntensity = (point.GetChannelProgress() / point.spawnEffectTime) * .75f;
                }
                else if (Input.GetAxis("Fire1") <= 0 && Input.GetAxis("Fire2") <= 0 && charging) {
                    point.InterruptTeleport();
                    charging = false;
                }
            }
        }

        _renderer.material.SetFloat(shaderProperty, proximityIntensity + chargeIntensity);
    }
}
