using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour {

    int poolCount;
    public int index;
    List<GameObject> missileArray;

    public GameObject missilePrefab;

    void Awake() {
        missileArray = new List<GameObject>();
        poolCount = 10;
        index = 0;
        for (int i = 0; i < poolCount; i++) {
            missileArray.Add(Instantiate(
                missilePrefab, 
                transform.position, 
                transform.rotation
            ));
            missileArray[i].transform.SetParent(transform);
            missileArray[i].SetActive(false);
        }
        
    }

    public void FireShot(Vector3 target) {
        Debug.Log("FireShot()");
        while (true) {
            if (!missileArray[index].activeSelf)
            {
                missileArray[index].SetActive(true);
                missileArray[index].GetComponent<Missile>().SetTarget(target);
                break;
            }
            else if (index == (poolCount-1)) {//if we're at the end, wait;
                missileArray[index].GetComponent<Missile>().ResetMissile();
            }
            index++;
            index = index % poolCount;
            
        }
        
    }

}
