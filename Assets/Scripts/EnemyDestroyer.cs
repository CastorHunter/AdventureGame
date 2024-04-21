using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    public GameObject Enemy;
    private void Start()
    {  
    }
    private void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other) // est détruit si touche un sprite possédant le tag "Sword"
    {
        if (other.CompareTag("Sword"))
        {
            Destroy(Enemy);
        }
    }
}