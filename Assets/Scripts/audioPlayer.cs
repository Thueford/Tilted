using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clipArray;
    public float volume;
    AudioClip lastClip;
    // Start is called before the first frame update
    void Start()
    {
        if (clipArray.Length >= 2) {
            audioSource.clip = RandomClip(clipArray);
            audioSource.Play();
        } else {
            audioSource.clip = clipArray[1];
            audioSource.Play();
        }
    }

    AudioClip RandomClip(AudioClip[] clips){
        int attempts = 3;
        AudioClip newClip = clips[Random.Range(0, clips.Length)];

        while (newClip == lastClip && attempts > 0)
        {
          newClip = clips[Random.Range(0, clips.Length)];
          attempts--;
        }

        lastClip = newClip;
        return newClip;
    }
    //
    // public static void playOneOf(AudioSource source, AudioClip[] clips, float vol){
    //     source.PlayOneShot(RandomClip(clips), vol);
    // }
    //
    // public static void playSoundOneshot(AudioSource source, AudioClip clip, float vol){
    //     source.PlayOneShot(clip, vol);
    // }

    // Update is called once per frame
    void Update()
    {

    }
}
