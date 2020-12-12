using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyHandler : MonoBehaviour
{
    public static keyHandler handler;
    public GameObject cd;
    public cool_down cooldown;

    public void Awake()
    {
        if (handler != null)
        {
            return;
        }
        handler = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //cooldown = cd.GetComponent<cool_down>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(cooldown.key) && cooldown.available) 
        {
            Debug.Log("Event!!");
        }//*/
        foreach (KeyCode key in playerattribute.player.keySkill.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                playerattribute.player.useSkill(playerattribute.player.keySkill[key]);
            }
        }


    }
}
