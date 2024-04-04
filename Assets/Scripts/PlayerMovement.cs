using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ProjectilesBehavior ProjectilePrefab;
    public Transform LaunchOffset;
    public AudioClip WaterSplash;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    public Transform weapon;
    public float offset;
    private bool canFire = false;
    public GameObject Sword;
    public GameObject Shield;
    public GameObject SwordLook;
    public GameObject Water;
    private GameObject ActualWeapon;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this);
        ChangeSword();
        SwordLook.SetActive(false);
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
        if (Input.GetKey("r"))
        {
            ChangeWater();
        }
        //Debug.Log(change); //pour debug

        Vector3 displacement = weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = - Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0, 0, angle + offset);

        if(Input.GetButtonDown("Fire1") && canFire == true){
            audioSource.PlayOneShot(WaterSplash, 0.7F);
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
    }
    void MoveCharacter()
    {
        myRigidbody.MovePosition(
            transform.position + change * speed
        );
    }
    //CHANGEMENT D'ARMES
    void ChangeSword()
    {
        Shield.SetActive(false);
        Water.SetActive(false);
        SwordLook.SetActive(true);
        canFire = false;
        ActualWeapon = Sword;
    }
    void ChangeShield()
    {
        SwordLook.SetActive(false);
        Water.SetActive(false);
        Shield.SetActive(true);
        canFire = false;
        ActualWeapon = Shield;
    }
    void ChangeWater()
    {
        Shield.SetActive(false);
        SwordLook.SetActive(false);
        Water.SetActive(true);
        canFire = true;
        ActualWeapon = Water;
    }
}


