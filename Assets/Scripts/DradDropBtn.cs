using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DradDropBtn : MonoBehaviour
{
    public Text txtDaD;

    // Update is called once per frame
    void Update()
    {
        int text = MouseInputHandler.num_max_humans;
        txtDaD.text = text.ToString();

    }
}
