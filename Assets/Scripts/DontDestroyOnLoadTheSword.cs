using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadTheSword : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Shield;
    // Start is called before the first frame update
    void Start()
    {
        //évite la destruction au chargement de la scène
        DontDestroyOnLoad(Sword);
        DontDestroyOnLoad(Shield);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
