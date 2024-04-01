using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDestroyedBySword : MonoBehaviour
{
    private void Start()
    {  
    }
    private void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            Destroy(gameObject);
        }
    }
}