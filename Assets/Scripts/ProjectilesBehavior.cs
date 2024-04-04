using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesBehavior : MonoBehaviour
{
    public float offset;
    public float Speed = 4.5f;    // Start is called before the first frame update
    void Start()
    {
        Vector3 displacement = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = - Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + offset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
