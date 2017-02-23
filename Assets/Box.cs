using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Box : MonoBehaviour {

    public int boxHealth;

    void Awake() {
        boxHealth = 2;
    }

    void Update() {
        if (boxHealth <= 0) {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log("Box: Checking for collision");
        if (other.gameObject.CompareTag("Projectile")) {
            boxHealth--;
        }
    }
}
