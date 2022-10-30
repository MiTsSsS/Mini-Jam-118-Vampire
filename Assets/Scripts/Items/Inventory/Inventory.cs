using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    public event EventHandler OnItemListChanged;

    private List<Item> items;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction) {
        items = new List<Item>();
        this.useItemAction = useItemAction;
    }

    public List<Item> getItems() {
        return items;
    }

    public void addItem(Item item) { 
        if(item.isStackable()) {
            bool isItemAlreadyInInventory = false;

            foreach(Item inventoryItem in items) {
                if(inventoryItem.itemType == item.itemType) {
                    inventoryItem.amount += item.amount;
                    isItemAlreadyInInventory = true;
                }
            }

            if(!isItemAlreadyInInventory) {
                items.Add(item);
            }
        }

        else {
            items.Add(item);
        }
        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void removeItem(Item item) {
        if (item.isStackable()) {
            Item itemInInventory = null;

            foreach (Item inventoryItem in items) {
                if (inventoryItem.itemType == item.itemType) {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                    
                }
            }

            if (itemInInventory != null && itemInInventory.amount <= 0) {
                items.Remove(itemInInventory);
            }
        } else {
            items.Remove(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void useItem(Item item) {
        useItemAction(item);
    }
}
