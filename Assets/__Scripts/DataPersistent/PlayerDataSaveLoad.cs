using UnityEngine;

public class PlayerDataSaveLoad : MonoBehaviour
{
    public static PlayerDataSaveLoad S;

    private PlayerController _player;
    private Inventory _playerInventory;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Error: PlayerDataSaveLoad");
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(_player, _playerInventory);

        for (int i = 0; i < _playerInventory.IsFull.Length; i++)
        {
            Debug.Log($"{i} {_playerInventory.IsFull[i]}");
        }
    }

    public void LoadPlayer()
    {
        var data = SaveSystem.LoadPlayer();

        PlayerController.Instance.Health = data.CurrentHealth;
        PlayerController.Instance.UpdateHealth();

        PlayerController.Instance.Position = new Vector3(data.Position[0], data.Position[1]);

        for (int i = 0; i < data.Slots.Length; i++)
        {
            _playerInventory.IsFull[i] = data.IsFull[i];

            if (_playerInventory.IsFull[i] == false)
            {
                _playerInventory.Slots[i].GetComponent<InventorySlot>().DropItem();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SavePlayer();
    }
}