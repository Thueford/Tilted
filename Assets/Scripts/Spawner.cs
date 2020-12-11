using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pfHuman;
    private ArrayList humans = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        GameObject o1 = Instantiate(pfHuman, new Vector3(300, 200, 0), Quaternion.identity);
        humans.Add(o1);
    }

    public ArrayList getHumans()
    {
        return humans;
    }

    public void killHuman(GameObject o)
    {
        humans.Remove(o);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
