using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public ProjectilesBehavior ProjectilePrefab;
    public Transform LaunchOffset;
    private Vector3 CheckpointLocation;
    public AudioClip WaterSplash;
    public float speed = 0.05F;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    public Transform weapon;
    public Transform swordcenter;
    public float offset;
    private int scene;
    // 1 : HouseZone
    // 2 : Village
    // 3 : Zone3
    // 4 : KrysseTomb
    // 5 : Church
    private bool canFire = false;
    private bool hasSword = false;
    private bool hasPray = false;
    private bool hasWater = false;
    private bool Life = false;
    private int pv = 6;
    public GameObject Sword;
    public GameObject Shield;
    public GameObject SwordLook;
    public GameObject Water;
    private GameObject LifeSystem;
    private GameObject LifeBar1;
    private GameObject LifeBar2;
    private GameObject LifeBar3;
    private GameObject ActualCheckPoint;
    private GameObject ActualWeapon;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this);
        ChangeNothing();
        SwordLook.SetActive(false);
        LifeBar1 = GameObject.Find("Life1/3");
        LifeBar2 = GameObject.Find("Life2/3");
        LifeBar3 = GameObject.Find("Life3/3");
        LifeSystem = GameObject.Find("LifeSystem");
        LifeModes life = LifeSystem.GetComponent<LifeModes>(); // Obtient une référence au script de la gestion de points de vie
        Life = life.LifeMode;
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

        if (Input.GetKey("q") && hasSword == true)
        {
            ChangeSword();
        }
        if (Input.GetKey("t"))
        {
            ChangeNothing();
        }
        if (Input.GetKey("e") && hasPray == true)
        {
            ChangeShield();
        }
        if (Input.GetKey("r") && hasWater == true)
        {
            ChangeWater();
        }
        //Debug.Log(change); //pour debug

        Vector3 displacement = weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = - Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0, 0, angle + offset);

        swordcenter.rotation = weapon.rotation;

        if(Input.GetButtonDown("Fire1") && canFire == true){
            audioSource.PlayOneShot(WaterSplash, 0.7F);
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
        if(pv == 6)
        {
            LifeBar1.SetActive(true);
            LifeBar2.SetActive(true);
            LifeBar3.SetActive(true);
        }
        if(pv < 6)
        {
            LifeBar3.SetActive(false);
        }
        if(pv < 4)
        {
            LifeBar2.SetActive(false);
        }
        if(pv < 2)
        {
            LifeBar1.SetActive(false);
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
    void ChangeNothing()
    {
        Shield.SetActive(false);
        Water.SetActive(false);
        SwordLook.SetActive(false);
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LakeOrRiverWater"))
        {
            speed = 0.01F;
        }
        if (other.CompareTag("GetSword"))
        {
            hasSword = true;
        }
        if (other.CompareTag("GetWater"))
        {
            hasWater = true;
        }
        if (other.CompareTag("GetPray"))
        {
            hasPray = true;
        }
        if (other.CompareTag("Enemy"))
        {
            if (Life == true && pv > 0)
            {
                this.GetComponent<Collider2D>().enabled = false;
                pv = pv -1;
                Invoke("recreateCollider", 2.0f);
            }
            else
            {
                Die();
                pv = 6;
            }
        }
        if (other.CompareTag("Checkpoint1"))//HouseZone
        {
            ActualCheckPoint = other.gameObject;
            CheckpointLocation = ActualCheckPoint.transform.position;
            scene = 1;
        }
        if (other.CompareTag("Checkpoint2"))//Village
        {
            ActualCheckPoint = other.gameObject;
            CheckpointLocation = ActualCheckPoint.transform.position;
            scene = 2;
        }
        if (other.CompareTag("Checkpoint3"))//Zone3
        {
            ActualCheckPoint = other.gameObject;
            CheckpointLocation = ActualCheckPoint.transform.position;
            scene = 3;
        }
        if (other.CompareTag("Checkpoint4"))//KrysseTombe
        {
            ActualCheckPoint = other.gameObject;
            CheckpointLocation = ActualCheckPoint.transform.position;
            scene = 4;
        }
        if (other.CompareTag("Checkpoint5"))//Church
        {
            ActualCheckPoint = other.gameObject;
            CheckpointLocation = ActualCheckPoint.transform.position;
            scene = 5;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("LakeOrRiverWater"))
        {
            speed = 0.05F;
        }
    }
    void Die()
    {
        if(scene == 1)//HouseZone
        {
            SceneManager.LoadScene(2);
        }
        if(scene == 2)//Village
        {
            SceneManager.LoadScene(7);
        }
        if(scene == 3)//Zone3
        {
            SceneManager.LoadScene(11);
        }
        if(scene == 4)//KrysseTombe
        {
            SceneManager.LoadScene(13);
        }
        if(scene == 5)//Church
        {
            SceneManager.LoadScene(5);
        }
        transform.position = CheckpointLocation;
    }
    void recreateCollider()
    {
        this.GetComponent<Collider2D>().enabled = true;
    }
}


