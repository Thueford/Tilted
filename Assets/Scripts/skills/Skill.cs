using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Dictionary<string, Func<Status, bool>> ability;
    public static Skill skill;
    public string currentSkill = "";
    public bool run_skill = false;
    public float time_testcool_down = 5f;
    public float time;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public enum Status
    {
        BEGIN, UPDATE, END
    }

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

        ability = new Dictionary<string, Func<Status, bool> >()
        {
            {"Freeze", freeze},
            {"Shockwave", shockwave},
            {"magnet", magnet}
        };
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (run_skill)
        {
            time -= Time.deltaTime;

            // Debug.Log(currentSkill);
            ability[currentSkill].DynamicInvoke(Status.UPDATE);

            if (time <= 0)
            {
                ability[currentSkill].DynamicInvoke(Status.END);
                run_skill = false;
            }
        }
    }

    public void runAbility()
    {
        ability[currentSkill].DynamicInvoke(Status.BEGIN);
    }

    public bool isAvailable()
    {
        return true;
    }

    private bool freeze(Status status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        if (status == Status.BEGIN)
        {
            //Sound wird gespielt
            audioSource.PlayOneShot(audioClips[0], 1f);

            //value is in playattr in futur
            Time.timeScale = 0.3f;
            Skill.skill.time = Skill.skill.time_testcool_down;
        } else if(status == Status.END)
        {
            //reset timescale
            Time.timeScale = 1f;
            //run after over
        }

        return false;
    }
    private bool shockwave (Status status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        if(status == Status.BEGIN)
        {
            Debug.Log("SHOCKWAVE");
            Vector2 shockwavepos = MouseInputHandler.mouse_position;
            Collider[] human = Physics.OverlapSphere(shockwavepos, sw_radius);
        }
        return false;
    }
    private bool wall(Status status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
    private bool magnet(Status status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        if (status == Status.BEGIN)
        {
            time = time_testcool_down;
        }
        else if (status == Status.END)
        {

        }

        return false;
    }
    private bool bomb(Status status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
    private bool hillclimber(Status status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
    private bool virus(Status status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
}
