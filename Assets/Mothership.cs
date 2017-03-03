using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour {
    static Mothership mInstance = null;

    void Awake()
    {
        if (mInstance != null)
        {
            Destroy(gameObject);
            //print("Duplicate music player is self-destructing");
        }
        else {
            mInstance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

    }
}
