using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Humanometer : MonoBehaviour
{
    public Text kt;
    // Start is called before the first frame update
    void Start()
    {
        kt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        kt.text = "Humans: " + Spawner.getHumans().Length;
    }

    public void OnDestroy()
    {
        if(PlayerPrefs.GetInt("Highscore") < Spawner.getHumans().Length || !PlayerPrefs.HasKey("Highscore"))
        {
            Debug.Log("Saved new Highscore");
            PlayerPrefs.SetInt("Highscore", Spawner.getHumans().Length);
        }
    }
}
