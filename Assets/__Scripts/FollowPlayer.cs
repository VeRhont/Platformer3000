using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _offset;

    [Header("Camera")]
    [SerializeField] private float _changeVelocity;
    [SerializeField] private float _standartSize;
    [SerializeField] private float _maxSize;
    [SerializeField] private float _deltaSize = 0.001f;
    [SerializeField] private float _normalYOffset;
    [SerializeField] private float _cameraMoveSpeed = 5;

    private Camera _camera;
    private Rigidbody2D _playerRb;
    private float _currentSize;

    private void Awake()
    {
        _camera = Camera.main;
        _playerRb = _player.GetComponent<Rigidbody2D>();

        _currentSize = _maxSize;
        _normalYOffset = _offset.y;
    }

    private void Update()
    {
        var verticalInput = Input.GetAxis("Vertical");

        if (_playerRb.isKinematic == false)
        {
            if (verticalInput < 0 && _normalYOffset - _offset.y < 3)
            {
                _offset.y += verticalInput * _cameraMoveSpeed * Time.deltaTime;
            }
            else
            {
                _offset.y = Mathf.Min(_normalYOffset, _offset.y + 0.05f);
            }
        }
    }

    private void LateUpdate()
    {
        if (_playerRb.velocity.y >= _changeVelocity)
        {
            _currentSize = Mathf.Min(_maxSize, _currentSize + _deltaSize);
            _camera.orthographicSize = _currentSize;
        }
        else
        {
            _currentSize = Mathf.Max(_standartSize, _currentSize - _deltaSize);
            _camera.orthographicSize = _currentSize;
        }

        transform.position = _player.transform.position + _offset;
    }
}