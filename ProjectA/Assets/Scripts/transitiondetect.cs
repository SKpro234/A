using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitiondetect : MonoBehaviour
{

    public LevelLoader LevelScript;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Scene")
        {
            LevelScript.LoadNextLevel();
        }
    }
}

