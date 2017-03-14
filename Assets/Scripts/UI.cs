using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    static UI instance = null;

    //[SerializeField]
    //private int buildIndex;

    //[SerializeField]
    //private int sceneTotalCount;

    void Awake()
    {
        //buildIndex = SceneManager.GetActiveScene().buildIndex;
        //sceneTotal = SceneManager.sceneCount;//sceneCount only counts active scenes.
        if (instance != null)
        {
            Destroy(gameObject);
            //print("Duplicate music player is self-destructing");
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}
