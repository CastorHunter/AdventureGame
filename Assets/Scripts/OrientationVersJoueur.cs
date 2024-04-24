using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationVersJoueur : MonoBehaviour
{
    public float offset;
    private Transform player; // Référence au joueur
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouver le joueur
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = gameObject.transform.position - player.position;
        float angle = - Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;//à toucher iciiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + offset);
    }
}
