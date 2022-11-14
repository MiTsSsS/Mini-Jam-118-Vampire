using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BaseItem : MonoBehaviour
{
    private Item item;  
    private TextMeshPro textMeshPro;

    public SpriteRenderer spriteRenderer;
    public Light2D flash;

    private void Awake() {
        textMeshPro = transform.Find("ItemAmount").GetComponent<TextMeshPro>();
    }

    public static BaseItem dropItem(Item item) {
        BaseItem itemInWorld = spawnItemInWorld(item, GridManager.instance.getTileAtPosition(UnitManager.instance.player.getOccupiedTile().tilePosition));
       
        return itemInWorld;
    }

    public void setItem(Item item) {
        this.item = item;

        spriteRenderer.sprite = item.getSprite();

        if(item.amount > 1) {
            textMeshPro.SetText(item.amount.ToString());
        }

        else {
            textMeshPro.SetText("");
        }
    }

    public Item getItem() {
        return item;
    }

    public void destroySelf() {
        Destroy(gameObject);
    }

    public static BaseItem spawnItemInWorld(Item item, BaseTile tile) {
        var spawnedItem = Instantiate(ItemAssets.instance.itemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        
        BaseItem itemInWorld = spawnedItem.GetComponent<BaseItem>();
        itemInWorld.getColorBasedOnRarity(item);
        itemInWorld.setItem(item);

        tile.setItemOnTile(itemInWorld);

        return itemInWorld;
    }

    private void getColorBasedOnRarity(Item item) {
        switch(item.itemType) {
            case Item.ItemType.Crossbow:
                flash.color = Color.blue;
                break;

            default: break;
        }
    }
}