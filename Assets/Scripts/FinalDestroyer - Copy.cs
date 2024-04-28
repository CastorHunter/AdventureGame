using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDestroyer : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
            player = GameObject.Find("Player");
            Destroy(player);
            player = GameObject.Find("MainCamera");
            Destroy(player);
            player = GameObject.Find("DontDestroyTheItems");
            Destroy(player);
            player = GameObject.Find("AudioManager");
            Destroy(player);
            player = GameObject.Find("Menu Canvas To Save");
            Destroy(player);
            player = GameObject.Find("IconesCanvas");
            Destroy(player);
            player = GameObject.Find("LifeSystem");
            Destroy(player);
    }
}
