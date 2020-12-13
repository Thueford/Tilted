using UnityEngine;

public class walking : MonoBehaviour
{
    public static float earthOff = 10;
    public static int mainDir = -1;

    private static Bounds bndEarth;
    private static earth_physics epEarth;
    public static System.Random rand { get; private set; } = new System.Random();

    public Rigidbody2D rb;
    public Vector2 addVelocity = new Vector2(0, 0);

    public float tx { get; private set; }
    public float y { get; private set; }

    public bool on_earth = false;
    public float speed;
    public bool right { get; private set; }
    public bool Bergsteiger;
    public bool climb = false;
    public float last_collision = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameObject earth = GameObject.FindGameObjectWithTag("Earth");
        bndEarth = earth.GetComponent<Renderer>().bounds;
        epEarth = earth.GetComponent<earth_physics>();
        rb = GetComponent<Rigidbody2D>();

        right = rand.Next(2) == 0;
        Bergsteiger = rand.Next(10) < 6;
    }

    private bool inRange(float v, float min, float max)
    {
        return v > min && v < max;
    }

    // Update is called once per frame
    void Update()
    {
        speed += 0.3f * Time.deltaTime;
        float ytilt = bndEarth.extents.x * Mathf.Sin(epEarth.cAngle);

        // default movement
        if (on_earth) rb.velocity = new Vector2(right ? speed : -speed, rb.velocity.y);

        if (last_collision <= 0)
        {
            if (
                inRange(rb.transform.position.x, bndEarth.min.x, bndEarth.min.x + 2 * earthOff) ||
                inRange(rb.transform.position.x, bndEarth.max.x - 2 * earthOff, bndEarth.max.x))
            {
                right = !right;
                last_collision = 1;
            }
        }

        // check for boundings
        // in earth x bounds and on y pos: stop falling
        if (rb.transform.position.y <= y && inRange(rb.transform.position.x, bndEarth.min.x + earthOff, bndEarth.max.x - earthOff))
        {
            // rb.transform.position = new Vector2(rb.transform.position.x, y);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        // under y pos and in earth bounds: start falling
        else if (rb.transform.position.y < y && inRange(rb.transform.position.x, bndEarth.min.x + earthOff, bndEarth.max.x - earthOff))
        {
            // rb.velocity = new Vector2(rb.velocity.x, 0.3f);
        }
        // out of earth bounds: reset velocity
        else if (!inRange(rb.transform.position.x, bndEarth.min.x + earthOff, bndEarth.max.x - earthOff))
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        // for effects
        // rb.velocity += addVelocity;

        //change walking directions
        if (speed * Time.deltaTime * rand.NextDouble() * 1e3 < 1 && on_earth && !climb)
        {
            double r = rand.NextDouble() * 100;
            if (r < 50)
            {
                //Debug.Log("change walk direction");
                right = r < 25 + mainDir * 20;
            }
            else
            {
                // generate new y pos
                float tmpY = rb.transform.position.y;
                y = (float)rand.NextDouble() * bndEarth.size.y * 0.1f;
                if (tmpY >= bndEarth.center.y) y = -y;
                // y = (float)rand.NextDouble() * (bndEarth.size.y * (0.3f * (float)Math.Pow(-1, rand.Next(2) + 1)));

                moveY(tmpY - y);
            }
        }
        
        /*winkel = GameObject.FindGameObjectsWithTag("Earth")[0].GetComponent<Rigidbody2D>().rotation;

        Debug.Log("Winkel: " + winkel);*/
        //if (!Bergsteiger && !climb && (rand.NextDouble() * 100) < Math.Abs(epEarth.cAngle) && last_collision < 0)
        //    right = epEarth.cAngle < 0;

        //if (climb) right = epEarth.cAngle > 0;
        //last_collision -= 0.5f;

        /* // 1
        if (!Bergsteiger && !climb && (Time.deltaTime * rand.NextDouble() * 100) < Math.Abs(epEarth.cAngle))
        {
            //if (last_collision < 0) right = epEarth.cAngle < 0;
            if (epEarth.cAngle > 0) right = !right;
        }*/

        if (climb) right = epEarth.cAngle > 0;
        if (last_collision > 0) last_collision -= Time.deltaTime;
        //last_collision -= 0.3f;
    }
    public void moveY(float y)
    {
        this.y = y;
    }
    public void moveX(float x)
    {
        this.tx = x;
    }

    public void OnTriggerEnter2D()
    {
        /*if (last_collision == 0)
        {
            last_collision = 50;
            right = !right;
        }*/

        float tmpY = rb.transform.position.y;
        y = (float)rand.NextDouble() * (bndEarth.size.y - 0.3f);
        if (y < 1) y += 3;
        if (y > (bndEarth.size.y - 3)) y -= 2;

        moveY(tmpY - y);
    }

    public void OnTriggerExit2D()
    {
        on_earth = true;
    }

    public void OnCollisionEnter2D()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        moveY(rb.position.y + 2);
    }

}
