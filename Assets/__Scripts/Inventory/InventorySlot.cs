using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private Inventory _inventory;
    public int index;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (transform.childCount <= 2)
        {
            _inventory.IsFull[index] = false;
            transform.Find("RemoveButton").gameObject.SetActive(false);
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            try
            {
                child.GetComponent<Drop>().SpawnDroppedItem();
                GameObject.Destroy(child.gameObject);
            }
            catch { }
        }
    }
}