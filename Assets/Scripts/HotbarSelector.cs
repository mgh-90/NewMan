using UnityEngine;
using System.Collections.Generic;

public class HotbarSelector : MonoBehaviour
{
    public int selectedSlot = -1;  // -1 یعنی هیچ اسلاتی انتخاب نشده
    private InventoryUI inventoryUI;

    void Start() => inventoryUI = FindObjectOfType<InventoryUI>();

    void Update()
    {
        if (inventoryUI == null) return;
        List<GameObject> hotbarSlots = inventoryUI.GetHotbarSlots();
        if (hotbarSlots == null) return;

        // کلیدهای 1 تا 9
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (selectedSlot == i)
                {
                    // اگر همان اسلات دوباره زده شد، آن را deselect کن
                    selectedSlot = -1;
                }
                else
                {
                    selectedSlot = i;
                }
                break;
            }
        }

        // اسکرول ماوس: اگر اسکرول کرد، همیشه یک اسلات انتخاب می‌شود (حتی اگر قبلاً -1 بود)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            if (selectedSlot == -1)
                selectedSlot = 0; // اگر هیچی انتخاب نبود، اولین اسلات رو انتخاب کن
            else
            {
                int newSlot = selectedSlot - (int)Mathf.Sign(scroll);
                if (newSlot >= 0 && newSlot < 9) selectedSlot = newSlot;
            }
        }
    }

    void LateUpdate()
    {
        if (inventoryUI == null) return;
        List<GameObject> hotbarSlots = inventoryUI.GetHotbarSlots();
        if (hotbarSlots == null || hotbarSlots.Count == 0) return;

        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            Transform highlight = hotbarSlots[i].transform.Find("SelectedHighlight");
            if (highlight != null)
                highlight.gameObject.SetActive(i == selectedSlot);
        }
    }
}