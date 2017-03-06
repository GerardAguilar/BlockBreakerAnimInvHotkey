using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
    static PlayerScript pInstance = null;


    [SerializeField]
    private StatScript health;

    [SerializeField]
    private StatScript energy;

    [SerializeField]
    private StatScript shield;

    private GameObject inventoryPanel;

    public BarScript healthBar;
    public BarScript energyBar;
    public BarScript shieldBar;

	// Use this for initialization
	void Start () {
        //inventoryPanel = GameObject.Find("InventoryPanel");
	}

    void Awake() {
        if (pInstance != null)
        {
            Destroy(gameObject);
            //print("Duplicate music player is self-destructing");
        }
        else {
            pInstance = this;
            healthBar = GameObject.Find("RadialBar").GetComponent<BarScript>();
            energyBar = GameObject.Find("EnergyBar").GetComponent<BarScript>();
            shieldBar = GameObject.Find("ShieldBar").GetComponent<BarScript>();

            DontDestroyOnLoad(gameObject);
        }

        health.Initialize(healthBar);
        energy.Initialize(energyBar);
        shield.Initialize(shieldBar);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            health.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            health.CurrentVal += 10;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            energy.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            energy.CurrentVal += 10;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            shield.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            shield.CurrentVal += 10;
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }

        if (health.CurrentVal <= 0) {
            SceneManager.LoadScene("You Lose Screen");
        }
    }

    public void PlayerTakeDamage() {
        health.CurrentVal -= 10;
    }

    public float GetCurrentHealth() {
        return health.CurrentVal;
    }
}
