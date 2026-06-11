using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject hotbarSlotPrefab;
    public Transform hotbarParent;
    public GameObject inventorySlotPrefab;
    public Transform inventoryParent;
    public Inventory inventory;
    public int hotbarSize = 9;

    public List<GameObject> currentHotbarSlots = new List<GameObject>();
    public List<GameObject> currentInventorySlots = new List<GameObject>();

    void Start()
    {
        if (inventory == null)
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        RefreshUI();
    }

    void Update()
    {
        // برای سادگی هر فریم به‌روز می‌شود. بعداً می‌توانید بهینه کنید.
        RefreshUI();
    }

    void RefreshUI()
    {
        ClearSlots();

        var itemsList = new List<KeyValuePair<string, int>>(inventory.items);

        // ===== نوار ابزار (هاتبار): ۹ آیتم اول =====
        for (int i = 0; i < hotbarSize && i < itemsList.Count; i++)
        {
            CreateSlot(hotbarSlotPrefab, hotbarParent, itemsList[i], currentHotbarSlots);
        }

        // ===== پنل اینونتوری: همه آیتم‌ها =====
        for (int i = 0; i < itemsList.Count; i++)
        {
            CreateSlot(inventorySlotPrefab, inventoryParent, itemsList[i], currentInventorySlots);
        }
    }

    void CreateSlot(GameObject prefab, Transform parent, KeyValuePair<string, int> item, List<GameObject> slotList)
    {
        GameObject slot = Instantiate(prefab, parent);
        slot.transform.SetParent(parent, false);

        // تنظیم آیکون (فعلاً رنگ سبز)
        Transform icon = slot.transform.Find("Icon");
        if (icon != null)
            icon.GetComponent<Image>().color = Color.green;

        // تنظیم تعداد
        Transform amountText = slot.transform.Find("AmountText");
        if (amountText != null)
            amountText.GetComponent<TextMeshProUGUI>().text = item.Value.ToString();

        slotList.Add(slot);
    }

    void ClearSlots()
    {
        foreach (var slot in currentHotbarSlots) Destroy(slot);
        foreach (var slot in currentInventorySlots) Destroy(slot);
        currentHotbarSlots.Clear();
        currentInventorySlots.Clear();
    }
}