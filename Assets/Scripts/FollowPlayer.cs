using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _offset;

    [Header("Camera")]
    [SerializeField] private float _changeVelocity;
    [SerializeField] private float _deltaOffsetY = 0.004f;
    [SerializeField] private float _deltaOffsetSize = 0.01f;
    [SerializeField] private float _maxSize;

    private Camera _camera;
    private Rigidbody2D _playerRb;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _playerRb = _player.GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    { 
        if (Mathf.Abs(_playerRb.velocity.x) >= _changeVelocity)
        {
            _offset.y = Mathf.Min(4, _offset.y + _deltaOffsetY);
            _camera.orthographicSize = Mathf.Min(_maxSize, _camera.orthographicSize + _deltaOffsetSize);
        }
        else
        {
            _offset.y = Mathf.Max(3, _offset.y - _deltaOffsetY);
            _camera.orthographicSize = Mathf.Max(5, _camera.orthographicSize - _deltaOffsetSize);
        }

        transform.position = _player.transform.position + _offset;
    }
}