using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour {

    Vector3 gravityCenter;
    GameObject gravityField;
    float pullRadius;
    
    public List<GameObject> target;

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

    public Toggle toggle;
    public bool autoPlay;

    public Transform[] targets;
    public Health health;

    WorldLimits limits;
    Inventory inv;

    void Awake() {
        gravityCenter = transform.position;
        target = new List<GameObject>();
        //gravityField = GameObject.Find("GravityField");
        pullRadius = 2;
        pullForce = 5f;
        rb = GetComponentInParent<Rigidbody2D>();
        followSpeed = .01f;
        beingDamaged = false;
        hasStarted = false;
        playerProjectiles = GameObject.Find("PlayerProjectiles");
        ballPrefab = Resources.Load("Prefabs/Ball") as GameObject;
        limits = GameObject.Find("Background").GetComponent<WorldLimits>();
        clampedPosition = new Vector2();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        targets = GameObject.Find("Boxes").GetComponentsInChildren<Transform>();
        health = GameObject.Find("Health").GetComponent<Health>();
        toggle = GameObject.Find("Autoplay").GetComponent<Toggle>();
        MakeBall();
    }

    void OnTriggerStay2D(Collider2D other) {//calculate ball direction to mothership
        if (other.gameObject.CompareTag("Projectile")) {            
            if (!target.Contains(other.gameObject))
            {
                target.Add(other.gameObject);
            }

        }
    }

    void Update() {
        autoPlay = toggle.isOn;
        health.invincible = autoPlay;
        if (Input.GetMouseButton(0))
        {//left click
            pullForce = 500f;

            if (!hasStarted)
            {//triggers our first ball
                hasStarted = true;
                MakeBall();
                for (int i = 0; i < target.Count; i++)
                {
                    pullForce = -1000f;
                }
            }

            if (Input.GetMouseButtonDown(1))
            {//Use picked up powerup
                inv.RemoveItem(0);
            }

        }
        else if (Input.GetMouseButton(1))
        {
            //rightclick
            pullForce = -5f;
            if (!hasStarted)
            {//triggers our first ball
                hasStarted = true;
                MakeBall();
                for (int i = 0; i < target.Count; i++)
                {
                    pullForce = -1000f;
                }
            }
        }
        
        else {
            pullForce = 500f;
        }
    }

    void FixedUpdate() {
        if (!autoPlay)
        {
            MoveWithMouse();
        }
        else {
            AutoPlay();
        }


        if (!beingDamaged) {
            rb.MovePosition(target2d);
        }
        Attract();
    }

    void MoveWithMouse() {
        mouse2d = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouseTarget = Camera.main.ScreenToWorldPoint(mouse2d);

        clampedPosition.y = Mathf.Clamp(mouseTarget.y, 
            limits.yMin, limits.yMax);
        clampedPosition.x = Mathf.Clamp(mouseTarget.x, 
            limits.xMin, limits.xMax);

        mouseTarget = clampedPosition;

        target2d = new Vector2(
            Mathf.Lerp(rb.position.x, mouseTarget.x, Time.time * followSpeed),
            Mathf.Lerp(rb.position.y, mouseTarget.y, Time.time * followSpeed));
    }

    void AutoPlay() {//Get first child of level boxes
        for (int i = 0; i < targets.Length; i++) {
            if (targets[i].gameObject.activeSelf) {
                target2d = new Vector2(
                                Mathf.Lerp(rb.position.x, targets[i].position.x, Time.time * followSpeed),
                                Mathf.Lerp(rb.position.y, targets[i].position.y, Time.time * followSpeed));
            }
        }

    }

    void Attract() {
        if (target.Count > 0)
        {
            Ball ball;
            Rigidbody2D ballRb;
            for (int i = 0; i < target.Count; i++)
            {
                //not liking these GetComponents, will have to create an OnTriggerEnter2D and keep track of IDs instead
                ball = target[i].gameObject.GetComponent<Ball>();
                ball.UpdateRelDirection(transform.position - ball.gameObject.transform.position);

                ballRb = target[i].GetComponent<Rigidbody2D>();
                ballRb.AddForce(ball.relativeDirection * pullForce * Time.deltaTime);
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

    public void MakeBall(Transform other) {
        GameObject ball = Instantiate(ballPrefab, other.position, other.rotation);
        ball.transform.SetParent(playerProjectiles.transform);
    }

    public void UseItem() {
        
    }
}
