using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    public Transform weapon;
    public float offset;
    public GameObject Sword;
    public GameObject Shield;
    public GameObject SwordLook;
    private GameObject ActualWeapon;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this);
        ChangeSword();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (change != Vector3.zero)
        {
            MoveCharacter();
        }

        if (Input.GetKey("q"))
        {
            ChangeSword();
        }

        if (Input.GetKey("e"))
        {
            ChangeShield();
        }
        //Debug.Log(change); //pour debug

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
    void ChangeSword()
    {
        Shield.SetActive(false);
        SwordLook.SetActive(true);
        ActualWeapon = Sword;
        weapon = ActualWeapon.transform;
    }
    void ChangeShield()
    {
        SwordLook.SetActive(false);
        Shield.SetActive(true);
        ActualWeapon = Shield;
        weapon = ActualWeapon.transform;
    }
}


