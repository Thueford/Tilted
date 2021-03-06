using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    public static MouseInputHandler Instance;

    public static Vector3 mouse_position;
    private List<GameObject> picked;
    //public List<GameObject> follow_mouse;
    public GameObject follow_mouse;

    public AudioSource audioSource;
    public AudioClip[] pickSounds;
    public AudioClip[] dropSounds;

    public static int num_max_humans = 2;
    private int pickup_radius = 15;

    void Awake()
    {
        if (Instance != null)
        {
            //Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
        picked = new List<GameObject>();
        //follow_mouse = new List<GameObject>();
        //GameObject follow_mouse;
    }

    // Update is called once per frame
    void Update()
    {
        mouse_position = Input.mousePosition;
        mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);
        mouse_position.z = 0;

        /*foreach (GameObject obj in follow_mouse)
        {
            //follow mouse
            obj.transform.position = mouse_position;
        }//*/
        if (follow_mouse)
        {
            follow_mouse.transform.position = mouse_position;
        }



        //checks for mouse events
        if (Input.GetMouseButtonDown(0))
        {
            GameObject[] humans = playerattribute.getNearestMouseHumans();

            if (Skill.skill.currentSkill != Skill.ESkill.NONE)
            {
                //sets start var true
                if (cool_down.cool_Downs[Skill.skill.currentSkill].isAvailable())
                {
                    cool_down.cool_Downs[Skill.skill.currentSkill].keystat = true;
                    cool_down.cool_Downs[Skill.skill.currentSkill].available = false;
                    if (Skill.skill.currentSkill != Skill.ESkill.WALL)
                    {
                        Destroy(follow_mouse);
                    }
                    Skill.skill.runAbility();
                    //Skill.skill.run_skill = true;

                    //foreach (GameObject obj in follow_mouse)
                    //{
                    //destroyobj again

                    //if maybe delete
                    //Debug.Log(Skill.skill.currentSkill);
                    //Debug.Log(Skill.ESkill.WALL);
                    
                        
                    //}
                    //follow_mouse.Clear();
                } else
                {
                    if (follow_mouse && Skill.skill.currentSkill != Skill.ESkill.WALL)
                    {
                        Destroy(follow_mouse);
                    }
                }
                
            }

            for (int i = 0; i<num_max_humans; i++)
            {
                if (humans.Length >= i)
                {
                    if (Vector3.Distance(humans[i].transform.position, mouse_position) <= pickup_radius)
                    {
                        audioSource.PlayOneShot(pickSounds[new System.Random().Next(0, pickSounds.Length)], 0.8f);
                        picked.Add(humans[i]);
                        //Debug.Log("pick");
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (picked.Count > 0) {
                audioSource.PlayOneShot(dropSounds[new System.Random().Next(0, dropSounds.Length)], 0.8f);
            }
            Debug.Log("clear");
            picked.Clear();
        }

        //if picked not empty make follow mouse
        if (picked.Count > 0)
            foreach (GameObject g in picked)
                g.transform.position = mouse_position;
    }

    float distanceOf(Vector3 go, Vector3 mouse)
    {
        return Mathf.Abs(mouse.x - go.x + mouse.y - go.y);
    }

    public static float getMouseDistance(GameObject o)
    {
        return Vector3.Distance(o.transform.position, MouseInputHandler.mouse_position);
    }
}
