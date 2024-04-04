using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Play : MonoBehaviour
{
    void Start() //Lance la scène 1
    {
        SceneManager.LoadScene(1);
    }
}