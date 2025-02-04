using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventorySlotPrefab;
    public Transform inventoryPanel;

    private List<InventoryItem> collectedItem = new List<InventoryItem>();

    public void AddItem(InventoryItem newItem)
    {
        collectedItem.Add(newItem);

        GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
        Button button = slot.GetComponent<Button>();

        Image image = slot.transform.Find("Icon").GetComponent<Image>();
        image.sprite = newItem.itemSprite;

        TextMeshProUGUI itemName = slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemName.text = newItem.itemName;

        button.onClick.AddListener(() => UseItem(newItem));
    }

    public void UseItem(InventoryItem item)
    {
        switch(item.itemType)
        {
            case ItemType.Health:
                Debug.Log("Vita"+item.restoreValue);
                break;
            case ItemType.Mana:
                Debug.Log("Mana" + item.restoreValue);
                break;
        }
        collectedItem.Remove(item);

        RefreshInventoryUI();
    }

    private void RefreshInventoryUI()
    {
        foreach(Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        foreach(var item in collectedItem)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
            Button button = slot.GetComponent<Button>();

            Image image = slot.transform.Find("Icon").GetComponent<Image>();
            image.sprite = item.itemSprite;

            TextMeshProUGUI itemName = slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            itemName.text = item.itemName;

            button.onClick.AddListener(() => UseItem(item));
        }
    }
}
