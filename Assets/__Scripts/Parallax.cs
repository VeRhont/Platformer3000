using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallax : MonoBehaviour
{
    private float _length;
    private float _startPosition;
    private float _positionY;
    private Camera _camera;

    [SerializeField, Range(0, 1)] private float _parallaxStrength;

    private void Start()
    {
        _startPosition = transform.position.x;
        _positionY = transform.position.y;

        _camera = Camera.main;
        _length = GetComponent<SpriteRenderer>().size.x;
    }

    private void Update()
    {
        var temp = _camera.transform.position.x * (1 - _parallaxStrength);
        var dist = _camera.transform.position.x * _parallaxStrength;

        transform.position = new Vector3(_startPosition + dist, _positionY, transform.position.z);

        if (temp > _startPosition + _length)
        {
            _startPosition += _length;
        }
        else if (temp < _startPosition - _length)
        {
            _startPosition -= _length;
        }
    }
}