using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {    
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other) // est détruit si touche un sprite possédant le tag "Sword"
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
