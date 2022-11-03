using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour {
    private Inventory inventory;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    public GameObject inventoryUi;

    private void Awake() {
        inventoryUi = GameObject.Find("UI");
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void setInventory(Inventory inv) {
        inventory = inv;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        refreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        refreshInventoryItems();
    }

    private void refreshInventoryItems() {
        foreach (Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate) continue;

            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 60f;

        foreach (Item item in inventory.getItems()) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();

            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                inventory.useItem(item);
            };

            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };

                inventory.removeItem(item);
                BaseItem.dropItem(duplicateItem);
            };

            Image image = itemSlotRectTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite = item.getSprite();

            TextMeshProUGUI itemAmountText = itemSlotRectTransform.Find("ItemAmount").GetComponent<TextMeshProUGUI>();
            
            if(item.amount > 1) {
                itemAmountText.SetText(item.amount.ToString());
            }

            else {
                itemAmountText.SetText("");
            }

            x++;

            if(x > 7) {
                x = 0;
                y--;
            }
        }
    }
}
