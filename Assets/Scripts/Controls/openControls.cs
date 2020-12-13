using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class openControls : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
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
        playClickSound();
        SceneManager.LoadScene("ShowControlsScene");
    }

    private void playClickSound() {
        audioSource.PlayOneShot(audioClips[new System.Random().Next(0, audioClips.Length)], 1f);
    }
}
