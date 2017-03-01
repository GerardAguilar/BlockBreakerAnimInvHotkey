﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float health;
    public float maxHealth;
    public float proportion;
    public float lerpSpeed;
    public Image healthUI;
    public Image healthHUD;

    Rigidbody2D rb;
    Circle mothership;

    bool initialized = false;

    void Start() {
        if (!initialized)
        {
            health = 20f;
            initialized = true;
        }
        maxHealth = 20f;
            
        healthUI = GetComponent<Image>();
        lerpSpeed = 10f;
        rb = GetComponentInParent<Rigidbody2D>();
        mothership = GetComponentInParent<Circle>();
        healthHUD = GameObject.Find("RadialContent").GetComponent<Image>();
    }

    void Update() {
        if (health <= 0) {
            SceneManager.LoadScene("You Lose Screen");
        }
        proportion = health / maxHealth;
        healthUI.fillAmount = Mathf.Lerp(healthUI.fillAmount, proportion, Time.deltaTime * lerpSpeed);
        healthHUD.fillAmount = Mathf.Lerp(healthUI.fillAmount, proportion, Time.deltaTime * lerpSpeed);
    }

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Collided with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("EnemyProjectile")) {
            TakeDamage();
            other.gameObject.GetComponent<Missile>().ResetMissile();
            //mothership.IsBeingDamaged(true);
            rb.AddForce(other.transform.position * -10f, ForceMode2D.Force);
        }
        
    }

    public void TakeDamage() {
        health--;
    }
}