using System;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public string itemName;
    public int amount;
}

public class Inventory : MonoBehaviour
{
    public ItemSlot[] slots = new ItemSlot[9]; // 9 اسلات هاتبار

    void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) slots[i] = new ItemSlot();
            slots[i].itemName = "";
            slots[i].amount = 0;
        }
    }

    // اضافه کردن آیتم به اولین اسلات خالی یا هماندازه
    public bool AddItem(string itemName, int amount = 1)
    {
        // ابتدا تلاش برای انباشتن روی اسلات همسان
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemName == itemName && slots[i].amount > 0)
            {
                slots[i].amount += amount;
                return true;
            }
        }
        // سپس اولین اسلات خالی
        for (int i = 0; i < slots.Length; i++)
        {
            if (string.IsNullOrEmpty(slots[i].itemName) || slots[i].amount == 0)
            {
                slots[i].itemName = itemName;
                slots[i].amount = amount;
                return true;
            }
        }
        Debug.LogWarning("هیچ اسلات خالی برای آیتم " + itemName + " وجود ندارد");
        return false;
    }

    public bool RemoveItem(string itemName, int amount = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemName == itemName && slots[i].amount >= amount)
            {
                slots[i].amount -= amount;
                if (slots[i].amount == 0) slots[i].itemName = "";
                return true;
            }
        }
        return false;
    }

    public int GetItemCount(string itemName)
    {
        int total = 0;
        for (int i = 0; i < slots.Length; i++)
            if (slots[i].itemName == itemName) total += slots[i].amount;
        return total;
    }

    public ItemSlot GetSlot(int index)
    {
        if (index >= 0 && index < slots.Length) return slots[index];
        return null;
    }
}