using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
    public CinemachineVirtualCamera playerCamera;

    public List<Item.ItemType> droppableCommonItems, droppableRareItems,
                                 droppableEpicItems, droppableArtifactItems; 
    
    private void Awake() {
        instance = this;

        droppableCommonItems = new List<Item.ItemType>();
        droppableRareItems = new List<Item.ItemType>();
        droppableEpicItems = new List<Item.ItemType>();
        droppableArtifactItems = new List<Item.ItemType>();

        droppableCommonItems.Add(Item.ItemType.ShortSword);
        droppableCommonItems.Add(Item.ItemType.ShortBow);

        droppableRareItems.Add(Item.ItemType.Crossbow);
        droppableRareItems.Add(Item.ItemType.Blood);
    }

    public Item.ItemType getItemToDrop(Item.ItemRarity rarity) {
        List<Item.ItemType> requestedItemsOfType = new List<Item.ItemType>();

        switch (rarity) {
            case Item.ItemRarity.Common:
                requestedItemsOfType = droppableCommonItems;
                break;

            case Item.ItemRarity.Rare:
                requestedItemsOfType = droppableRareItems;
                break;

            case Item.ItemRarity.Epic:
                requestedItemsOfType = droppableEpicItems;
                break;

            case Item.ItemRarity.Artifact:
                requestedItemsOfType = droppableArtifactItems;
                break;
        }

        int randomIndex = Random.Range(0, requestedItemsOfType.Count);

        Debug.Log("RANDOMIZED INDEX: " + randomIndex);

        return requestedItemsOfType.ElementAt(randomIndex);
    }

    public void setupCamera(PlayerUnit unitToFollow) {
        playerCamera.Follow = unitToFollow.transform;
    }
}
