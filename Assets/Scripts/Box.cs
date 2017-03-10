using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Box : MonoBehaviour {
    [SerializeField]
    public static int breakCount = 0;
    public GameObject ballPrefab;
    public int boxHealth;
    public bool isInvulnerable;

    Level level;

    GameObject playerProjectiles;
    GameObject gravityField;
    int hitCount;

    public Sprite newSprite;

    public Sprite[] hitSprites;
    

    void Awake() {
        boxHealth = 2;
        level = GameObject.Find("Level").GetComponent<Level>();
        if (this.tag == "Level") {
            level.IncreaseBoxCount();
        }
        playerProjectiles = GameObject.Find("PlayerProjectiles");
        gravityField = GameObject.Find("GravityField");
        newSprite = Resources.Load<Sprite>("Sprites/BoxBroken");//Here the Load<Sprite> is important
        hitCount = 0;
    }

    void Update() {
        if (boxHealth <= 0) {
            
            gameObject.SetActive(false);
            level.DecreaseBoxCount();

        } else if (boxHealth < hitSprites.Length) {
            //GetComponent<SpriteRenderer>().sprite = newSprite;
            LoadSprites();
        }

        if (breakCount % 20 == 10) {
            gravityField.GetComponent<Circle>().MakeBall();
            //GameObject ball = Instantiate(ballPrefab, mothership.transform.position, transform.rotation);
            //ball.transform.SetParent(playerProjectiles.transform);
            breakCount = 0;//required because the update function calls MakeBall() a lot of times before the OnCollisionEnter2D() increments the break count
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log("Box: Checking for collision");
        bool isBreakable = (this.tag == "Level");
        if (isBreakable)
        {
            if (other.gameObject.CompareTag("Projectile"))
            {
                if (!isInvulnerable)
                {
                    hitCount++;
                    boxHealth--;
                    breakCount++;
                    
                }
            }
        }
        
    }

    void LoadSprites() {
        int spriteIndex = hitCount;
        if (hitSprites[spriteIndex])
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
    }
}
