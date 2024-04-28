using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PorteEnd : MonoBehaviour
{
    private GameObject musique;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(16);
            musique = GameObject.Find("CursedWorld");
            musique.SetActive(false);
            musique = GameObject.Find("BossTheme");
            musique.GetComponent<AudioSource>().enabled = true;
        }
    }
}
