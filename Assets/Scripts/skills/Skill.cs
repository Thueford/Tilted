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
            {"Freeze", freeze}
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
            ability[currentSkill].DynamicInvoke("test");
            time -= Time.deltaTime;
            if (time <= 0)
            {
                run_skill = false;
            }
        }
    }

    private bool freeze(string a)
    {

        Debug.Log("Freeze");
        return false;
    }

    
}
