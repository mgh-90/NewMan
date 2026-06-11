# NewMan
"First-person chemist game with production and sales system."
# Chemist Game - Development Log

## Version 0.1 - Date 2026-06-12

### Current Status
- First-person movement with Character Controller
- Item pickup using Raycast and E key
- Inventory dictionary (item name, quantity)
- InventoryUI with hotbar (9 slots) and main panel (with Tab)
- Slot selection with number keys 1-9 and mouse scroll
- Held item display (HeldItem) with model swapping

### Key Scripts (with code summary)

#### Inventory.cs
- Dictionary <string, int>
- Methods: AddItem(name, amount), RemoveItem(name, amount), GetItemCount(name), ListItems()

#### Pickup.cs
- Attached to Player (previously on item)
- Raycast from camera center, range = 3
- Hits objects with tag "Pickup"
- Calls inventory.AddItem(item.name, 1)

#### HotbarSelector.cs
- selectedSlot (0 to 8)
- Input: number keys 1-9 and mouse scroll wheel
- Changes slot color via InventoryUI

#### HeldItem.cs
- Attached to HandSlot (child of Player)
- Reads selectedSlot from HotbarSelector
- Shows prefab matching item name (cleans name from "(Clone)")

### Prefabs
- `HotbarSlot` (contains Icon, AmountText, SelectedHighlight)
- `ItemSlot`
- `ChemicalVial` (simple test model)

### Inspector Settings
- HotbarPanel: Grid Layout Group, 9 columns, Cell Size 80x80
- InventoryPanel: Grid Layout Group, 4 columns, Cell Size 90x90, initially disabled
- HandSlot: Position (0.35, -0.25, 0.55), Rotation (10, -15, 0)

### Next Steps (as planned)
1. Build crafting system (crafting table + recipes)
2. Selling and economy system
3. Enemies (police, mafia, aliens)
