using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class cool_down : MonoBehaviour
{
    //public static List<cool_down> cool_Downs = new List<cool_down>();
    public static Dictionary<Skill.ESkill, cool_down> cool_Downs = new Dictionary<Skill.ESkill, cool_down>();
    public Skill.ESkill icon_name;
    public float cooldown_time;
    public float time;
    public bool available = true;
    public bool keystat;
    public Text txtTime;
    public bool executeOnes = false;

    private void Awake()
    {
        //cool_Downs.Add(this);
        if(!cool_Downs.ContainsKey(icon_name)) cool_Downs.Add(icon_name, this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerattribute.cool_down_time.ContainsKey(icon_name))
        {
            cooldown_time = playerattribute.cool_down_time[icon_name];
        } else
        {
            Debug.LogError("EY DER COOLDOWN GEHT NICHT");
        }
        
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
            if (!executeOnes)
            {
                txtTime.text = time.ToString("0.0");
            }
        }
        else
        {
            KeyCode key = playerattribute.keySkill.FirstOrDefault(o => o.Value == icon_name).Key;
            txtTime.text = key == 0 ? "0.0" : char.ConvertFromUtf32((int)key).ToUpper();
            if (!executeOnes)
            {
                time = cooldown_time;
                available = true;
                keystat = false;
            }
            
        }
    }
}
