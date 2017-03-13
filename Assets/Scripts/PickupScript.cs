using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {
    public int itemID;
    public Inventory inventory;

    void Awake() {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            inventory.AddItem(itemID);
            //gameObject.SetActive(false);
        }
    }
}
