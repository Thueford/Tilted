using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class openControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn_pressed()
    {
        PlayerPrefs.SetInt("previousLevel", Application.loadedLevel);
        SceneManager.LoadScene("ShowControlsScene");
    }
}
