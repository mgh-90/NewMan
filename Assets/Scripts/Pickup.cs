using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Settings")]
    public float pickupRange = 3f;          // حداکثر فاصله برای برداشتن
    public KeyCode pickupKey = KeyCode.E;
    public Transform cameraTransform;       // دوربین اول شخص (Main Camera)

    [Header("Feedback (Optional)")]
    public GameObject crosshair;            // برای تغییر رنگ کراس‌هیر
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.yellow;

    private Inventory inventory;
    private GameObject currentTarget;       // آیتمی که در حال حاضر نشانه رفته

    void Start()
    {
        inventory = GetComponent<Inventory>();
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        if (crosshair != null && crosshair.TryGetComponent<UnityEngine.UI.Image>(out var img))
        {
            img.color = defaultColor;
        }
    }

    void Update()
    {
        // هر فریم یک Ray از مرکز دوربین به جلو پرتاب کن
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        // آیا به چیزی برخورد کرده؟
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            // شیء برخورد کرده باید تگ "Pickup" داشته باشد (اختیاری) یا هر آیتمی که اسکریپت PickupItem داشته باشد
            // برای سادگی، فرض می‌کنیم هر شیء با تگ "Pickup" قابل برداشت است.
            if (hit.collider.CompareTag("Pickup"))
            {
                currentTarget = hit.collider.gameObject;
                HighlightTarget(true);
                if (Input.GetKeyDown(pickupKey))
                {
                    PickUpItem(currentTarget);
                }
            }
            else
            {
                HighlightTarget(false);
                currentTarget = null;
            }
        }
        else
        {
            HighlightTarget(false); currentTarget = null;
            currentTarget = null;
        }
    }

    void PickUpItem(GameObject item)
    {
        // فرض می‌کنیم اسم آیتم یا یک کامپوننت خاص دارد
        string itemName = item.name;
        inventory.AddItem(itemName, 1);
        Debug.Log($"Picked up: {itemName}");
        Destroy(item);
        HighlightTarget(false);
        currentTarget = null;
    }

    void HighlightTarget(bool highlight)
    {
        if (crosshair != null && crosshair.TryGetComponent<UnityEngine.UI.Image>(out var img))
        {
            img.color = highlight ? highlightColor : defaultColor;
        }
        // در آینده می‌توانی افکت‌های دیگری مثل بزرگ‌نمایی، تغییر رنگ آیتم و... اضافه کنی
    }
}