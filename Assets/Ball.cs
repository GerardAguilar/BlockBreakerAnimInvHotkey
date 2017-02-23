using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 relativeDirection;
    Rigidbody2D rb;
    float maxSpeed;
   

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        maxSpeed = 10f;
    }

    void Update()
    {
        // Trying to Limit Speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    public void UpdateRelDirection(Vector3 forceDirection) {
        relativeDirection = forceDirection;
    }
}
