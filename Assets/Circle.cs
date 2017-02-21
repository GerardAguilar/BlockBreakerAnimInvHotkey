using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    public Vector3 gravityCenter;
    public GameObject gravityField;
    public float pullRadius = 2;
    public float pullForce = 50;
    public Vector3 forceDirection;
    public List<GameObject> target;
    //public Collider2D[] colArray;

    void Awake() {
        gravityCenter = transform.position;
        target = new List<GameObject>();
        //gravityField = GameObject.Find("GravityField");
    }

    void OnTriggerStay2D(Collider2D other) {//calculate ball direction to mothership
        if (other.gameObject.CompareTag("Projectile")) {
            forceDirection = transform.position - other.transform.position;
            target.Add(other.gameObject);
        }
    }

    void FixedUpdate() {
        if (target.Count > 0) {
            for(int i=0; i<target.Count; i++)
            {
                target[i].GetComponent<Rigidbody2D>().AddForce(forceDirection * pullForce * Time.deltaTime);
            }
        }
    }
        
}
