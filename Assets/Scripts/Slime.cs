using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    [SerializeField] private ContactFilter2D _ground;

    private bool _isOnGround => _enemyRb.IsTouching(_ground);

    private GameObject _player;

    private void FixedUpdate()
    {
        ChasePlayer();
    }

    private void Jump()
    {
        _enemyAnimator.SetBool("IsMoving", true);

        if (_isOnGround)
        {
            _enemyRb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void ChasePlayer()
    {
        if (_player == null) return;

        _enemyRb.AddForce(Vector2.left * _speed);

        Jump();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
            _enemyAnimator.SetBool("IsMoving", false);
        }
    }
}