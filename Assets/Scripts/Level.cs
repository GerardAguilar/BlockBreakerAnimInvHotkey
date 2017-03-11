using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    //public string nextLevel;

    public int currentLevelBoxCount = 0;
    LevelManager levelManager;

    void Awake() {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    //void Update()
    //{

    //}

    public void IncreaseBoxCount()
    {
        currentLevelBoxCount++;
    }

    public void DecreaseBoxCount()
    {
        currentLevelBoxCount--;
        if (currentLevelBoxCount == 0)
        {//this shouldn't automatically trigger since Awake functions would increase the box count
            levelManager.NextLevel();
        }
    }


}
