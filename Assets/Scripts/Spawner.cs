using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pfHuman = null;
    private static List<GameObject> humans = new List<GameObject>();
    private static Timer itvWave;
    private static System.Random rand = new System.Random();

    public static int wavePeriod = 1024;
    public static int waveVary = 512;

    // Start is called before the first frame update
    void Start()
    {
        //UnityEngine.Debug.Log("Hello");
        itvWave = new Timer((time) => {
            //UnityEngine.Debug.Log("World");
            //spawnHuman((float)rand.NextDouble()*50 + 300, (float)rand.NextDouble()*50 + 200);
            //int ran = (int)(waveVary * rand.NextDouble()) + wavePeriod;
            //itvWave.Change(ran, ran);

            spawnHuman(300, 200);
        }, null, 1000, 1000);

        spawnHuman(300, 230);
        spawnHuman(380, 250);
        spawnHuman(450, 200);
    }

    public static GameObject[] getHumans()
    {
        return humans.ToArray();
    }

    public static void killHumans(params GameObject[] l)
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

    private void spawnHuman(float x, float y)
    {
        GameObject o = Instantiate(pfHuman, new Vector3(x, y, 0), Quaternion.identity);
        o.transform.localScale = new Vector3(0.25f, 0.4f, 0);
        humans.Add(o);
    }
}
