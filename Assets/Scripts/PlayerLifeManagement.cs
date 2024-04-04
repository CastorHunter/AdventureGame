using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManagement : MonoBehaviour
{
    //VARIABLES LIEES AU PVS
    private int pv = 3;
    public GameObject lifebar2pv;
    public GameObject lifebar3pv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GESTION DES PV
        if(pv == 1)
        {
            lifebar2pv.GetComponent<Renderer>().enabled = false;
        }
        if(pv == 2)
        {
            lifebar3pv.GetComponent<Renderer>().enabled = false;
        }
        
    }
}
