using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool followingPlayer;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    public Transform weapon;
    public float offset;
    void Start()
    {
        if (other.CompareTag("Player"))
        {
        change = Vector3.zero;
        change.x = 1;
        change.y = 1;
        if (change != Vector3.zero)
        {
            Debug.Log(change);
            MoveCharacter();
        }
        }
    }
    void Update()
    {
        Vector3 displacement = weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = - Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0, 0, angle + offset);
    }
    void MoveCharacter()
    {
        myRigidbody.MovePosition(
            transform.position + change * speed
        );
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        change = Vector3.zero;
        change.x = 1;
        change.y = 1;
        if (change != Vector3.zero)
        {
            Debug.Log(change);
            MoveCharacter();
        }
        }
    }
}
