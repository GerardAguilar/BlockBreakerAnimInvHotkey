using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    Vector3 target;
    Vector2 center;
    float radius;
    public float nextFire;
    public float fireInterval;
    RadarSystem radarSystem;
    MissilePool missilePool;

    void Awake () {
        nextFire = 0f;
        fireInterval = .1f;
        radarSystem = GetComponentInChildren<RadarSystem>();
        missilePool = GetComponentInChildren<MissilePool>();
    }

    void Update() {
        PrepareToFire();
    }

    //void OnTriggerEnter2D(Collider2D other) {
    //    if (other.gameObject.CompareTag("Projectile")) {
    //        if (!colliderList.Contains(other)) {
    //            colliderList.Add(other);
    //        }
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other) {
    //    if (other.gameObject.CompareTag("Projectile")) {
    //        if (colliderList.Contains(other)) {
    //            colliderList.Remove(other);
    //        }
    //    }
    //}

    void PrepareToFire()
    {            
        if (radarSystem.HowManyTargetted() > 0 && Time.time > nextFire)
        {
            nextFire = Time.time + fireInterval;
            //Debug.Log("PrepareToFire(): Fire");
            StartCoroutine(missilePool.FireShot());
            //missilePool.FireShot();

        }



    }
}
