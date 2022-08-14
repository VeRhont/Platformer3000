using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject Item;

    [SerializeField] private Vector2 _offset = new Vector2(2, 0);
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        var playerPosition = new Vector2(_player.position.x + _offset.x, _player.position.y + _offset.y);
        Instantiate(Item, playerPosition, Quaternion.identity);
    }
}