using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    private Item item;
    private string data;
    private GameObject tooltip;

    void Awake() {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    void Update() {
        if (tooltip.activeSelf) {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Initialize(Item item) {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);

    }
    public void Deactivate() {
        tooltip.SetActive(false);
    }

    public void ConstructDataString() {
        //data = item.Title;
        data = "test";
        //data = "<color=#0473f0><b>" + item.Title + "</b></color>\n" + item.Description + "\nPower: " + item.Power;
        tooltip.transform.GetComponentInChildren<Text>().text = data;
    }
}
