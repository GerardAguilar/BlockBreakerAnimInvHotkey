using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour {

    int poolCount;
    public int index;
    List<GameObject> missileArray;

    public GameObject missilePrefab;

    RadarSystem radarSystem;

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
        radarSystem = transform.parent.GetComponentInChildren<RadarSystem>();
        
    }

    public IEnumerator FireShot()
    {
        //Debug.Log("FireShot()");
        while (true) {
            if (!missileArray[index].activeSelf)
            {
                missileArray[index].GetComponent<Missile>().SetTarget(radarSystem.WheresThePlayer());
                missileArray[index].SetActive(true);
                break;
            }
            //else if (index == (poolCount - 1))
            //{//if we're at the end, wait;
            //    missileArray[index].GetComponent<Missile>().ResetMissile();
            //}
            else {
                yield return new WaitForSeconds(1f);
            }
            index++;
            index = index % poolCount;
            
        }
        
    }

}
