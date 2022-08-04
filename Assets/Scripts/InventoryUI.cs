using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory _inventory;

    private Transform _itemSlotContainer;
    private Transform _itemSlotTemplate;

    private void Awake()
    {
        _itemSlotContainer = transform.Find("itemSlotContainer");
        _itemSlotTemplate = _itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
    }

    private void RefreshInventoryItems()
    {
        foreach (var item in _inventory.GetItemList())
        {
            Instantiate(_itemSlotTemplate, _itemSlotContainer);

            var itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
        }
    }
}
