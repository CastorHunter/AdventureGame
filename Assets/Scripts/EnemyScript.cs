using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public AudioClip TLaugh;
    public float moveSpeed = 5f; // Vitesse de déplacement de l'ennemi
    private Transform player; // Référence au joueur
    private bool canMove = false;
    AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouver le joueur
        audioSource = GetComponent<AudioSource>();

    }
    private void Update()
    {
        if (canMove == true){
            Move();
        }
    }
    private void Move()
    {
        if (player != null)
        {
            // Calculer la direction du mouvement vers le joueur
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Appliquer le mouvement
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canMove = true;
            audioSource.PlayOneShot(TLaugh, 0.7F);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canMove = false;
        }
    }
}