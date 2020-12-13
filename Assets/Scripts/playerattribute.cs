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

    private void Awake()
    {
        if (player != null) return;
        player = this;

        keySkill = new Dictionary<KeyCode, Skill.ESkill>()
        {
            {KeyCode.Q, Skill.ESkill.FREEZE},
            {KeyCode.W, Skill.ESkill.SHOCK},
            {KeyCode.E, Skill.ESkill.WALL},
            {KeyCode.R, Skill.ESkill.MAGNET},
            {KeyCode.T, Skill.ESkill.BOMB},
            {KeyCode.Z, Skill.ESkill.CLIMB},
            {KeyCode.U, Skill.ESkill.COVID19},
            {KeyCode.Space, Skill.ESkill.EMERGENCY}
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
