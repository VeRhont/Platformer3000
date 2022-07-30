using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    [SerializeField] private ContactFilter2D _ground;

    private bool _isOnGround => _enemyRb.IsTouching(_ground);

    private GameObject _player;

    private void FixedUpdate()
    {
        ChasePlayer();
    }

    private void MoveToPlayer()
    {
        _enemyAnimator.SetBool("IsMoving", true);

        LookAtPlayer();

        if (_isOnGround)
        {
            _enemyRb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void ChasePlayer()
    {
        if (_player == null) return;

        MoveToPlayer();
    }

    private void LookAtPlayer()
    {
        var scaleX = _player.transform.position.x > transform.position.x ? -1 : 1;

        transform.localScale = new Vector3(scaleX, 1, 1);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject;

            LookAtPlayer();
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