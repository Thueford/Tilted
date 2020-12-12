using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Dictionary<string, Func<string, bool>> ability;
    public static Skill skill;
    public string currentSkill = "";
    public bool run_skill = false;
    public float time_testcool_down = 5f;
    public float time;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public float sw_radius; 

    // Start is called before the first frame update
    private void Awake()
    {
        if (skill != null)
        {
            //Debug.LogError("There is more than one instance!");
            return;
        }
        skill = this;

        ability = new Dictionary<string, Func<string, bool> >()
        {
            {"Freeze", freeze},
            {"Shockwave", shockwave}
        };
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (run_skill == true)
        {
            time -= Time.deltaTime;
            /////////////////////////
            Debug.Log(currentSkill);
            /////////////////////////
            if (time <= 0)
            {
                ability[currentSkill].DynamicInvoke("end");
                run_skill = false;
            }
        }
    }

    public void runAbility()
    {
        ability[currentSkill].DynamicInvoke("begin");
    }

    public bool isAvailable()
    {
        return true;
    }

    private bool freeze(string a)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        if (a == "begin")
        {
            //Sound wird gespielt
            audioSource.PlayOneShot(audioClips[0], 1f);

            //value is in playattr in futur
            Time.timeScale = 0.3f;
            Skill.skill.time = Skill.skill.time_testcool_down;
        } else
        {
            //reset timescale
            Time.timeScale = 1f;
            //run after over
        }

        return false;
    }
    private bool shockwave (string a)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        if(a == "begin")
        {
            Debug.Log("SHOCKWAVE");
            Vector2 shockwavepos = MouseInputHandler.mouse_position;
            Collider[] human = Physics.OverlapSphere(shockwavepos, sw_radius);
        }
        return false;
    }
    private bool wall(string a)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
    private bool magnet(string a)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
    private bool bomb(string a)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
    private bool hillclimber(string a)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
    private bool virus(string a)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
}
