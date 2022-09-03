using UnityEngine;

public class Skeleton : Enemy
{
    [Header("Attack")]
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _timeBetweenAttack = 2f;
    [SerializeField] private float _timeToNextAttack = 0f;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _deltaX = 0.5f;

    [SerializeField] private AudioSource _walkSound;
    [SerializeField] private AudioSource _deathSound;
    [SerializeField] private AudioSource _attackSound;

    private GameObject _player;

    private void Update()
    {
        if (_player == null)
        {
            _walkSound.Pause();
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
        _walkSound.UnPause();
        _enemyAnimator.SetBool("IsMoving", true);

        if (_player.transform.position.x + _deltaX > transform.position.x )
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        var horizontalDirection = transform.right * transform.localScale.x;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + horizontalDirection, _speed * Time.deltaTime);
    }

    private void Attack()
    {
        _attackSound.Play();
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
        base.Die();

        _deathSound.Play();
        _enemyAnimator.SetBool("IsDead", true);

        Destroy(gameObject, 1.5f);
        Destroy(this);

        Destroy(_healthBar.gameObject);
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