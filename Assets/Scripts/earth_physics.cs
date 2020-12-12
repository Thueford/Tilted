using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth_physics : MonoBehaviour
{


    public double neigung;

    private const float max_neigung = 30f;

    public Rigidbody2D rb;

    private float center;
    private float xsize;


    // Start is called before the first frame update
    void Start()
    {
        rb.freezeRotation = true;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("start");
        center = GetComponent<Renderer>().bounds.center.x;
        xsize = GetComponent<Renderer>().bounds.extents.x;
        Debug.Log(xsize);
        Debug.Log(transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        neigung = 0;
        foreach(GameObject g in Spawner.getHumans())
        {
            //Debug.Log("Human detektet");
            float tmp = g.transform.position.x;
            //Debug.Log(tmp);
            Debug.Log(tmp - transform.position.x);
            neigung += (tmp - transform.position.x);
        }
        
        adjust_angle((float) -neigung );
    }

    private void adjust_angle(float n)
    {
        if (rb.rotation < neigung)
        {
                rb.rotation += 1;
        } else if (rb.rotation > neigung)
        {
            rb.rotation -= 1;
        }
        else
        {
            rb.rotation = rb.rotation;
        }
    }
}
