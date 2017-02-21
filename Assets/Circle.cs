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

    Rigidbody2D rb;
    Vector2 mouse2d;
    Vector2 mouseTarget;
    //public Collider2D[] colArray;

    void Awake() {
        gravityCenter = transform.position;
        target = new List<GameObject>();
        //gravityField = GameObject.Find("GravityField");
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D other) {//calculate ball direction to mothership
        if (other.gameObject.CompareTag("Projectile")) {
            forceDirection = transform.position - other.transform.position;
            target.Add(other.gameObject);
        }
    }

    void FixedUpdate() {

        //transform.position = Vector3.Lerp(transform.position, Input.mousePosition, .9f);
        mouse2d = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouseTarget = Camera.main.ScreenToWorldPoint(mouse2d);
        rb.MovePosition(mouseTarget);

        if (target.Count > 0) {
            for(int i=0; i<target.Count; i++)
            {
                target[i].GetComponent<Rigidbody2D>().AddForce(forceDirection * pullForce * Time.deltaTime);
            }
        }
    }
        
}
