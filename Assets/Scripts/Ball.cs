using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    

    public Vector3 relativeDirection;
    WorldLimits limits;

    Rigidbody2D rb;
    float maxSpeed;

    Level level;
    GameObject soundEffectsPlayer;
    AudioSource myAudio;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        maxSpeed = 10f;
        level = GetComponent<Level>();
        soundEffectsPlayer = GameObject.Find("SoundEffectsPlayer");
        myAudio = soundEffectsPlayer.GetComponent<AudioSource>();
        limits = GameObject.Find("Background").GetComponent<WorldLimits>();
    }

    void Update()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, limits.xMin, limits.xMax),
            Mathf.Clamp(transform.position.y, limits.yMin, limits.yMax)
            );

        // Trying to Limit Speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        
    }

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Collided with: " + other.gameObject.name);
        //if (other.gameObject.CompareTag("EnemyProjectile"))
        //{
        //    rb.AddForce(other.transform.position * -100f, ForceMode2D.Force);
        //}
        //else 
        if (other.gameObject.CompareTag("Level")) {
            //Debug.Log("CollidedWithLevel");
            if (myAudio.isPlaying)
            {
                myAudio.Stop();
            }
            myAudio.Play();
        }
    }

    public void UpdateRelDirection(Vector3 forceDirection) {
        relativeDirection = forceDirection;
    }

}
