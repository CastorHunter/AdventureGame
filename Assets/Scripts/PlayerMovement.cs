using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    // 6 : VillageCursed
    // 7 : ChurchCursed
    // 8 : HouseZoneCursed
    private bool canFire = false;
    private bool hasSword = false;
    private bool hasPray = false;
    private bool hasWater = false;
    private bool Life = false;
    private bool GameMode = true;
    private int pv = 6;
    public GameObject Sword;
    public GameObject Shield;
    public GameObject SwordLook;
    public GameObject Water;
    public Image InventoryEmpty;
    public Image InventorywithPray;
    public Image InventorywithSword;
    public Image InventorywithWater;
    private Image Inventory;
    private GameObject LifeSystem;
    private GameObject LifeBarTempo;
    private GameObject bloquage;
    private GameObject musique;
    private Image LifeBar0;
    private Image LifeBar1;
    private Image LifeBar2;
    private Image LifeBar3;
    public Image HandsIcone;
    public Image PrayIcone;
    public Image SwordIcone;
    public Image WaterIcone;
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
        LifeSystem = GameObject.Find("LifeSystem");
        Inventory = InventoryEmpty;//porte de Shar T-T
        LifeModes life = LifeSystem.GetComponent<LifeModes>(); // Obtient une référence au script de la gestion de points de vie
        Life = life.LifeMode;
        LifeModes gamemode = LifeSystem.GetComponent<LifeModes>();
        GameMode = gamemode.Clavier;
        LifeBarTempo = GameObject.Find("Life1/3");
        LifeBar1 = LifeBarTempo.GetComponent<Image>();
        LifeBarTempo = GameObject.Find("Life2/3");
        LifeBar2 = LifeBarTempo.GetComponent<Image>();
        LifeBarTempo = GameObject.Find("Life3/3");
        LifeBar3 = LifeBarTempo.GetComponent<Image>();
        LifeBarTempo = GameObject.Find("LifeEmpty");
        LifeBar0 = LifeBarTempo.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMode == true)
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            if (change != Vector3.zero)
            {
                MoveCharacter();
            }
        }
        else
        {
            float moveHorizontal = Input.GetAxis("JoystickHorizontal");
            float moveVertical = Input.GetAxis("JoystickVertical");
            Vector3 movement = new Vector3(moveHorizontal, -moveVertical, 0.0f);
            transform.Translate(movement * speed*50 * Time.deltaTime);
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
        // if (Input.GetKey("JoystickA"))
        // {
        //     Debug.Log("oulalalala");
        //     // ChangeShield();
        // }
        if (Input.GetKey("r") && hasWater == true)
        {
            ChangeWater();
        }
        if (Input.GetKey("f"))
        {
            Debug.Log(Inventory);
            Inventory.enabled = true;
        }
        else
        {
            Inventory.enabled = false;
        }
        //Debug.Log(change); //pour debug

        if(Input.GetButtonDown("Fire1") && canFire == true){
            audioSource.PlayOneShot(WaterSplash, 0.7F);
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
        if (GameMode == true)
        {
        Vector3 displacement = weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = - Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0, 0, angle + offset);

        swordcenter.rotation = weapon.rotation;
        }
        else
        {
            float moveHorizontal = Input.GetAxis("JoystickHorizontal");
            float moveVertical = Input.GetAxis("JoystickVertical");
            Vector3 displacement = new Vector3(-moveHorizontal, moveVertical, 0.0f);
            float angle = - Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;
            weapon.rotation = Quaternion.Euler(0, 0, angle + offset);
            swordcenter.rotation = weapon.rotation;
        }

        if(pv == 6)
        {
            LifeBar3.enabled = true;
            LifeBar0.enabled = false;
            LifeBar2.enabled = false;
            LifeBar1.enabled = false;
        }
        if(pv < 6 && pv > 3)
        {
            LifeBar2.enabled = true;
            LifeBar3.enabled = false;
        }
        if(pv < 4 && pv > 1)
        {
            LifeBar1.enabled = true;
            LifeBar2.enabled = false;
        }
        if(pv < 2)
        {
            LifeBar0.enabled = true;
            LifeBar1.enabled = false;
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
        if (other.CompareTag("Bateau"))
        {
            SceneManager.LoadScene(18);
            transform.position = new Vector3(23,-76,-2);
            musique = GameObject.Find("Ambiance");
            musique.SetActive(false);
            musique = GameObject.Find("CursedWorld");
            musique.GetComponent<AudioSource>().enabled = true;
        }
        if (other.CompareTag("GetSword"))
        {
            SwordIcone.enabled = true;
            hasSword = true;
            Inventory = InventorywithSword;
        }
        if (other.CompareTag("GetWater"))
        {
            WaterIcone.enabled = true;
            hasWater = true;
            Inventory = InventorywithWater;
        }
        if (other.CompareTag("GetPray"))
        {
            HandsIcone.enabled = true;
            PrayIcone.enabled = true;
            hasPray = true;
            Inventory = InventorywithPray;
        }
        if (other.CompareTag("Enemy"))
        {
            if (Life == true && pv >= 0)
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
        if (other.CompareTag("Echelle"))
        {
            if (hasSword == true)
            {
                SceneManager.LoadScene(23);
            }
            else
            {
                SceneManager.LoadScene(2);
            }
        }
        if (other.CompareTag("BloqueForPray"))
        {
            if (hasPray == true)
            {
                bloquage = GameObject.Find("BloquageForPray");
                Destroy(bloquage);
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
        if (other.CompareTag("Checkpoint6"))//VillageCursed
        {
            ActualCheckPoint = other.gameObject;
            CheckpointLocation = ActualCheckPoint.transform.position;
            scene = 6;
        }
        if (other.CompareTag("Checkpoint7"))//ChurchCursed
        {
            ActualCheckPoint = other.gameObject;
            CheckpointLocation = ActualCheckPoint.transform.position;
            scene = 7;
        }
        if (other.CompareTag("Checkpoint8"))//HouseZoneCursed
        {
            ActualCheckPoint = other.gameObject;
            CheckpointLocation = ActualCheckPoint.transform.position;
            scene = 8;
        }
        if (other.CompareTag("Heart"))
        {
            pv = 6;
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
        if(scene == 6)//VillageCursed
        {
            SceneManager.LoadScene(18);
        }
        if(scene == 7)//ChurchCursed
        {
            SceneManager.LoadScene(20);
        }
        if(scene == 8)//HouseZoneCursed
        {
            SceneManager.LoadScene(23);
        }
        transform.position = CheckpointLocation;
    }
    void recreateCollider()
    {
        this.GetComponent<Collider2D>().enabled = true;
    }
}
// Bouton A = PB (joystick button 0) | Key or Mouse Button | Get Motion from all Joystick
// Bouton B = PB (joystick button 1) | Key or Mouse Button | Get Motion from all Joystick
// Bouton X = PB (joystick button 2) | Key or Mouse Button | Get Motion from all Joystick
// Bouton Y = PB (joystick button 3) | Key or Mouse Button | Get Motion from all Joystick
// Bouton LB = PB (joystick button 4) | Key or Mouse Button | Get Motion from all Joystick
// Bouton RB = PB (joystick button 5) | Key or Mouse Button | Get Motion from all Joystick
// Bouton Back = PB (joystick button 6) | Key or Mouse Button | Get Motion from all Joystick
// Bouton Start = PB (joystick button 7) | Key or Mouse Button | Get Motion from all Joystick
// Clic Joystick Gauche = PB (joystick button 8) | Key or Mouse Button | Get Motion from all Joystick
// Clic Joystick Droite = PB (joystick button 9) | Key or Mouse Button | Get Motion from all Joystick