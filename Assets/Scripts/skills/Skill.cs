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
    public AudioSource music;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public ESkill[] eSkills;
    private GameObject icePicture;
    private GameObject bombPicture;
    private GameObject covidPicture;

    private float bombRadius = 55f;

    public enum EStatus
    {
        BEGIN, UPDATE, END
    }

    public enum ESkill
    {
        NONE, FREEZE, SHOCK, WALL, MAGNET, BOMB, CLIMB, COVID19, EMERGENCY
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
            {ESkill.COVID19, virus},
            {ESkill.EMERGENCY, emergency}
        };
        currentSkills = new Dictionary<ESkill, float>();

        //Init effect pics
        icePicture = GameObject.FindGameObjectWithTag("Ice");
        icePicture.SetActive(false);
        bombPicture = GameObject.FindGameObjectWithTag("BombPic");
        bombPicture.SetActive(false);
        covidPicture = GameObject.FindGameObjectWithTag("CovidPic");
        covidPicture.SetActive(false);

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
        if (playerattribute.skill_duration.ContainsKey(currentSkill)) {
            currentSkills.Add(currentSkill, playerattribute.skill_duration[currentSkill]);
        } else
        {
            currentSkills.Add(currentSkill, time_testcool_down);
        }
        //currentSkills.Add(currentSkill, playerattribute.skill_duration[currentSkill]);
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
            //Sound wird gespielt, musik verlangsamt
            audioSource.PlayOneShot(audioClips[0], 1f);
            music.pitch=0.5f;
            //value is in playattr in futur
            Time.timeScale = 0.3f;
            skill.time = skill.time_testcool_down;

            //show ice picture
            icePicture.SetActive(true);
        }
        else if(status == EStatus.END)
        {
            //musik wieder schnell
            music.pitch=1f;

            //reset timescale
            Time.timeScale = 1f;
            //run after over

            //hideFlags ice picture
            icePicture.SetActive(false);
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
            audioSource.PlayOneShot(audioClips[1], 0.5f);
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
        if(status == EStatus.BEGIN)
        {
            audioSource.PlayOneShot(audioClips[2], 0.7f);
        }
        return false;
    }
    private bool magnet(EStatus status)
    {
        ///////////////////////////////
        ///     ADD SOUND HERE      ///
        ///////////////////////////////
        if (status == EStatus.BEGIN)
        {
            audioSource.PlayOneShot(audioClips[3], 0.3f);
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
        if (status == EStatus.BEGIN)
        {

            audioSource.PlayOneShot(audioClips[4], 0.5f);
            
            //Vector3 mouse_position = MouseInputHandler.mouse_position;

            foreach (GameObject human in Spawner.getHumans())
            {
                if (MouseInputHandler.getMouseDistance(human) <= bombRadius)
                {
                    Spawner.killHumans(human);
                }
            }

            //show bomb picture
            bombPicture.SetActive(true);
        }
        else if (status == EStatus.END)
        {

            //hide bombpic
            bombPicture.SetActive(false);
        }
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
            audioSource.PlayOneShot(audioClips[5], 1f);
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

        if (status == EStatus.BEGIN)
        {

            audioSource.PlayOneShot(audioClips[6], 0.5f);
            //show bomb picture
            covidPicture.SetActive(true);
        }
        else if (status == EStatus.END)
        {

            //hide bombpic
            covidPicture.SetActive(false);
        }
        return false;
    }

    private bool emergency(EStatus status)
    {
        ///////////////////////////////
        ///////////////////////////////

        if (status == EStatus.BEGIN)
        {
            audioSource.PlayOneShot(audioClips[7], 0.3f);

            foreach (GameObject g in Spawner.getHumans())
            {
                g.GetComponent<walking>().moveX(GameObject.FindGameObjectWithTag("Earth").GetComponent<Renderer>().bounds.center.x);
            }
        }
        else if (status == EStatus.END)
        {

            //hide bombpic
            covidPicture.SetActive(false);
        }
        return false;

        return false;
    }
}
