using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeze : MonoBehaviour
{
    /*  Freeze skill
     *  makes time slower?
    */
    private GameObject[] humans;
    private float freeze_time = 10;
    private float time;

    private float freezed_speed = 0.5f;
    private float normal_speed = 1f;

    void Start()
    {
        /////////////////////////////////////////
        ////////    ADD SOUNDS HERE    //////////
        /////////////////////////////////////////

        //setSpeed(freezed_speed);
        Time.timeScale = freezed_speed;
        time = freeze_time;
    }
    /*
    private void setSpeed(float speed)
    {
        humans = Spawner.getHumans();
        foreach (GameObject human in humans)
        {
            human.GetComponent<walking>().speed = freezed_speed;
            time = freeze_time;
        }
    }//*/

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Time.timeScale = normal_speed;
            //setSpeed(normal_speed);
        }
    }
}
