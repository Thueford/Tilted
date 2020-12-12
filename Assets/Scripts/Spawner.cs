using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public int[] prefabProb;

    private static Bounds bndEarth;
    private static List<GameObject> humans = new List<GameObject>();
    private static System.Random rand = new System.Random();

    public static int wavePeriod = 1024;
    public static int waveVary = 512;
    public static int waveAmount = 5;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(prefabs.Length == prefabProb.Length);
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Earth");
        if (tmp.Length == 0) throw new NullReferenceException("Earth not found.");
        bndEarth = tmp[0].GetComponent<Renderer>().bounds;

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
            Debug.Log("Spawn " + waveAmount + "Humans");
            for (int i = 0; i < waveAmount; i++)
            {
                spawnHuman(
                    bndEarth.min.x + 0.9f * (float)rand.NextDouble() * bndEarth.size.x,
                    bndEarth.max.y + bndEarth.size.y * 4 + (float)rand.NextDouble() * bndEarth.size.y * 6);
            }

            yield return new WaitForSeconds(4);
        }
    }

    private void spawnHuman(float x, float y)
    {
        int i, tgt = (int)(rand.NextDouble() * prefabProb.Sum());
        //Debug.Log(tgt);
        for (i = 0; tgt >= prefabProb[i]; ++i) tgt -= prefabProb[i];
        //Debug.Log(i);

        // UnityEngine.Debug.Log("Spawn Human at " + x + ", " + y);
        GameObject o = Instantiate(prefabs[i], new Vector3(x, y, 0), Quaternion.identity);
        o.transform.localScale = new Vector3(2.3f, 3.5f, 0);
        humans.Add(o);
    }
}
