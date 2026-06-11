using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions; // برای پاک کردن پسوندها

public class HeldItem : MonoBehaviour
{
    public HotbarSelector hotbarSelector;
    public Inventory inventory;
    public GameObject[] itemPrefabs; // نام پریفاب‌ها باید با نام اصلی آیتم‌ها یکی باشد

    private GameObject currentItem;

    void Start()
    {
        if (hotbarSelector == null)
            hotbarSelector = GetComponentInParent<HotbarSelector>();
        if (inventory == null)
            inventory = GetComponentInParent<Inventory>();

        if (hotbarSelector == null)
            Debug.LogError("HeldItem: HotbarSelector not found!");
        if (inventory == null)
            Debug.LogError("HeldItem: Inventory not found!");
    }

    void Update()
    {
        if (hotbarSelector == null || inventory == null) return;

        int selected = hotbarSelector.selectedSlot;
        var itemsList = new List<KeyValuePair<string, int>>(inventory.items);
        if (selected < itemsList.Count)
        {
            string rawItemName = itemsList[selected].Key;
            ShowItem(rawItemName);
        }
        else
        {
            HideItem();
        }
    }

    void ShowItem(string rawItemName)
    {
        // حذف "(Clone)" و "(1)" و "...(2)" از نام آیتم
        string cleanName = Regex.Replace(rawItemName, @"\s*\([^)]*\)", "");
        Debug.Log("ShowItem called for: " + rawItemName + " → cleaned: " + cleanName);

        GameObject prefab = GetPrefabByName(cleanName);
        if (prefab == null)
        {
            Debug.LogWarning("No prefab found for: " + cleanName);
            return;
        }

        // اگر همین مدل هم‌اکنون در دست است، نیازی به ساخت مجدد نیست
        if (currentItem != null && currentItem.name == prefab.name)
            return;

        // حذف مدل قبلی
        if (currentItem != null)
            Destroy(currentItem);

        // ساخت مدل جدید به عنوان فرزند HandSlot
        currentItem = Instantiate(prefab, transform);
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;
        currentItem.transform.localScale = prefab.transform.localScale;

        Debug.Log("Spawned item in hand: " + currentItem.name);
    }

    void HideItem()
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
            currentItem = null;
            Debug.Log("Item hidden from hand");
        }
    }

    GameObject GetPrefabByName(string name)
    {
        foreach (var prefab in itemPrefabs)
        {
            if (prefab != null && prefab.name == name)
                return prefab;
        }
        return null;
    }
}