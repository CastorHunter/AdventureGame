using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeModes : MonoBehaviour
{
    public GameObject otherMode;
    public GameObject LifeBar;
    public bool LifeMode = false;
    // Start is called before the first frame update
    void Start()
    {
        otherMode.SetActive(false);
        LifeBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q"))
        {
            otherMode.SetActive(false);
            LifeBar.SetActive(false);
        }

        if (Input.GetKey("e"))
        {
            otherMode.SetActive(true);
            LifeBar.SetActive(true);
        }
    }
}
