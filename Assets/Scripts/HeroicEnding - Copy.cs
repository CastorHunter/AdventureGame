using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroicEnding : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Sword"))
            {
                Invoke("Restart", 7.0f);
                player = GameObject.Find("Chien");
                player.SetActive(false);
                player = GameObject.Find("EndingPlayer");
                player.GetComponent<Renderer>().enabled = true;
                player = GameObject.Find("Player");
                Destroy(player);
                player = GameObject.Find("DontDestroyTheItems");
                Destroy(player);
                player = GameObject.Find("Menu Canvas To Save");
                Destroy(player);
                player = GameObject.Find("IconesCanvas");
                Destroy(player);
                player = GameObject.Find("SwordCenter");
                Destroy(player);
                player = GameObject.Find("LifeSystem");
                Destroy(player);
                player = GameObject.Find("Pray");
                Destroy(player);
            }
        }
    void Restart()
    {
        player = GameObject.Find("AudioManager");
        Destroy(player);
    }
}