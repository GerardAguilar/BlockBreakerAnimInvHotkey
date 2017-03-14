using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    

    [SerializeField]
    GameObject inventoryPanel;
    [SerializeField]
    GameObject slotPanel;

    private int slotAmount;
    public ItemDatabase database;

    //[SerializeField]
    public GameObject inventorySlot;
    //[SerializeField]
    public GameObject inventoryItem;

    
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private bool databaseIsReady;

    void Awake() {
        databaseIsReady = false;
        inventorySlot = Resources.Load("Prefabs/Prefab/Slot") as GameObject;
        inventoryItem = Resources.Load("Prefabs/Prefab/Item") as GameObject;
        database = GetComponent<ItemDatabase>();//since the Inventory script needs to wait for the database, why not have the database be awakened earlier?
    }

    void Start() {
        slotAmount = 3;
        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;



        for (int i = 0; i < slotAmount; i++) {
            //add a blank inventory slot into the slots collective
            items.Add(new Item());//back-end, initialize items
            slots.Add(Instantiate(inventorySlot));//back-end, setup slots
            slots[i].GetComponent<ItemSlot>().slotID = i;
            slots[i].transform.SetParent(slotPanel.transform);//front-end, show slots
        }

        //StartCoroutine(database.LoadJson());

        //AddItem(0);//possible here that the database just woke up and hasn't finished loading the JSON file yet

        //StartCoroutine doesn't stall the thread
        //yield return does
        //Inventory.cs needs to wait for the database to load in order to add an item
        //Adding an item needs to stall the thread until the database is loaded
        //But the database and the inventory are separate
        //The database is scheduled to wake up first, but the inventory is the parent of the itemDatabase

        //Inventory.cs
        //On Awake(), also wake up the itemDatabase
        //Call AddItem(0) in-game, rather than on awake.

    }

    

    /*
    Add item into items list and slots list.
        */
    public void AddItem(int id) {
        Item itemToAdd = null;

        //StartCoroutine(WaitForDatabaseLoad());
        ////we can try adding a while loop here that consistently asks ItemDatabase if it's done loading stuff yet
        //while (!database.doneLoading) {//locks up

        //}

        //maybe call an IEnum before AddItem() to make sure that the database has been formed.

        //Or maybe turn AddItem into an IEnumerator?
        //But what would the yield return new be?
        //yield return database.AmIDoneLoading()?
        //Debug.Log("AddItem: Fetching item: " + id);
        if (database.FetchItemByID(id) != null)
        {
            Debug.Log("Database fetch is not null!");
            itemToAdd = database.FetchItemByID(id);//Grab the item from our item library
            //Debug.Log("Inventory.cs: itemToAdd: " + itemToAdd.ToString());
        }
        else {
            Debug.Log("database is null");
        }

        //Debug.Log("item: " + itemToAdd.Title + " -> " + itemToAdd.Sprite.ToString());

        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))//update ItemData instead of adding a new item
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();//the itemData can change if the item is dragged to other values, in other words, the ItemData data.slot needs to be refreshed
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount + 1 +"";
                }
            }
        }
        else {//add a new item
            for (int i = 0; i < items.Count; i++)
            {
                //if empty slot (-1 id), then add the itemTo Add to that slot
                if (items[i].ID == -1)
                {
                    //Debug.Log("items[i].ID != -1");
                    items[i] = itemToAdd;//add the item into the items list
                    GameObject itemObj = Instantiate(inventoryItem);//just the item template, no icon yet
                    itemObj.GetComponent<ItemData>().item = itemToAdd;//Update the item in the itemData
                    //itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().slot = i;//update slot location of item
                    
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;//the itemToAdd from the library should have everything in it. 
                    //Debug.Log("Setting itemObj sprite: " + itemObj.GetComponent<SpriteRenderer>().sprite.ToString());
                    //Problem above is that the Image component should maybe instead be a SpriteRenderer one?
                    itemObj.transform.position = slots[i].transform.position;
                    itemObj.transform.SetParent(slots[i].transform);//attach item to corresponding slot
                    
                    //Debug.Log(itemObj.transform.position + ": " + itemObj.GetComponentInParent<Transform>().position);
                    itemObj.name = itemToAdd.Title;

                    break;
                }
            }
        }
    }

    public void RemoveItem(int slotToRemove) {
        slotToRemove = 0;//dummy value
        //Debug.Log("Remove Item on: " + slotToRemove);
        //Destroy(slots[slotToRemove].GetComponentInChildren<ItemData>().gameObject);
        //Need to reverse AddNewItem without stacking, and AddNewItem with stacking.
    }

    bool CheckIfItemIsInInventory(Item item) {//here, there's a bug that if an item is dragged onto another item to switch places, this check doesn't find the item
        bool isInInventory = false;
        for (int i = 0; i < items.Count; i++) {
            //Debug.Log(items[i].ID + " ? " + item.ID);

            if (items[i].ID == item.ID)
            {//here, the items[i].ID and item.ID can be matched more than once. Looks like items[i].ID is NOT getting updated during the switch
                //Debug.Log("[" + i + "]" + ": " + items[i].ID + ": " + item.ID + " <- Found Match");
                isInInventory = true;
            }
            else {
                //Debug.Log("[" + i + "]" + ": " + items[i].ID + ": " + item.ID);
            }
        }
        return isInInventory;
    }

    //IEnumerator WaitForDatabaseLoad() {

    //    yield return database.AmIDoneLoading();
    //}

    //public void HasDatabaseLoadedCompletely(bool loadSuccess) {
    //    Debug.Log("HasDatabaseLoadedCompletely: " + loadSuccess);
    //    databaseIsReady = loadSuccess;
    //}
}
