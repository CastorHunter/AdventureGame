using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialog : MonoBehaviour
{
        private void Start()
    {
        this.GetComponent<Renderer>().enabled = false; //cache le dialogue
    }
    private void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //si le joueur est à distance de parole du pnj, le dialogue apparaît
        {
            this.GetComponent<Renderer>().enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //si le joueur n'est plus à bonne distance de parole du pnj, le dialogue apparaît
        {
            this.GetComponent<Renderer>().enabled = false;
        }
    }
}