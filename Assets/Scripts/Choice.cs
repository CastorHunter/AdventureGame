using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    public GameObject choice1;
    public GameObject choice2;
    private GameObject sword;
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
            if (other.CompareTag("Player"))
            {
                Invoke("FinalChoice", 4.0f);
            }
        }
    void FinalChoice()
    {
        choice1.SetActive(true);
        choice2.SetActive(true);
        sword = GameObject.Find("Sword");
        sword.SetActive(false);
    }
}
