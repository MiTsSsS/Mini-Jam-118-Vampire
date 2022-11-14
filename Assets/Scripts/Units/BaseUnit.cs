using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BaseUnit : MonoBehaviour
{
    [System.Serializable]
    public struct ItemDropRate {
        public int common;
        public int rare;
        public int epic;
        public int artifact;
    }

    [System.Serializable]
    public struct Stats {
        public float hp;
        public float maxHp;
        public float baseDamage;
        public float physicalDamageResist;
        public float spellDamageResist;
    }

    [SerializeField] private BaseTile occupiedTile;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;

    public Weapon weapon;
    public UnitType unitType;
    public Healthbar healthbar;
    public Stats unitStats;
    public ItemDropRate itemRarityDropRate;
    public string unitName;
    public SpriteRenderer unitRenderer;

    private Material originalMaterial;
    private Coroutine flashRoutine;

    private void Start() {
        healthbar.setMaxHpValue(unitStats.maxHp);

        originalMaterial = unitRenderer.material;
    }

    public void setUnitType(UnitType type) {
        unitType = type;
    }

    //Tile Related Functions
    public void setOccupiedTile(BaseTile tile) {
        occupiedTile = tile;
    }

    public BaseTile getOccupiedTile() {
        return occupiedTile;
    }
    //End Tile Related Functions

    //Combat
    private Item.ItemRarity getRandomRarityForDroppableItem() {
        int randomValue = Random.Range(0, 100);
        Item.ItemRarity randomizedRarity = Item.ItemRarity.Common;

        if (randomValue >= itemRarityDropRate.rare && randomValue <= itemRarityDropRate.epic && itemRarityDropRate.rare != -1) {
            randomizedRarity = Item.ItemRarity.Rare;
        }

        if(randomValue >= itemRarityDropRate.epic && randomValue <= itemRarityDropRate.artifact && itemRarityDropRate.epic != -1) {
            randomizedRarity = Item.ItemRarity.Epic;
        }

        if(randomValue >= itemRarityDropRate.artifact && randomValue <= 99 && itemRarityDropRate.artifact != -1) {
            randomizedRarity = Item.ItemRarity.Artifact;
        }

        return randomizedRarity;
    }

    public void takeDamage(float damageValue) {
        unitStats.hp -= damageValue;
        Debug.Log("Damage taken: " + damageValue);
        Debug.Log("New Hp: " + unitStats.hp);
        Flash(Color.red);

        if (unitStats.hp <= 0) {
            onUnitDie(unitType);
        }

        healthbar.setHp(unitStats.hp);
    }

    public void heal(float value) {
        if(unitStats.hp == unitStats.maxHp) {
            return;
        }

        if(unitStats.hp + value > unitStats.maxHp) {
            unitStats.hp = unitStats.maxHp;
        }

        else {
            unitStats.hp += value;
        }

        Flash(Color.green);
        healthbar.setHp(unitStats.hp);
        Debug.Log("Health: " + unitStats.hp);
    }

    public void onUnitDie(UnitType type) {
        if (type == UnitType.Enemy) {
            var itemTypeToDrop = GameManager.instance.getItemToDrop(getRandomRarityForDroppableItem());

            dropItem(itemTypeToDrop);
        }

        Destroy(gameObject);
    }

    public void dropItem(Item.ItemType itemType) {
        BaseItem.spawnItemInWorld(new Item { itemType = itemType, amount = 1 }, occupiedTile);
    }

    private IEnumerable spriteFlashEffect(Color color, float duration) {
        unitRenderer.color = color;

        yield return new WaitForSeconds(duration);

        unitRenderer.color = Color.white;
    }
    //End Combat

    public void Flash(Color color) {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null) {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine(color));
    }

    private IEnumerator FlashRoutine(Color color) {
        // Swap to the flashMaterial.
        unitRenderer.material = flashMaterial;

        // Set the desired color for the flash.
        flashMaterial.color = color;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        unitRenderer.material = originalMaterial;

        // Set the flashRoutine to null, signaling that it's finished.
        flashRoutine = null;
    }
}

public enum UnitType {
    Player = 0,
    Enemy = 1,
    NPC = 2
}