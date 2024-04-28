using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeModes : MonoBehaviour
{
    public GameObject otherMode;
    public GameObject RealMode;
    public GameObject otherControl;
    public GameObject LifeBar;
    public bool LifeMode = false;
    public bool Clavier = true;
    // Start is called before the first frame update
    void Start()
    {
        otherMode.SetActive(false);
        LifeBar.SetActive(false);
        otherControl.SetActive(true);
        Clavier = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q"))
        {
            otherMode.SetActive(false);
            RealMode.SetActive(true);
            LifeBar.SetActive(false);
            LifeMode = false;
        }

        if (Input.GetKey("e"))
        {
            otherMode.SetActive(true);
            RealMode.SetActive(false);
            LifeBar.SetActive(true);
            LifeMode = true;
        }

        if (Input.GetKey("r"))
        {
            otherControl.SetActive(false);
            Clavier = false;
        }

        if (Input.GetKey("t"))
        {
            otherControl.SetActive(true);
            Clavier = true;
        }
    }
}
