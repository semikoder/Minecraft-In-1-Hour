using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    private void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public PlayerController playerController;
    public Dictionary<Item, int> items = new Dictionary<Item, int>();
    public ItemSlot[] itemSlots;

    public Item giveItem;

    private void Start ()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].ClearSlot();
        }

        for (int i = 0; i < 100; i++)
        {
            GetItem(giveItem);
        }
    }

    private void Update ()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (Input.GetKeyDown(itemSlots[i].slotKeyCode))
            {
                // Select this item:
                Item desiredItem = itemSlots[i].SelectSlot();

                if (desiredItem != null)
                {
                    // Equip:
                    playerController.Equip(desiredItem);
                }
                else
                {
                    playerController.Equip(null);
                }
            }
        }
    }

    public void GetItem (Item newItem)
    {
        if (!items.ContainsKey(newItem))
        {
            items.Add(newItem, 1);
        }
        else
        {
            // add item:
            items[newItem] = items[newItem] + 1;
        }
        RefreshUI();
    }

    public void ConsumeItem (Item item)
    {
        items[item] -= 1;

        if (items[item] <= 0)
        {
            items.Remove(item);
        }
        RefreshUI();
    }

    private void RefreshUI ()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].ClearSlot();
        }

        int itemIndex = 0;
        foreach (KeyValuePair<Item, int> item in items)
        {
            if (itemIndex > itemSlots.Length)
            {
                // More items than item slots:
                return;
            }

            itemSlots[itemIndex].UpdateSlot(item.Key, item.Value);
            itemIndex++;
        }
    }
}
