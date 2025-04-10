using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<InventoryObject> playerInventory;

    public delegate void PickUpItem(InventoryObject item);
    public static PickUpItem pickUpItem;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnEnable()
    {
        pickUpItem += AddItemToInventory;
    }

    private void OnDisable()
    {
        pickUpItem -= AddItemToInventory;
    }

    public void AddItemToInventory(InventoryObject itemToAdd)
    {
        foreach (var item in playerInventory)
        {
            if (item.type == itemToAdd.type)
            {
                item.quantity += itemToAdd.quantity;
                return;
            }
        }

        playerInventory.Add(itemToAdd);

        Debug.Log(playerInventory);
    }
}

[Serializable]
public class InventoryObject
{
    public GameObject worldItem;
    public int quantity = 0;
    public ItemType type;

}

public enum ItemType
{
    Log,
    Campfire
}
