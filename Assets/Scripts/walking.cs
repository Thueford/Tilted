using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour
{
    public float y;
    private static Bounds bndEarth;
    private static System.Random rand = new System.Random();
    public Rigidbody2D rb;

    private float winkel;

    public bool on_earth;

    public float speed;

    public bool right;

    public bool Bergsteiger;

    private float last_collision;
    // Start is called before the first frame update
    void Start()
    {
        bndEarth = GameObject.FindGameObjectsWithTag("Earth")[0].GetComponent<Renderer>().bounds;
        rb = GetComponent<Rigidbody2D>();



        if (rand.Next(2) == 0) { right = false; } else { right = true; }
        if (rand.Next(10) < 4) { Bergsteiger = true; } else { Bergsteiger = false; }
        on_earth = false;
        last_collision = 0;
    }

    // Update is called once per frame
    void Update()
    {
        speed += 0.001f;
        if(rb.transform.position.y == y && ((rb.transform.position.x < bndEarth.max.x) && (rb.transform.position.x > bndEarth.min.x)))
        {
            rb.transform.position = new Vector2(rb.transform.position.x, y);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        } else if (rb.transform.position.y < y && ((rb.transform.position.x < bndEarth.max.x) && (rb.transform.position.x > bndEarth.min.x)))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.3f);
        } else if (((rb.transform.position.x > bndEarth.max.x) && (rb.transform.position.x < bndEarth.min.x)))
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        if (right && on_earth)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        } else if(on_earth)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        //change walking directions
        if (rand.NextDouble()*100 == 1 && on_earth)
        {
            if (rand.NextDouble() * 100 < 50)
            {
                right = !right;
            } else
            {
                float tmpY = rb.transform.position.y;
                if (tmpY < bndEarth.center.y)
                {
                    y = (float)rand.NextDouble() * (bndEarth.size.y * 0.1f);
                } else
                {
                    y = (float)rand.NextDouble() * (bndEarth.size.y * -0.1f);
                }
                //y = (float)rand.NextDouble() * (bndEarth.size.y * (0.3f * (float)Math.Pow(-1, rand.Next(2) + 1)));

                moveY(tmpY - y);
            }
        }
        /*winkel = GameObject.FindGameObjectsWithTag("Earth")[0].GetComponent<Rigidbody2D>().rotation;
        Debug.Log("Winkel: " + winkel);*/
        winkel = GameObject.FindGameObjectsWithTag("Earth")[0].GetComponent<earth_physics>().cAngle;
        if (!Bergsteiger && (rand.NextDouble() * 100) < Math.Abs(winkel) && last_collision < 0)
        {
            if (winkel < 0)
            {
                right = true;
            } else
            {
                right = false;
            }
        }
        last_collision -= 0.5f;
    }
    public void moveY(float y)
    {
        this.y = y;
    }

    public void OnTriggerEnter2D()
    {
        right = !right;
        float tmpY = rb.transform.position.y;
        y = (float)rand.NextDouble() * (bndEarth.size.y - 0.3f);
        if (y < 1)
        {
            y+=3;
        }
        if(y>(bndEarth.size.y - 3))
        {
            y-=2;
        }
        moveY(tmpY - y);
        last_collision = 5;
    }

    public void OnTriggerExit2D()
    {
        on_earth = true;
    }

    public void OnCollisionEnter2D()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        moveY(rb.position.y+2);
    }

}
