using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pfHuman = null;
    private static GameObject earth;
    private static List<GameObject> humans = new List<GameObject>();
    private static System.Random rand = new System.Random();

    public static int wavePeriod = 1024;
    public static int waveVary = 512;
    public static int waveAmount = 5;
    GameObject o;

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Earth");
        if (tmp.Length == 0) throw new NullReferenceException("Earth not found.");
        earth = tmp[0];

        StartCoroutine(spawnWave());
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

    private IEnumerator spawnWave()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            UnityEngine.Debug.Log("Spawn " + waveAmount + "Humans");
            for (int i = 0; i < waveAmount; i++)
            {
                spawnHuman(
                    earth.transform.position.x/2 + (float)rand.NextDouble() * earth.transform.position.x,
                    earth.transform.position.y + 200 + (float)rand.NextDouble() * 400);
            }

            yield return new WaitForSeconds(4);
        }
    }

    private void spawnHuman(float x, float y)
    {
        // UnityEngine.Debug.Log("Spawn Human at " + x + ", " + y);
        GameObject o = Instantiate(pfHuman, new Vector3(x, y, 0), Quaternion.identity);
        o.transform.localScale = new Vector3(0.25f, 0.4f, 0);
        humans.Add(o);
    }
}
