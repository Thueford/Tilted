using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour
{
    public float y;
    private static Bounds bndEarth;
    private static System.Random rand = new System.Random();
    public Rigidbody2D rb;

    public bool right;
    // Start is called before the first frame update
    void Start()
    {
        bndEarth = GameObject.FindGameObjectsWithTag("Earth")[0].GetComponent<Renderer>().bounds;
        y = (float)rand.NextDouble() * bndEarth.size.y + bndEarth.min.y;
        rb = GetComponent<Rigidbody2D>();

        right = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (right)
        {
            rb.velocity = new Vector2(5f, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(-5f, rb.velocity.y);
        }
        
    }
    public void moveY(float y)
    {
        if(bndEarth.max.y>y && bndEarth.min.y < y) { this.y = y; }
    }

}
