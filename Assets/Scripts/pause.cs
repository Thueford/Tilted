using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pause : MonoBehaviour
{
    public Image panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<Image>();
        panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enterPause()
    {
        panel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void endPause()
    {
        panel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
