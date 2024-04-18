using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifJoueurpourVide : MonoBehaviour
{
    public GameObject Vide;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vide.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vide.SetActive(false);
        }
    }
}
