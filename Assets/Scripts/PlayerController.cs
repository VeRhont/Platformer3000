using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _maxHealth;
    private float _health;

    [Header("Attack")]
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _attackRate = 2f;
    [SerializeField] private float _nextAttackTime = 0f;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _enemyLayers;

    [SerializeField] private GameObject[] _knifePrefabs;
    [SerializeField] private Transform _throwKnifePosition;

    [Header("Components")]
    private Animator _playerAnimator;
    private Rigidbody2D _playerRb;

    [SerializeField] private ContactFilter2D _ground;
    private bool _isOnGround => _playerRb.IsTouching(_ground);

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _health = _maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            Jump();
        }

        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
                _nextAttackTime = Time.time + 1f / _attackRate;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ThrowKnife();
                _nextAttackTime = Time.time + 1f / _attackRate;
            }
        }
        
    }

    private void FixedUpdate()
    {
        var movement = Input.GetAxis("Horizontal");

        _playerAnimator.SetBool("IsMoving", movement != 0.0f);

        var xScale = _playerRb.velocity.x >= 0 ? 1 : -1;
        transform.localScale = new Vector3(xScale, 1, 1);        

        _playerRb.AddForce(Vector2.right * _speed * movement);
    }

    private void Jump()
    {
        _playerAnimator.SetTrigger("Jump");
        _playerRb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    #region attack

    private void ThrowKnife()
    {
        _playerAnimator.SetTrigger("ThrowKnife");
    }

    public void OnThrowKnife()
    {
        var index = Random.Range(0, _knifePrefabs.Length);
        Instantiate(_knifePrefabs[index], _throwKnifePosition.position, Quaternion.identity);
    }

    private void Attack()
    {
        _playerAnimator.SetTrigger("Attack");
    }

    public void OnAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRange, _enemyLayers);

        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPosition == null) return;

        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);
    }
    #endregion

    private void UpdateHealth()
    {

    }

    private void TakeDamage()
    {
        _playerAnimator.SetTrigger("Damage");
    }

    private void Die()
    {
        _playerAnimator.SetBool("IsDead", true);
    }
}