using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    public float JumpForce
    {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }

    [Header("Health")]
    [SerializeField] private Image _healthBar;
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

    [Header("Inventory")]
    [SerializeField] private InventoryUI _uiInventory;
    private Inventory _inventory;

    [Header("Components")]
    private Animator _playerAnimator;
    private Rigidbody2D _playerRb;

    [SerializeField] private ContactFilter2D _ground;
    private bool IsOnGround => _playerRb.IsTouching(_ground);

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();

        _inventory = new Inventory();
        _uiInventory.SetInventory(_inventory);
    }

    private void Start()
    {
        _health = _maxHealth;
        UpdateHealth();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
            Jump();

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

        if (Input.GetButton("Horizontal"))
            Run();
        else
            _playerAnimator.SetBool("IsMoving", false);
    }

    private void Run()
    {
        var horizontalDirection = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + horizontalDirection, _speed * Time.deltaTime);

        transform.localScale = new Vector3((horizontalDirection.x >= 0f ? 1f : -1f), 1f, 1f);

        _playerAnimator.SetBool("IsMoving", horizontalDirection.x != 0.0f);
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
        if (_health == 0)
            Die();

        _healthBar.fillAmount = _health / _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _playerAnimator.SetTrigger("Damage");

        _health = Mathf.Max(0, _health - damage);

        UpdateHealth();
    }

    public void Heal(float hp)
    {
        _health = Mathf.Min(100f, _health + hp);
        UpdateHealth();
    }

    private void Die()
    {
        _playerAnimator.SetBool("IsDead", true);
        SceneManager.LoadScene(0);
    }
}