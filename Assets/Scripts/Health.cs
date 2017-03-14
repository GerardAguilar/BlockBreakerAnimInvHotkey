using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    //public float health;//handled in the PlayerScript
    public float maxHealth;
    public float proportion;
    public float lerpSpeed;
    public Image healthUI;
    public Image healthHUD;
    public bool invincible;
    

    Rigidbody2D rb;
    Circle mothership;
    PlayerScript playerScript;

    bool initialized = false;

    void Awake() {
        invincible = false;
        if (!initialized)
        {
            //health = 20f;
            initialized = true;
        }
        maxHealth = 100f;

        healthUI = GetComponent<Image>();
        lerpSpeed = 10f;
        rb = GetComponentInParent<Rigidbody2D>();
        mothership = GetComponentInParent<Circle>();
        healthHUD = GameObject.Find("RadialContent").GetComponent<Image>();
        playerScript = GetComponentInParent<PlayerScript>();
    }

    void Update() {        
        //TO-DO: This can probably be moved to the UI BarScript/Health Script
        healthUI.fillAmount = healthHUD.fillAmount;        
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
        //health--;
        if (!invincible)
        {
            playerScript.PlayerTakeDamage();//need to link this to the healthHUD
            proportion = playerScript.GetCurrentHealth() / maxHealth;
            healthHUD.fillAmount = Mathf.Lerp(healthUI.fillAmount, proportion, Time.deltaTime * lerpSpeed);
        }
    }
}
