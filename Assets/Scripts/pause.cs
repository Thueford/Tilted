using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pause : MonoBehaviour
{
    public Image panel;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<Image>();
        panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enterPause()
    {
        panel.gameObject.SetActive(true);
        playClickSound();
        Time.timeScale = 0;
    }

    public void endPause()
    {
        playClickSound();
        panel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void playClickSound() {
        audioSource.PlayOneShot(audioClips[new System.Random().Next(0, audioClips.Length)], 1f);
        Debug.Log("Player Click");
    }
}
