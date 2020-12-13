using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattribute : MonoBehaviour
{
    public static playerattribute player;
    public int xp;
    public float time;
    public ArrayList highscore;
    public int anzMenschen;
    public int dragDropAnz;
    public static Dictionary<KeyCode, Skill.ESkill> keySkill;
    public static Dictionary<Skill.ESkill, GameObject> has_follow_mouse;
    public GameObject bombPrefab;
    public GameObject magnetPrefab;
    public static Dictionary<Skill.ESkill, float> cool_down_time;
    public static Dictionary<Skill.ESkill, float> skill_duration;

    private void Awake()
    {
        if (player != null) return;
        player = this;

        keySkill = new Dictionary<KeyCode, Skill.ESkill>()
        {
            {KeyCode.A, Skill.ESkill.FREEZE},
            {KeyCode.W, Skill.ESkill.SHOCK},
            {KeyCode.S, Skill.ESkill.WALL},
            {KeyCode.Y, Skill.ESkill.MAGNET},
            {KeyCode.Q, Skill.ESkill.BOMB},
            {KeyCode.X, Skill.ESkill.CLIMB},
            {KeyCode.C, Skill.ESkill.COVID19},
            {KeyCode.Space, Skill.ESkill.EMERGENCY}
        };

        has_follow_mouse = new Dictionary<Skill.ESkill, GameObject>()
        {
            {Skill.ESkill.BOMB, bombPrefab},
            {Skill.ESkill.MAGNET, magnetPrefab},
        };

        cool_down_time = new Dictionary<Skill.ESkill, float>() {
            {Skill.ESkill.FREEZE, 20f},
            {Skill.ESkill.SHOCK, 15f},
            {Skill.ESkill.WALL, 20f},
            {Skill.ESkill.MAGNET, 25f},
            {Skill.ESkill.BOMB, 15f},
            {Skill.ESkill.CLIMB, 20f},
            {Skill.ESkill.COVID19, 25f}
        };

        skill_duration = new Dictionary<Skill.ESkill, float>() {
            {Skill.ESkill.FREEZE, 3f},
            {Skill.ESkill.WALL, 5f},
            {Skill.ESkill.MAGNET, 10f},
            {Skill.ESkill.CLIMB, 5f},
        };


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static GameObject[] getNearestMouseHumans()
    {
        GameObject[] l = Spawner.getHumans();
        System.Array.Sort(l, (a, b) => MouseInputHandler.getMouseDistance(a) < MouseInputHandler.getMouseDistance(b) ? -1 : 1 );
        return l;
    }

    public static GameObject[] getHumansInMouseRange(float radius)
    {
        GameObject[] l = Array.FindAll(Spawner.getHumans(), o => MouseInputHandler.getMouseDistance(o) < radius);
        Array.Sort(l, (a, b) => MouseInputHandler.getMouseDistance(a) < MouseInputHandler.getMouseDistance(b) ? -1 : 1);
        return l;
    }

    public void useSkill(Skill.ESkill key)
    {
         //falls erwünscht wieder nutzen
        /*if (!cool_down.cool_Downs[key].isAvailable())
        {
            return;
        }//*/

        //Wählt skill aus jedoch führt den nicht aus
        if (Skill.skill.ability.ContainsKey(key))
        {
            //hier vorher auf icon cooldown checken
            //time auf cooldowntime setzten
            Skill.skill.currentSkill = key;

            if (has_follow_mouse.ContainsKey(key))
            {
                GameObject follow_mouse_object = Instantiate(has_follow_mouse[key], MouseInputHandler.mouse_position, Quaternion.identity);
                //MouseInputHandler.Instance.follow_mouse.Add(follow_mouse_object);
                if (MouseInputHandler.Instance.follow_mouse)
                {
                    Destroy(MouseInputHandler.Instance.follow_mouse);
                }
                MouseInputHandler.Instance.follow_mouse = follow_mouse_object;
            }

            //bomb follow
            //if in list it icon spawn
            //GameObject a = Instantiate(prefabs[i], new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(!GAMEOVER) { ... }
        time = Time.time;
    }
    
  
    // Highscore
    public void newHighscore(float value)
    {
        highscore = new ArrayList();
        highscore.Add(value);
    }

    public float getHighscore()
    {
        return time; // time = hightscore
    }

    // XP Methoden
    private int getXP()
    {
        return xp;
    }

    public void setXP(int newXP)
    {
        xp = newXP;
    }

    public void addToXP(int value)
    {
        xp += value;
    }

    public void subToXP(int value)
    {
        xp -= value;
    }

    // Anzahl der Menschen
    public int getAnzMenschen()
    {
        return Spawner.getHumans().Length;
    }

    // Anzahl der gedrag und dropten Menschen
    public int getMaxDragDrop()
    {
        return MouseInputHandler.num_max_humans;
    }

    public void setMaxDragDrp(int value)
    {
        MouseInputHandler.num_max_humans = value;
    }
}
