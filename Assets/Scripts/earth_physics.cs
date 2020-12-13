using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class earth_physics : MonoBehaviour
{
    // how much you want the player to suffer
    public static float tiltFactor = 0.3f;

    public float cAngle; // current angle
    public float tAngle; // target angle
    public float diff;   // current tilt difference per second

    public AudioSource audioSource;
    public AudioSource walkSource;
    public AudioClip[] audioClips;
    public AudioClip[] walkSounds;

    private const float max_neigung = 20f;

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
        float neigung = 0; // median accumulator
        int count = 10;    // default earth inertia
        foreach (GameObject g in Spawner.getHumans())
        {
            if (g.GetComponent<walking>().on_earth) { //(g.transform.position.y < bounds.max.y && g.transform.position.y > bounds.min.y) {
                neigung += g.transform.position.x - bounds.center.x;
                count++;
                playWalkSound();
            }
        }
        if(count > 0 && neigung != 0)
            adjust_angle(-neigung/(count*bounds.extents.x));
    }

    private void adjust_angle(float n)
    {
        // calculate target angle
        tAngle = (float)(180 * Math.Asin(n < -1 ? -1 : n > 1 ? 1 : n) / Math.PI);
        cAngle = rb.transform.rotation.eulerAngles.z;
        // rotation sux and thinks negative degrees need to be adjusted to 360° 
        if (cAngle > 180) cAngle -= 360;

        // rotate
        diff = Math.Sign(tAngle - cAngle) * (float)Math.Pow(tAngle - cAngle, 2);
        rb.transform.Rotate(new Vector3(0, 0, tiltFactor * diff * Time.deltaTime));

        // current tilt based stuff
        diff = Math.Abs(1000 * diff * Time.deltaTime);
        if (diff > 40) playTiltSound();
        if (cAngle >= max_neigung) SceneManager.LoadScene("GameOverScene");
    }

    private void playTiltSound(int n = 0) {
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(audioClips[new System.Random().Next(0, audioClips.Length)], 0.4f);
        }
    }

    private void playWalkSound() {
        if (!walkSource.isPlaying) {
            walkSource.PlayOneShot(walkSounds[1], 0.5f);
        }
    }

}
