using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class showHighscore : MonoBehaviour
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
        kt.text = "Highscore: " + PlayerPrefs.GetInt("Highscore") + " Menschen";
    }
}
