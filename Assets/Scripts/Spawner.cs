using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pfHuman;
    private List<GameObject> humans = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject o1 = Instantiate(pfHuman, new Vector3(300, 200, 0), Quaternion.identity);
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        //rt.rect.width = 50f;
        //rt.rect.height = 100f;
        rt.localScale = new Vector3(50, 90, 0);
        humans.Add(o1);
    }

    public GameObject[] getHumans()
    {
        return humans.ToArray();
    }

    public void killHumans(params GameObject[] l)
    {
        foreach(GameObject o in l) {
            humans.Remove(o);
            Destroy(o);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
