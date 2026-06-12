using UnityEngine;

public class Crafting : MonoBehaviour
{
    public float interactRange = 3f;
    public KeyCode interactKey = KeyCode.E;
    public Transform cameraTransform;

    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("CraftingStation"))
            {
                // هایلایت کراس‌هیر (اختیاری)
                if (Input.GetKeyDown(interactKey))
                {
                    OpenCraftingMenu();
                }
            }
        }
    }

    void OpenCraftingMenu()
    {
        // محاسبه تعداد آیتم‌های غیرخالی در اینونتوری
        int itemCount = 0;
        foreach (var slot in inventory.slots)
        {
            if (slot != null && !string.IsNullOrEmpty(slot.itemName) && slot.amount > 0)
                itemCount++;
        }
        Debug.Log("🔧 Crafting menu opened. Inventory contains: " + itemCount + " items.");
        // بعداً یک UI واقعی باز می‌شود
    }
}