using UnityEngine;

public class Slime : Enemy
{
    [Header("Set in Inspector: Slime")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackDistance = 0.5f;
    [SerializeField] private float _timeToNextAttack = 1f;

    [SerializeField] private ContactFilter2D _ground;

    private bool IsOnGround => _enemyRb.IsTouching(_ground);

    private float _direction;
    private GameObject _player;
    private ParticleSystem _deathParticles;

    private void Start()
    {
        _deathParticles = GameObject.FindGameObjectWithTag("SlimeParticles").GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    public override void Die()
    {
        _deathParticles.transform.position = gameObject.transform.position;
        _deathParticles.Play();
        base.Die();
    }

    private void MoveToPlayer()
    {
        if (_player == null) return;

        _enemyAnimator.SetBool("IsMoving", true);
        LookAtPlayer();

        transform.position += _direction * transform.right * _speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - _player.transform.position.x) <= _attackDistance)
        {
            if (_timeToNextAttack <= 0)
                Attack();
        }

        if (IsOnGround)
        {
            _enemyRb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        _timeToNextAttack -= Time.fixedDeltaTime;
    }

    private void LookAtPlayer()
    {
        var scaleX = _player.transform.position.x > transform.position.x ? -1 : 1;
        _direction = -scaleX;

        transform.localScale = new Vector3(scaleX, 1, 1);
    }

    private void Attack()
    {
        _player.GetComponent<PlayerController>().TakeDamage(_damage);
        _timeToNextAttack = 1f;
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