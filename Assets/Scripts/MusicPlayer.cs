using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    static MusicPlayer instance = null;

	// Use this for initialization
	void Awake () {
        //if (MusicPlayerManager.musicPlayerCount < 1)
        //{

        if (instance != null)
        {
            Destroy(gameObject);
            //print("Duplicate music player is self-destructing");
        }
        else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

        //    MusicPlayerManager.musicPlayerCount++;
        //}
        //else {
        //    Destroy(gameObject);
        //}

        //Debug.Log("Awake() " + GetInstanceID());

    }

}

//public static class MusicPlayerManager{
//    public static int musicPlayerCount = 0;
//}
