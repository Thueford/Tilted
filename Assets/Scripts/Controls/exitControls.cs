using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class exitControls : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    void Start()
    {
        // playClickSound();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btn_pressed()
    {
        playClickSound();
        Application.LoadLevel(PlayerPrefs.GetInt("previousLevel"));
    }

    private void playClickSound() {
        audioSource.PlayOneShot(audioClips[new System.Random().Next(0, audioClips.Length)], 1f);
    }
}
