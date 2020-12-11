using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// start_button pressed
    /// </summary>
    public void start_btn_pressed()
    {
        //start game
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// settings button pressed
    /// </summary>
    public void settings_btn_pressed()
    {
        //open settings
    } 
    /// <summary>
    /// skilltreebutton pressed
    /// </summary>
    public void skilltree_btn_pressed()
    {
        //open skilltree
    }
}
