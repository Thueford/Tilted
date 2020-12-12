﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public int[] prefabProb;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private static Bounds bndEarth;
    private static List<GameObject> humans = new List<GameObject>();
    private static System.Random rand = new System.Random();

    public static int wavePeriod = 1024;
    public static int waveVary = 512;
    public static int waveAmount = 5;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Spawner");
        if (prefabs.Length != prefabProb.Length)
            throw new AssertionException("prefabs.Length != prefabProb.Length", "Turtle.Spawner prefabs must have same length as prefabProb");
        //Debug.Assert(prefabs.Length == prefabProb.Length, "Turtle.Spawner prefabs must have same length as prefabProb.");

        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Earth");
        if (tmp.Length == 0) throw new NullReferenceException("Earth not found.");
        bndEarth = tmp[0].GetComponent<Renderer>().bounds;

        StartCoroutine(waveInterval());
        //spawnWave();
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

    public static void killAllHumans()
    {
        humans.Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator waveInterval()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            spawnWave();
            yield return new WaitForSeconds(4);
        }
    }

    private void spawnWave()
    {
        Debug.Log("Spawn " + waveAmount + " Humans");
        audioSource.PlayOneShot(audioClips[rand.Next(0, audioClips.Length)], 0.8f);
        for (int i = 0; i < waveAmount; i++)
        {
            spawnHuman(
                bndEarth.min.x + (float)(0.1 + 0.8 * rand.NextDouble()) * bndEarth.size.x,
                bndEarth.max.y + bndEarth.size.y * 6 + (float)rand.NextDouble() * bndEarth.size.y * 2);
        }
    }

    private void spawnHuman(float x, float y)
    {
        int i, tgt = (int)(rand.NextDouble() * prefabProb.Sum());
        for (i = 0; tgt >= prefabProb[i]; ++i) tgt -= prefabProb[i];

        // UnityEngine.Debug.Log("Spawn Human at " + x + ", " + y);
        GameObject o = Instantiate(prefabs[i], new Vector3(x, y, 0), Quaternion.identity);
        o.transform.localScale = new Vector3(2.3f, 3.5f, 0);
        humans.Add(o);
    }

    public void OnDestroy()
    {
        humans.Clear();
    }
}
