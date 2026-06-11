using UnityEngine;
using System.Collections.Generic;

public class HotbarSelector : MonoBehaviour
{
    public int selectedSlot = 0;
    private InventoryUI inventoryUI;

    void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    void Update()
    {
        if (inventoryUI == null) return;
        List<GameObject> hotbarSlots = inventoryUI.currentHotbarSlots;

        // انتخاب با کلیدهای 1 تا 9
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (i < hotbarSlots.Count)
                {
                    selectedSlot = i;
                    Debug.Log($"Selected slot {selectedSlot + 1}");
                }
                break;
            }
        }

        // انتخاب با چرخ ماوس
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            int newSlot = selectedSlot - (int)Mathf.Sign(scroll);
            if (newSlot >= 0 && newSlot < hotbarSlots.Count)
            {
                selectedSlot = newSlot;
                Debug.Log($"Selected slot {selectedSlot + 1}");
            }
        }
    }

    void LateUpdate()
    {
        // بعد از اینکه InventoryUI اسلات‌ها را بازسازی کرد، هایلایت را به‌روز می‌کنیم
        if (inventoryUI == null) return;
        List<GameObject> hotbarSlots = inventoryUI.currentHotbarSlots;
        if (hotbarSlots == null || hotbarSlots.Count == 0) return;

        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            Transform highlight = hotbarSlots[i].transform.Find("SelectedHighlight");
            if (highlight != null)
                highlight.gameObject.SetActive(i == selectedSlot);
        }
    }
}