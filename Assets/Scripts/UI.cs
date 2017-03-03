using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {
    static UI instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            //print("Duplicate music player is self-destructing");
        }
        else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

    }
}
