using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cool_down : MonoBehaviour
{
    public static List<cool_down> cool_Downs = new List<cool_down>();
    public string icon_name = "";
    public float cooldown_time;
    public float time;
    public bool available = true;
    public bool keystat;
    public Text txtTime;

    private void Awake()
    {
        cool_Downs.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        time = cooldown_time;
    }

    public bool isAvailable()
    {
        return available;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(key))
        {
            keystat = true;
            available = false;
        }//*/

        if(time > 0 && keystat)
        {
            time -= Time.deltaTime;
            GetComponent<Image>().fillAmount = time / cooldown_time;
            txtTime.text = time.ToString("F");
        } else
        {
            txtTime.text = "0.00";
            keystat = false;
            time = cooldown_time;
            available = true;
        }
    }
}
