using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{
    public float speed = 0.2f;
    public float offset = 0;

    private float X;

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(offset, 0, 0);
        X = offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        X -= speed * Time.deltaTime;
        if (X < -1) {
            X *= -1;
            transform.Translate(X * 2, 0, 0);
        }
    }
}
