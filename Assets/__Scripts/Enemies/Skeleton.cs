using UnityEngine;

public class Skeleton : Enemy
{
    [Header("Attack")]
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _timeBetweenAttack = 2f;
    [SerializeField] private float _timeToNextAttack = 0f;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _deltaX = 0.5f;

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
            if (_timeToNextAttack <= 0)
            {
                Attack();
                _timeToNextAttack = _timeBetweenAttack;
            }
        }

        RunToPlayer();

        _timeToNextAttack -= Time.deltaTime;
    }

    private void RunToPlayer()
    {
        _enemyAnimator.SetBool("IsMoving", true);
        if (_player.transform.position.x + _deltaX > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        var deltaX =  _player.transform.position.x - transform.position.x;
        var direction = new Vector3(deltaX, 0, 0);

        _enemyRb.MovePosition(transform.position + direction * Time.deltaTime);
    }

    private void Attack()
    {
        _enemyAnimator.SetTrigger("Attack");

        Collider2D hitPlayer = Physics2D.OverlapCircle(_attackPosition.position, _attackRange, _playerLayer);

        if (hitPlayer != null)
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