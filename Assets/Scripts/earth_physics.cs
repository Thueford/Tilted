using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth_physics : MonoBehaviour
{

    private Spawner s;

    private double neigung;

    private const float max_neigung = 30f;
    private const float neutralizer = 385.3f;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        neigung = 0;
        foreach(GameObject g in s.getHumans())
        {
            neigung += (g.transform.position.x - neutralizer);
        }
        float tmp = (float)(Math.Asin(neigung / (2 * 560)));
        rb.rotation = tmp; 
    }
}
