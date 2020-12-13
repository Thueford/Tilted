using System;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Dictionary<ESkill, Func<EStatus, bool>> ability;
    public static Skill skill;
    public ESkill currentSkill = ESkill.NONE;
    public Dictionary<ESkill, float> currentSkills;
    public bool run_skill = false;
    public float time_testcool_down = 5f;
    public float time;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public ESkill[] eSkills;

    public enum EStatus
    {
        BEGIN, UPDATE, END
    }

    public enum ESkill
    {
        NONE, FREEZE, SHOCK, WALL, MAGNET, BOMB, CLIMB, COVID19
    }

    public float shock_radius;
    public float magnet_radius;
    public float climb_radius;

    public float shock_strength;
    public float magnet_strength;

    // Start is called before the first frame update
    private void Awake()
    {
        if (skill != null)
        {
            //Debug.LogError("There is more than one instance!");
            return;
        }
        skill = this;

        ability = new Dictionary<ESkill, Func<EStatus, bool> >()
        {
            {ESkill.FREEZE, freeze},
            {ESkill.SHOCK, shockwave},
            {ESkill.WALL, wall},
            {ESkill.MAGNET, magnet},
            {ESkill.BOMB, bomb},
            {ESkill.CLIMB, hillclimber},
            {ESkill.COVID19, virus}
        };
        currentSkills = new Dictionary<ESkill, float>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (run_skill)
        {
            time -= Time.deltaTime;

            Debug.Log(currentSkill);
            ability[currentSkill].DynamicInvoke(EStatus.UPDATE);

            if (time <= 0)
            {
                ability[currentSkill].DynamicInvoke(EStatus.END);
                currentSkill = ESkill.NONE;
                run_skill = false;
            }
        }//*/

        //check for each active skill if its done

        eSkills = new ESkill[currentSkills.Keys.Count];
        currentSkills.Keys.CopyTo(eSkills, 0);
        foreach (ESkill sk in eSkills)
        {
            ////
            //Debug.Log(currentSkill);
            ////

            ability[sk].DynamicInvoke(EStatus.UPDATE);
            currentSkills[sk] -= Time.deltaTime;
            if (currentSkills[sk] <= 0)
            {
                currentSkills.Remove(sk);
                ability[sk].DynamicInvoke(EStatus.END);
            }
        }
    }

    public void runAbility()
    {
        currentSkills.Add(currentSkill, time_testcool_down);
        ability[currentSkill].DynamicInvoke(EStatus.BEGIN);
        currentSkill = ESkill.NONE;
    }

    private bool freeze(EStatus status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        if (status == EStatus.BEGIN)
        {
            //Sound wird gespielt
            audioSource.PlayOneShot(audioClips[0], 1f);

            //value is in playattr in futur
            Time.timeScale = 0.3f;
            skill.time = skill.time_testcool_down;
        }
        else if(status == EStatus.END)
        {
            //reset timescale
            Time.timeScale = 1f;
            //run after over
        }

        return false;
    }
    private bool shockwave (EStatus status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        if(status == EStatus.BEGIN)
        {
            time = time_testcool_down;
        }
        else if(status == EStatus.UPDATE)
        {
            foreach (GameObject o in playerattribute.getHumansInMouseRange(shock_radius))
            {
                float d = MouseInputHandler.getMouseDistance(o);
                o.GetComponent<walking>().addVelocity = shock_strength * Time.deltaTime * (d / shock_radius - 1) * (MouseInputHandler.mouse_position - o.transform.position) * new Vector2(1, 0.4f);
            }
        }
        return false;
    }
    private bool wall(EStatus status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
    private bool magnet(EStatus status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        if (status == EStatus.BEGIN)
        {
            time = time_testcool_down;
        }
        else if (status == EStatus.UPDATE)
        {
            foreach(GameObject o in playerattribute.getHumansInMouseRange(magnet_radius))
            {
                float d = MouseInputHandler.getMouseDistance(o);
                Rigidbody2D rb = o.GetComponent<Rigidbody2D>();
                rb.position += magnet_strength * Time.deltaTime * (1 - d/magnet_radius) * (MouseInputHandler.mouse_position - o.transform.position) * new Vector2(1, 0.2f);
            }
        }

        return false;
    }
    private bool bomb(EStatus status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
    private bool hillclimber(EStatus status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        if (status == EStatus.BEGIN)
        {
            //Sound wird gespielt
            audioSource.PlayOneShot(audioClips[0], 1f);
            time = time_testcool_down;
            foreach(GameObject o in playerattribute.getHumansInMouseRange(climb_radius))
            {
                o.GetComponent<walking>().climb = true;
            } 
        } 
        else if (status == EStatus.END)
        {
            foreach(GameObject o in Spawner.getHumans())
            {
                o.GetComponent<walking>().climb = false;
            }
        }

        return false;
    }
    private bool virus(EStatus status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        return false;
    }
}
