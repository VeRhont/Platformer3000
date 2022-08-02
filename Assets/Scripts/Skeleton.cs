using UnityEngine;

public class Skeleton : Enemy
{
    [Header("Attack")]
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _attackRate = 2f;
    [SerializeField] private float _nextAttackTime = 0f;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _attackDistance;

    private GameObject _player;

    private void Update()
    {
        if (_player == null)
        {
            _enemyAnimator.SetBool("IsMoving", false);
            return;
        }

        if (Mathf.Abs(transform.position.x - _player.transform.position.x) < _attackDistance)
        {
            if (Time.time >= _nextAttackTime)
            {
                Attack();
                _nextAttackTime = Time.time + 1f / _attackRate;
            }
        }

        RunToPlayer();
    }

    private void RunToPlayer()
    {
        _enemyAnimator.SetBool("IsMoving", true);
        if (_player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        var direction =  _player.transform.position - transform.position;

        _enemyRb.MovePosition(transform.position + direction * Time.deltaTime);
    }

    private void Attack()
    {
        _enemyAnimator.SetTrigger("Attack");

        Collider2D hitPlayer = Physics2D.OverlapCircle(_attackPosition.position, _attackRange, _playerLayer);

        hitPlayer.GetComponent<PlayerController>().TakeDamage(_attackDamage);

    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPosition == null) return;

        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);
    }

    public override void Die()
    {
        _enemyAnimator.SetBool("IsDead", true);

        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }
}