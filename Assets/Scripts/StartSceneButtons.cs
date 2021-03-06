using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneButtons : MonoBehaviour
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
    /// <summary>
    /// start_button pressed
    /// </summary>
    public void start_btn_pressed()
    {
        playClickSound();
        //start game
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// settings button pressed
    /// </summary>
    public void settings_btn_pressed()
    {
        playClickSound();
        //open settings
        PlayerPrefs.SetInt("previousLevel", Application.loadedLevel);
        SceneManager.LoadScene("ShowControlsScene");

    }
    /// <summary>
    /// skilltreebutton pressed
    /// </summary>
    public void exit_btn_pressed()
    {
        playClickSound();
        //open skilltree
        Application.Quit();
    }

    
    public void credits_btn_pressed()
    {
        playClickSound();
        SceneManager.LoadScene("CreditsScene");
    }
    public void exit_credit_btn_pressed()
    {
        //playClickSound();
        SceneManager.LoadScene("StartScene");
    }
    private void playClickSound() {
        audioSource.PlayOneShot(audioClips[new System.Random().Next(0, audioClips.Length)], 1f);
    }

    public void mouse_enter_button(Image btn)
    {
        float height = btn.GetComponent<RectTransform>().rect.height + 5;
        float width = btn.GetComponent<RectTransform>().rect.width + 5;
        btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public void mouse_exit_button(Image btn)
    {
        float height = btn.GetComponent<RectTransform>().rect.height - 5;
        float width = btn.GetComponent<RectTransform>().rect.width - 5;
        btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        btn.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
