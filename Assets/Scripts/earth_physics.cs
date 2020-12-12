﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth_physics : MonoBehaviour
{


    public double neigung;

    private const float max_neigung = 30f;

    public Rigidbody2D rb;

    private float xcenter;
    private float ycenter;
    private float xsize;
    private float ysize;


    // Start is called before the first frame update
    void Start()
    {
        rb.freezeRotation = true;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("start");
        xcenter = GetComponent<Renderer>().bounds.center.x;
        ycenter = GetComponent<Renderer>().bounds.center.y;
        xsize = GetComponent<Renderer>().bounds.extents.x;
        ysize = GetComponent<Renderer>().bounds.extents.y;
        Debug.Log(xsize);
        Debug.Log(transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        neigung = 0;
        foreach (GameObject g in Spawner.getHumans())
        {
            if (g.transform.position.y < ycenter+ysize && g.transform.position.y > ycenter-ysize) {
                float tmp = g.transform.position.x;
                
                
                neigung += (tmp - transform.position.x);
            }
        }
        adjust_angle((float) -neigung/Spawner.getHumans().Length);
    }

    private void adjust_angle(float n)
    {
        //Debug.Log("Winkel: " + n);
        if (rb.rotation < n-4) { 
            rb.transform.Rotate(new Vector3(0, 0, 0.5f * Time.deltaTime * 0.8f)); 
        } else if(rb.rotation > n+4)
        {
            rb.transform.Rotate(new Vector3(0, 0, -0.5f * Time.deltaTime * 0.8f));
        }
        
    }
}
