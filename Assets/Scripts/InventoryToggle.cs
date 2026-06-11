using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isActive = !inventoryPanel.activeSelf;
            inventoryPanel.SetActive(isActive);

            // قفل و نمایش موس
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isActive;
        }
    }
}