using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {
    public int slotID;
    private Inventory inv;

    void Start() {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData){//knows which gameobject is being dragged
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();//pointerDrag is the gameobject being dragged
        //Debug.Log(inv.items[slotID].ID);
        if (inv.items[slotID].ID == -1){//if this slot is empty
            inv.items[droppedItem.slot] = new Item();//resets slot in the inventory array
            inv.items[slotID] = droppedItem.item;//insert droppedItem into inventory array
            droppedItem.slot = slotID;//updates the id, the actual movement is in ItemData.cs
        }
        else if(droppedItem.slot != slotID) {//if this slot has something, and it's not the same place it came from

            /*
            Somewhere in here, we'll have to reassign the data component of the item to update it's slot designation, but that's already been done before

            ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();//the itemData can change if the item is dragged to other values, in other words, the ItemData data.slot needs to be refreshed
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount + 1 +"";
            */


            Transform item = this.transform.GetChild(0);//grab the other item this slot has

            int originalSlot = droppedItem.slot;
            int originalID = droppedItem.GetComponent<ItemData>().item.ID;
            int replaceSlot = slotID;
            int replaceID = item.GetComponent<ItemData>().item.ID;

            //Send displaced item to the originalSlot
            item.GetComponent<ItemData>().slot = droppedItem.slot;//change that other item's slot to the original slot of the current item being held
            item.transform.SetParent(inv.slots[droppedItem.slot].transform);//set that other item's parent as the original slot of the current item being held
            item.transform.position = inv.slots[droppedItem.slot].transform.position;//change that other item's position to the original slot of the current item being held
            inv.items[originalSlot] = item.GetComponent<ItemData>().item;//update the inventory with other item's new index
            inv.items[originalSlot].ID = replaceID;

            //Set dropped item attributes to current slotID
            droppedItem.slot = replaceSlot;//assign the current item's slot to this one, wait, why are we changing slotIDs? Ah, this is the ItemSlot, we're actually rewriting the slot designation of the dropped item
            droppedItem.transform.SetParent(this.transform);//set the current item's parent to this one
            droppedItem.transform.position = this.transform.position;//change the current item's position to this one
            inv.items[replaceSlot] = droppedItem.GetComponent<ItemData>().item;//update the inventory with other item's new index
            inv.items[replaceSlot].ID = originalID;

        }
    }

}
