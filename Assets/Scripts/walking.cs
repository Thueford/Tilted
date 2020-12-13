using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour
{
    public float y;
    private static Bounds bndEarth;
    private static earth_physics epEarth;
    private static System.Random rand = new System.Random();
    public Rigidbody2D rb;
    public Vector2 addVelocity = new Vector2(0, 0);

    public bool on_earth;
    public float speed;
    public bool right;
    public bool Bergsteiger;
    public bool climb;

    private float last_collision;
    // Start is called before the first frame update
    void Start()
    {
        GameObject earth = GameObject.FindGameObjectsWithTag("Earth")[0];
        bndEarth = earth.GetComponent<Renderer>().bounds;
        epEarth = earth.GetComponent<earth_physics>();
        rb = GetComponent<Rigidbody2D>();

        right = rand.Next(2) == 0;
        Bergsteiger = rand.Next(10) < 6;
        on_earth = false;
        climb = false;
        last_collision = 0;
    }

    // Update is called once per frame
    void Update()
    {
        speed += 1f * Time.deltaTime;

        // default movement
        if (right && on_earth)
            rb.velocity = new Vector2(right ? speed : -speed, rb.velocity.y);

        // check for boundings
        if (rb.transform.position.y == y && ((rb.transform.position.x < bndEarth.max.x) && (rb.transform.position.x > bndEarth.min.x)))
        {
            rb.transform.position = new Vector2(rb.transform.position.x, y);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else if (rb.transform.position.y < y && ((rb.transform.position.x < bndEarth.max.x) && (rb.transform.position.x > bndEarth.min.x)))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.3f);
        }
        else if (((rb.transform.position.x > bndEarth.max.x) && (rb.transform.position.x < bndEarth.min.x)))
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        // for effects
        rb.velocity += addVelocity;

        //change walking directions
        if (rand.NextDouble()*100 == 1 && on_earth && !climb)
        {
            if (rand.NextDouble() * 100 < 50)
            {
                right = !right;
            }
            else
            {
                float tmpY = rb.transform.position.y;
                y = (float)rand.NextDouble() * (bndEarth.size.y * 0.1f);
                if (tmpY >= bndEarth.center.y) y = -y;
                //y = (float)rand.NextDouble() * (bndEarth.size.y * (0.3f * (float)Math.Pow(-1, rand.Next(2) + 1)));

                moveY(tmpY - y);
            }
        }
        /*winkel = GameObject.FindGameObjectsWithTag("Earth")[0].GetComponent<Rigidbody2D>().rotation;
		
        Debug.Log("Winkel: " + winkel);*/
        if (!Bergsteiger && !climb && (rand.NextDouble() * 100) < Math.Abs(epEarth.cAngle) && last_collision < 0)
            right = epEarth.cAngle < 0;

        if (climb) right = epEarth.cAngle > 0;

        /*float winkel = GameObject.FindGameObjectsWithTag("Earth")[0].GetComponent<earth_physics>().cAngle;
        if (!Bergsteiger && !climb && (rand.NextDouble() * 100) < Math.Abs(winkel))
        {
            if (last_collision < 0) right = winkel < 0;
            else if (winkel > 0) right = !right;
        }

        if (climb) right = winkel > 0;*/
        last_collision -= 0.3f;
    }
    public void moveY(float y)
    {
        this.y = y;
    }

    public void OnTriggerEnter2D()
    {
        if(last_collision<3)right = !right;
        float tmpY = rb.transform.position.y;
        y = (float)rand.NextDouble() * (bndEarth.size.y - 0.3f);
        if (y < 1) y += 3;
        if (y > (bndEarth.size.y - 3))  y -= 2;

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
