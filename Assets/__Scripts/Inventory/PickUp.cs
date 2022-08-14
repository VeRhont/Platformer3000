using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory _inventory;
    public GameObject SlotButton;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < _inventory.Slots.Length; i++)
            {
                if (_inventory.IsFull[i] == false)
                {
                    _inventory.IsFull[i] = true;
                    var removeButton = _inventory.Slots[i].transform.Find("RemoveButton").gameObject;
                    removeButton.SetActive(true);

                    Instantiate(SlotButton, _inventory.Slots[i].transform);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}