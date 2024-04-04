using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public AudioClip TLaugh; //Audio
    public float moveSpeed = 5f; // Vitesse de déplacement de l'ennemi
    private Transform player; // Référence au joueur
    private bool canMove = false; //true si l'ennemi peut bouger
    private bool isTouchingPray = false; //true si l'ennemi touche la prière du joueur
    AudioSource audioSource; //Audio

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouver le joueur
        audioSource = GetComponent<AudioSource>(); // Audio

    }
    private void Update()
    {
        //vérifie si l'ennemi peut bouger et si il ne touche pas la prière, et si c'est le cas l'autorise à bouger
        if (canMove == true && isTouchingPray == false){
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
        if (other.CompareTag("Player")) //l'ennemi repère le joueur et peut donc bouger
        {
            canMove = true;
            audioSource.PlayOneShot(TLaugh, 0.7F);
        }
        if (other.CompareTag("Pray")) //l'ennemi touche la prière et ne peut donc pas bouger
        {
            isTouchingPray = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //l'ennemi ne voit plus le joueur et ne peut donc pas bouger
        {
            canMove = false;
        }
        if (other.CompareTag("Pray")) //l'ennemi ne touche pas la prière et peut donc bouger
        {
            isTouchingPray = false;
        }
    }
}