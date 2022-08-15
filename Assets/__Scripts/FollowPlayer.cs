using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _;

    [Header("Camera")]
    [SerializeField] private float _changeVelocity;
    [SerializeField] private float _standartSize;
    [SerializeField] private float _maxSize;
    [SerializeField] private float _deltaSize = 0.001f;

    private Camera _camera;
    private Rigidbody2D _playerRb;
    private float _currentSize;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _playerRb = _player.GetComponent<Rigidbody2D>();

        _currentSize = _maxSize;
    }

    private void Update()
    {
        var verticalInput = Input.GetAxis("Vertical");

        if (_playerRb.isKinematic == false)
        {
            if (verticalInput > 0)
            {
                _offset.y += verticalInput * Time.deltaTime;
            }
            else if (verticalInput < 0)
            {
                _offset.y += verticalInput * Time.deltaTime;
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