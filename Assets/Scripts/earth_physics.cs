using System;
using UnityEngine;

public class earth_physics : MonoBehaviour
{

    public float cAngle;
    public float tAngle;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private const float max_neigung = 30f;

    public Rigidbody2D rb;

    private Bounds bounds;


    // Start is called before the first frame update
    void Start()
    {
        rb.freezeRotation = true;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("start");
        bounds = GetComponent<Renderer>().bounds;
        Debug.Log(transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        float neigung = 0;
        int count = 10;
        foreach (GameObject g in Spawner.getHumans())
        {
            walking w = g.GetComponent<walking>();
            if(w.on_earth) { //(g.transform.position.y < bounds.max.y && g.transform.position.y > bounds.min.y) {
                neigung += g.transform.position.x - bounds.center.x;
                count++;
            }
        }
        if(count > 0) adjust_angle((float) -neigung/(count*bounds.extents.x));
    }

    private void adjust_angle(float n)
    {
        tAngle = (float)(180 * Math.Asin(n < -1 ? -1 : n > 1 ? 1 : n) / Math.PI);
        cAngle = rb.transform.rotation.eulerAngles.z;
        float diff = Math.Sign(tAngle - cAngle) * (float)Math.Pow(tAngle - cAngle, 2);
        rb.transform.Rotate(new Vector3(0, 0, 0.3f * diff * Time.deltaTime));
        playTiltSound();
    }

    private void playTiltSound() {
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(audioClips[new System.Random().Next(0, audioClips.Length)], 0.6f);
        }
    }

}
