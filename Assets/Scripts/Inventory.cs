using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddItem(string itemName, int amount = 1)
    {
        if (items.ContainsKey(itemName))
            items[itemName] += amount;
        else
            items[itemName] = amount;

        Debug.Log($"Added {amount}x {itemName}. Total: {items[itemName]}");
    }

    public bool RemoveItem(string itemName, int amount = 1)
    {
        if (!items.ContainsKey(itemName) || items[itemName] < amount)
            return false;

        items[itemName] -= amount;
        if (items[itemName] == 0)
            items.Remove(itemName);

        Debug.Log($"Removed {amount}x {itemName}. Remaining: {(items.ContainsKey(itemName) ? items[itemName] : 0)}");
        return true;
    }

    public int GetItemCount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }

    public void ListItems()
    {
        string list = "Inventory: ";
        foreach (var kvp in items)
            list += $"{kvp.Key} x{kvp.Value} ";
        Debug.Log(list);
    }
}