using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialog : MonoBehaviour
{
        private void Start()
    {
        this.GetComponent<Renderer>().enabled = false;
    }
    private void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Renderer>().enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Renderer>().enabled = false;
        }
    }
}