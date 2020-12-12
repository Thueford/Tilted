using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    public static MouseInputHandler Instance;
    private Vector3 mouse_position;
    private List<GameObject> picked = new List<GameObject>();
    private int num_max_humans = 2;
    private int pickup_radius = 15;

    void Awake()
    {
        if (Instance != null)
        {
            //Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //checks for mouse events
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            GameObject[] humans = Spawner.getHumans();
            
            mouse_position = Input.mousePosition;
            mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);
            mouse_position.z = 0;

            //Debug.Log("Distance: ");
            //System.Array.Sort(humans, (a, b) => { return distanceOf(a.transform.position, mouse_position) < distanceOf(b.transform.position, mouse_position) ? -1 : 1; });
            System.Array.Sort(humans, (a, b) => { return Vector3.Distance(a.transform.position, mouse_position) < Vector3.Distance(b.transform.position, mouse_position) ? -1 : 1; });


            //Debug.Log("---------------------------");
            //Debug.Log(distanceOf(humans[0].transform.position, mouse_position));
            //Debug.Log(humans[0].transform.position);
            for (int i = 0; i<num_max_humans; i++)
            {
                Debug.Log(Vector3.Distance(humans[i].transform.position, mouse_position));
                if (Vector3.Distance(humans[i].transform.position, mouse_position) <= pickup_radius)
                {
                    /////////////////////////////////////////
                    ////////    ADD SOUNDS HERE    //////////
                    /////////////////////////////////////////

                    picked.Add(humans[i]);
                    Debug.Log("pick");
                }
            }
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            Debug.Log("clear");
            picked.Clear();
        }

        //if picked not empty make follow mouse
        if (picked.Count > 0)
        {
            foreach (GameObject g in picked)
            {
                mouse_position = Input.mousePosition;
                mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);
                Vector3 npos = new Vector3();

                npos.x = mouse_position.x; //-2 istoffset
                npos.y = mouse_position.y;
                npos.z = 0;
                g.transform.position = npos;
                //Debug.Log(g.transform.position);
            }
        }
    }

    float distanceOf(Vector3 go, Vector3 mouse)
    {
        return Mathf.Abs(mouse.x - go.x + mouse.y - go.y);
    }
}
