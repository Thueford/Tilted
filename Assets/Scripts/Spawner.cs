using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject o1 = Instantiate(myPrefab, new Vector3(300, 200, 0), Quaternion.identity);
        //o1.width = 20;
        //o1.width = 30;

        //Instantiate(myPrefab, new Vector3(350, 200, 0), Quaternion.identity);
        //Instantiate(myPrefab, new Vector3(250, 200, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
