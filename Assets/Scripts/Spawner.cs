using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pfHuman;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(pfHuman, new Vector3(300, 200, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
