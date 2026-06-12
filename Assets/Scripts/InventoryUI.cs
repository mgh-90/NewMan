using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject hotbarSlotPrefab;
    public Transform hotbarParent;
    public Inventory inventory;
    public int hotbarSize = 9;

    private List<GameObject> hotbarSlots = new List<GameObject>();
    private List<TextMeshProUGUI> amountTexts = new List<TextMeshProUGUI>();
    private List<Image> icons = new List<Image>();

    void Start()
    {
        if (inventory == null)
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        CreateHotbarSlots();
    }

    void CreateHotbarSlots()
    {
        foreach (var slot in hotbarSlots) Destroy(slot);
        hotbarSlots.Clear();
        amountTexts.Clear();
        icons.Clear();

        for (int i = 0; i < hotbarSize; i++)
        {
            GameObject slot = Instantiate(hotbarSlotPrefab, hotbarParent);
            slot.transform.SetParent(hotbarParent, false);
            hotbarSlots.Add(slot);

            Transform amountTransform = slot.transform.Find("AmountText");
            Transform iconTransform = slot.transform.Find("Icon");
            amountTexts.Add(amountTransform?.GetComponent<TextMeshProUGUI>());
            icons.Add(iconTransform?.GetComponent<Image>());
        }
    }

    void Update() => UpdateUI();

    void UpdateUI()
    {
        for (int i = 0; i < hotbarSize; i++)
        {
            var slot = inventory.GetSlot(i);
            bool hasItem = slot != null && !string.IsNullOrEmpty(slot.itemName) && slot.amount > 0;
            if (hasItem)
            {
                if (amountTexts[i] != null) amountTexts[i].text = slot.amount.ToString();
                if (icons[i] != null) icons[i].color = Color.green;
            }
            else
            {
                if (amountTexts[i] != null) amountTexts[i].text = "";
                if (icons[i] != null) icons[i].color = Color.gray;
            }
        }
    }

    // دسترسی برای HotbarSelector
    public List<GameObject> GetHotbarSlots() => hotbarSlots;
}