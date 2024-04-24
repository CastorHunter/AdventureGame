using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    private bool Life = false;
    private GameObject LifeSystem;
    public HeartScript HeartPrefab;
    public GameObject Enemy;
    private void Start()
    {
        LifeSystem = GameObject.Find("LifeSystem");
        LifeModes life = LifeSystem.GetComponent<LifeModes>(); // Obtient une référence au script de la gestion de points de vie
        Life = life.LifeMode;
    }
    private void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other) // est détruit si touche un sprite possédant le tag "Sword"
    {
        if (other.CompareTag("Sword"))
        {
            Destroy(Enemy);
            if (Life == true)
            {
            Instantiate(HeartPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}