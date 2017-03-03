using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    Vector3 gravityCenter;
    GameObject gravityField;
    float pullRadius;
    
    List<GameObject> target;

    Rigidbody2D rb;
    public Vector2 mouse2d;
    Vector2 target2d;
    public Vector2 mouseTarget;
    float followSpeed;

    Vector2 clampedPosition;

    public float pullForce;
    public bool beingDamaged;
    public bool hasStarted;
    public GameObject playerProjectiles;
    public GameObject ballPrefab;

    WorldLimits limits;

    void Awake() {
        gravityCenter = transform.position;
        target = new List<GameObject>();
        //gravityField = GameObject.Find("GravityField");
        pullRadius = 2;
        pullForce = .5f;
        rb = GetComponentInParent<Rigidbody2D>();
        followSpeed = .01f;
        beingDamaged = false;
        hasStarted = false;
        playerProjectiles = GameObject.Find("PlayerProjectiles");
        ballPrefab = Resources.Load("Prefabs/Ball") as GameObject;
        limits = GameObject.Find("Background").GetComponent<WorldLimits>();
        clampedPosition = new Vector2();
    }

    void OnTriggerStay2D(Collider2D other) {//calculate ball direction to mothership
        if (other.gameObject.CompareTag("Projectile")) {
            //not liking these GetComponents, will have to create an OnTriggerEnter2D and keep track of IDs instead
            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.UpdateRelDirection(transform.position - other.transform.position);
            target.Add(other.gameObject);
        }
    }

    void Update() {
        if (Input.GetMouseButton(0))
        {//left click
            pullForce = 5f;
            if (!hasStarted) {//triggers our first ball
                hasStarted = true;
                MakeBall();
                for (int i = 0; i < target.Count; i++) {
                    pullForce = -10f;
                }
            }
            
        }
        else if (Input.GetMouseButton(1)) {
            //rightclick
            pullForce = -5f;
            if (!hasStarted) {//triggers our first ball
                hasStarted = true;
                MakeBall();
                for (int i = 0; i < target.Count; i++)
                {
                    pullForce = -10f;
                }
            }
        }
        else {
            pullForce = .5f;
        }
    }

    void FixedUpdate() {
        mouse2d = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouseTarget = Camera.main.ScreenToWorldPoint(mouse2d);

        clampedPosition.y = Mathf.Clamp(mouseTarget.y, limits.yMin, limits.yMax);
        clampedPosition.x = Mathf.Clamp(mouseTarget.x, limits.xMin, limits.xMax);

        mouseTarget = clampedPosition;

        target2d = new Vector2(
            Mathf.Lerp(rb.position.x, mouseTarget.x, Time.time * followSpeed),
            Mathf.Lerp(rb.position.y, mouseTarget.y, Time.time * followSpeed));

        if (!beingDamaged) {
            rb.MovePosition(target2d);
        }
        Attract();
    }

    void Attract() {
        if (target.Count > 0)
        {
            for (int i = 0; i < target.Count; i++)
            {
                target[i].GetComponent<Rigidbody2D>().AddForce(target[i].GetComponent<Ball>().relativeDirection * pullForce * Time.deltaTime);
            }
        }
    }

    public void IsBeingDamaged(bool myBool) {
        beingDamaged = myBool;
    }

    public void MakeBall() {
        GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
        ball.transform.SetParent(playerProjectiles.transform);
        //breakCount = 0;
    }
        
}
