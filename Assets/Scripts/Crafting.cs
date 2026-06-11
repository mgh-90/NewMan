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
                // هایلایت کراس‌هیر (اختیاری، می‌توانی یک متد مشترک بسازی)
                if (Input.GetKeyDown(interactKey))
                {
                    OpenCraftingMenu();
                }
            }
        }
    }

    void OpenCraftingMenu()
    {
        Debug.Log("🔧 Crafting menu opened. Inventory contains: " + inventory.items.Count + " items.");
        // بعداً یک UI واقعی باز می‌شود
    }
}