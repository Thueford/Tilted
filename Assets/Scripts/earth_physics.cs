using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth_physics : MonoBehaviour
{

    private Spawner s;

    public double neigung;

    private const float max_neigung = 30f;
    private const float neutralizer = 385.3f;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.freezeRotation = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        neigung = 0;
        foreach(GameObject g in Spawner.getHumans())
        {
            Debug.Log("Human detektet");
            float tmp = g.transform.position.x;
            Debug.Log(tmp);
            neigung += (tmp - neutralizer);
        }
        rb.rotation = (float)(Math.Asin(neigung / (2 * 560)));
    }
}
