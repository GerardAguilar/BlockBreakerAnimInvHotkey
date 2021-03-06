﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;//access files

//Allows us to take JSON data and turn it into a C# object, and vice versa
public class ItemDatabase : MonoBehaviour {
    [SerializeField]
    public List<Item> database = new List<Item>();
    [SerializeField]
    private JsonData itemData;

    public string filePath;
    public WWW www;

    public string myString;
    public bool doneLoading = false;

    Inventory inv;

    void Awake()
    {
        //extract all items from the json file

        //StartCoroutine(LoadJson());
        inv = GetComponentInParent<Inventory>();

        myString = "nada";
        Debug.Log("Before LoadJson: " + myString);
        StartCoroutine(LoadJson());

        //Debug.Log(database[1].Slug);
        //Debug.Log(FetchItemByID(0).Description);
    }

    public IEnumerator LoadJson() {//this is used to resolve loading the streaming json from an internet hierarchy (which introduces extra slashes)
        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "ItemsAgain.json");
        //Debug.Log("[GAA1]" + Application.dataPath + "/StreamingAssets/ItemsAgain.json");
        //Debug.Log("[GAA2]" + filePath);
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            www = new WWW(filePath);
            //itemData = JsonMapper.ToObject(File.ReadAllText(www.text));
            yield return www;//this should be waiting for www to finish downloading
            doneLoading = true;
            //Debug.Log("www: " + www.text);

            itemData = JsonMapper.ToObject(www.text);//www is not completing....., or is it JsonMapper that's now finishing?

            yield return itemData;//this fixed the WWW is still loading error.

            //inv.HasDatabaseLoadedCompletely(true);

            //Debug.Log("After JsonMapper: " + itemData.ToString());

            //why not have these calls inside the IEnumerator, waiting for the loading to finish?
            // maybe it's the JsonMapper or the ConstructItemDataase that causes the issues.

            //Wait, they already are?

            //How about having the ItemDatabase.cs send a message to Inventory.cs when it's done?

            //Debug.Log("In LoadJson: " + myString);//doesn't get to here
            ConstructItemDatabase();
        }
        else {
            doneLoading = true;
            itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ItemsAgain.json"));
            //Debug.Log("In LoadJson: " + myString);//doesn't get to here
            yield return itemData;

            //inv.HasDatabaseLoadedCompletely(true);
            //Debug.Log("After JsonMapper: " + itemData.ToString());

            ConstructItemDatabase();
        }
        //Debug.Log("In LoadJson: "+ myString);//doesn't get to here
        //ConstructItemDatabase();
    }



    public Item FetchItemByID(int id) {
        for (int i = 0; i < database.Count; i++) {
            if (database[i].ID == id) {
                return database[i];//is an item
            }
        }
        return null;
    }

    //grab each item from itemData
    void ConstructItemDatabase() {
        //Debug.Log("ConstructItemDatabase");
        for (int i = 0; i < itemData.Count; i++) {
            //take the list, and loop through each item, adding them to the database
            database.Add(
                new Item(
                    (int)itemData[i]["id"],
                    itemData[i]["title"].ToString(),
                    (int)itemData[i]["value"],
                    (int)itemData[i]["stats"]["power"],
                    (int)itemData[i]["stats"]["defence"],
                    (int)itemData[i]["stats"]["vitality"],
                    itemData[i]["description"].ToString(),
                    (bool)itemData[i]["stackable"],
                    (int)itemData[i]["rarity"],
                    itemData[i]["slug"].ToString()
                ));
        }
    }

    public bool AmIDoneLoading()
    {
        //Debug.Log("AmIDoneLoading: " + doneLoading);
        return doneLoading;
    }
}

public class Item {//properties start with capitals, and the attribute it works with is the lowercase one. Methods can be attached to each property.
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Power { get; set; }
    public int Defence { get; set; }
    public int Vitality { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite[] SpriteSheet { get; set; }//required to keep our items in a sprite sheet
    public Sprite Sprite { get; set; }

    //attributes of each property
    public Item(int id, string title, int value, int power, int defence, int vitality, string description, bool stackable, int rarity, string slug) {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Power = power;
        this.Defence = defence;
        this.Vitality = vitality;
        this.Description = description;
        this.Stackable = stackable;
        this.Rarity = rarity;
        this.Slug = slug;//The name of the asset
        //this.Sprite = Resources.Load<Sprite>("Sprites/"+slug);
        /*
        Since we're using a SpriteSheet, we'll need to unpack it into an array first, and then find the actual sprite we want to use.
        The search for the proper sprite should be a Database function instead, so as to keep the Item Class compact.
        But, there should be a function to assign the sprite in the Item class.
        */
        this.SpriteSheet = Resources.LoadAll<Sprite>("Sprites/uipack_rpg_sheet");//will have to change this later to access different sprite sheets.
        for (int i = 0; i < this.SpriteSheet.Length; i++) {
            string temp = this.SpriteSheet[i].name;
            if (temp.Equals(slug)) {
                this.Sprite = this.SpriteSheet[i];
                break;
            }
        }
    }

    public Item(int id, string title, int value)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        //this.Power = power;
        //this.Defence = defence;
        //this.Vitality = vitality;
        //this.Description = description;
        //this.Stackable = stackable;
        //this.Rarity = rarity;
        //this.Slug = slug;
    }

    //Default
    public Item() {
        this.ID = -1;
    }
}


