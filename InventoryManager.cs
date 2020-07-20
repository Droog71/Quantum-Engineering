﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventory;
    StateManager game;
    public string ID = "unassigned";
    public int address;
    string originalID;
    public bool initialized;
    private float updateTick;
    public int maxStackSize = 1000;
    public bool itemAdded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ID != "unassigned" && initialized == false)
        {
            game = GameObject.Find("GameManager").GetComponent<StateManager>();
            if (game.worldLoaded == true)
            {
                inventory = new InventorySlot[16];
                int count = 0;
                while (count <= 15)
                {
                    inventory[count] = gameObject.AddComponent<InventorySlot>();
                    string countType = PlayerPrefs.GetString(game.WorldName + "inventory" + ID + "slot" + count + "type");
                    if (!countType.Equals(""))
                    {
                        inventory[count].typeInSlot = PlayerPrefs.GetString(game.WorldName + "inventory" + ID + "slot" + count + "type");
                        inventory[count].amountInSlot = PlayerPrefs.GetInt(game.WorldName + "inventory" + ID + "slot" + count + "amount");
                    }
                    count++;
                }
                originalID = ID;
                initialized = true;
                if (ID.Equals("Rocket"))
                {
                    maxStackSize = 100000;
                }
                else
                {
                    maxStackSize = 1000;
                }
                //Debug.Log("Loaded inventory for : " + ID);
            }
        }

        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            //Debug.Log(ID + " Machine update tick: " + address * 0.1f);
            if (GetComponent<RailCart>() == null && GetComponent<Retriever>() == null && GetComponent<AutoCrafter>() == null && GetComponent<PlayerController>() == null && GetComponent<Rocket>() == null && ID != "Lander")
            {
                GetComponent<PhysicsHandler>().UpdatePhysics();
            }
            if (ID == "player" || ID == "Lander")
            {
                SaveData();
            }
            updateTick = 0;
        }
    }

    public void SaveData()
    {
        if (initialized == true)
        {
            if (ID != originalID)
            {
                int originalCount = 0;
                while (originalCount <= 15)
                {
                    PlayerPrefs.SetString(game.WorldName + "inventory" + originalID + "slot" + originalCount + "type", "nothing");
                    PlayerPrefs.SetInt(game.WorldName + "inventory" + originalID + "slot" + originalCount + "amount", 0);
                    originalCount++;
                }
                originalID = ID;
            }
            int count = 0;
            while (count <= 15)
            {
                PlayerPrefs.SetString(game.WorldName + "inventory" + ID + "slot" + count + "type", inventory[count].typeInSlot);
                PlayerPrefs.SetInt(game.WorldName + "inventory" + ID + "slot" + count + "amount", inventory[count].amountInSlot);
                count++;
            }
        }
    }

    public void AddItem(string type, int amount)
    {
        itemAdded = false;
        foreach (InventorySlot slot in inventory)
        {
            if (slot != null && itemAdded == false)
            {
                if (slot.typeInSlot == "nothing" || slot.typeInSlot == type || slot.typeInSlot == "")
                {
                    if (slot.amountInSlot <= maxStackSize - amount)
                    {
                        slot.typeInSlot = type;
                        slot.amountInSlot += amount;
                        itemAdded = true;
                    }
                }
            }
        }
    }

    public void AddItemToSlot(string type, int amount, int slot)
    {
        inventory[slot].typeInSlot = type;
        inventory[slot].amountInSlot += amount;
    }
}