using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform _followingTarget;
    [SerializeField, Range(0, 1)] private float _parallaxStrength;
    [SerializeField] private bool _disableVerticalParallax;

    private Vector3 _previousTargetPosition;

    private void Start()
    {
        if (_followingTarget == false)
        {
            _followingTarget = Camera.main.transform;
        }

        _previousTargetPosition = _followingTarget.position;
    }

    private void Update()
    {
        var delta = _followingTarget.position - _previousTargetPosition;

        if (_disableVerticalParallax)
            delta.y = 0;

        _previousTargetPosition = _followingTarget.position;

        transform.position += delta * _parallaxStrength;
    }
}
