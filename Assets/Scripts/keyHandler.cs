using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyHandler : MonoBehaviour
{

    public GameObject cd;
    public cool_down cooldown;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = cd.GetComponent<cool_down>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(cooldown.key) && cooldown.available) 
        {
            Debug.Log("Event!!");
        }

    }
}
