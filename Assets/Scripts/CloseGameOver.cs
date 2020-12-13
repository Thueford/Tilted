using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseGameOver : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip clickSound;

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
        audioSource.PlayOneShot(clickSound, 1f);
        SceneManager.LoadScene("StartScene");
    }
}
