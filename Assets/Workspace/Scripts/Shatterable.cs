using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatterable : MonoBehaviour
{
    public GameObject shatteredModel;

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("EarthOrb")) return;

        shatteredModel.SetActive(true);
        for (int i = 0; i < shatteredModel.transform.childCount; i++) {
            shatteredModel.transform.GetChild(i).gameObject.AddComponent<Rigidbody>().AddExplosionForce(10, shatteredModel.transform.GetChild(i).position, 10, 1, ForceMode.Impulse);
        }

        // for (int i = 0; i < shatteredModel.transform.childCount; i++) {
        //     shatteredModel.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
        // }
        gameObject.SetActive(false);        
    }
}
