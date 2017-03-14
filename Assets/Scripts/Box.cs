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

    public AudioClip crack;
    

    void Awake() {
        boxHealth = 2;
        level = GameObject.Find("Level").GetComponent<Level>();
        if (this.tag == "Breakable") {
            level.IncreaseBoxCount();
        }
        playerProjectiles = GameObject.Find("PlayerProjectiles");
        gravityField = GameObject.Find("GravityField");
        newSprite = Resources.Load<Sprite>("Sprites/BoxBroken");//Here the Load<Sprite> is important
        crack = Resources.Load<AudioClip>("Sounds/crack");
        hitCount = 0;
    }

    void Update() {
        if (boxHealth <= 0) {
            level.DecreaseBoxCount();
            breakCount++;
            //Debug.Log();
            gameObject.SetActive(false);            

        } else if (boxHealth < hitSprites.Length) {
            //GetComponent<SpriteRenderer>().sprite = newSprite;
            LoadSprites();
        }

        if (breakCount % 10 == 5) {//if %5 = 0, then initial set up freezes game
            breakCount = 0;//required because the update function calls MakeBall() a lot of times before the OnCollisionEnter2D() increments the break count
            gravityField.GetComponent<Circle>().MakeBall();
            //GameObject ball = Instantiate(ballPrefab, mothership.transform.position, transform.rotation);
            //ball.transform.SetParent(playerProjectiles.transform);
            
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log("Box: Checking for collision");
        bool isBreakable = (this.tag == "Breakable");
        if (isBreakable)
        {
            if (other.gameObject.CompareTag("Projectile"))
            {
                if (!isInvulnerable)
                {
                    hitCount++;
                    boxHealth--;
                    //AudioSource.PlayClipAtPoint(crack, transform.position);//instantiates the clip, audio can still play when the gameobject is done
                    
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
