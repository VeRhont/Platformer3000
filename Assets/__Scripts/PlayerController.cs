using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;
    public bool IsGameActive = true;

    public float Health
    {
        get { return _health; }
        set
        {
            if (value >= 0 && value <= _maxHealth)
                _health = value;
        }
    }
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    [Header("Player Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpPressedRememberTime = 0.1f;
    [SerializeField] private float _jumpGroundedRememberTime = 0.1f;
    [SerializeField, Range(0, 1)] private float _cutJumpHeight;
    private float _jumpPressedRemember = 0f;
    private float _jumpGroundedRemember = 0f;

    public float JumpForce
    {
        get { return _jumpForce; }
        set
        {
            if (value > 0)
                _jumpForce = value;
        }
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

    [Header("Shield")]
    [SerializeField] private float _maxShieldDuration;
    [SerializeField] private float _maxCooldown;
    [SerializeField] private Image _useShieldImage;
    [SerializeField] private GameObject _shield;
    private float _shieldDuration;
    private float _cooldown;
    private bool _isShieldActive = false;

    [Header("Potions")]
    [SerializeField] private GameObject _useJumpPotionImage;
    private bool _isFirstIteration = true;
    private float _addingJumpForce;
    private float _endTime;

    [Header("Components")]
    private Animator _playerAnimator;
    private Rigidbody2D _playerRb;

    [SerializeField] private ContactFilter2D _ground;
    private bool IsOnGround => _playerRb.IsTouching(_ground);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one player");

        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _health = _maxHealth;
        _cooldown = _maxCooldown;

        UpdateHealth();
    }

    private void Update()
    {
        _jumpGroundedRemember -= Time.deltaTime;
        if (IsOnGround)
        {
            _jumpGroundedRemember = _jumpGroundedRememberTime;
        }

        _jumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressedRemember = _jumpPressedRememberTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (_playerRb.velocity.y > 0)
            {
                _playerRb.velocity = new Vector2(_playerRb.velocity.x, _playerRb.velocity.y * _cutJumpHeight);
            }
        }

        if ((_jumpPressedRemember > 0) && (_jumpGroundedRemember > 0))
        {
            _jumpPressedRemember = 0;
            _jumpGroundedRemember = 0;
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

        if (Input.GetButton("Horizontal"))
            Run();
        else
            _playerAnimator.SetBool("IsMoving", false);

        if (Input.GetKeyDown(KeyCode.Q) && _cooldown >= _maxCooldown)
        {
            UseShield();
        }

        UpdateShield();
        if (_isFirstIteration == false)
        {
            IncreaseJumpForce(_addingJumpForce, _endTime);
        }
    }

    private void UseShield()
    {
        _isShieldActive = true;
        _shield.SetActive(true);

        _shieldDuration = _maxShieldDuration;

        _cooldown = 0;
    }

    private void UpdateShield()
    {
        // reloading
        if (_cooldown < _maxCooldown)
        {
            _cooldown = Mathf.Min(_cooldown + Time.deltaTime, _maxCooldown);
            _useShieldImage.fillAmount = _cooldown / _maxCooldown;
        }

        if (_isShieldActive)
        {
            _shieldDuration = Mathf.Max(0, _shieldDuration - Time.deltaTime);

            if (_shieldDuration == 0)
            {
                _isShieldActive = false;
                _shield.SetActive(false);
            }
        }
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

    #region IncreaseJumpForce

    public void IncreaseJumpForce(float addingJumpForce, float endTime)
    {
        if (_isFirstIteration)
        {
            _addingJumpForce = addingJumpForce;
            _endTime = endTime;
            _isFirstIteration = false;

            JumpForce += _addingJumpForce;

            _useJumpPotionImage.gameObject.SetActive(true);
        }

        var image = _useJumpPotionImage.transform.Find("PotionActive").GetComponent<Image>();
        image.fillAmount = (endTime - Time.time) / 5;

        if (Time.time >= _endTime)
        {
            JumpForce -= _addingJumpForce;
            _isFirstIteration = true;

            _useJumpPotionImage.gameObject.SetActive(false);
        }
    }
    #endregion

    #region attack

    private void ThrowKnife()
    {
        _playerAnimator.SetTrigger("ThrowKnife");
    }

    public void OnThrowKnife()
    {
        var index = Random.Range(0, _knifePrefabs.Length);
        var knife = Instantiate(_knifePrefabs[index], _throwKnifePosition.position, Quaternion.identity);

        if (transform.localScale.x == -1)
        {
            knife.transform.eulerAngles = new Vector3(0, 180, 0);
        }
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

    public void UpdateHealth()
    {
        if (_health == 0)
            Die();

        _healthBar.fillAmount = _health / _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_isShieldActive == false)
        {
            _playerAnimator.SetTrigger("Damage");

            _health = Mathf.Max(0, _health - damage);

            UpdateHealth();
        }
    }

    public void Heal(float hp)
    {
        _health = Mathf.Min(100f, _health + hp);
        UpdateHealth();
    }

    private void Die()
    {
        _playerAnimator.SetBool("IsDead", true);

        Invoke("RestartScene", 1.5f);
    }

    private PlayerDataSaveLoad _saveLoadSystem;

    private void RestartScene()
    {
        if (_saveLoadSystem == null)
        {
            _saveLoadSystem = GameObject.Find("_GameManager").GetComponent<PlayerDataSaveLoad>();
        }
        _playerAnimator.SetBool("IsDead", false);
        _saveLoadSystem.LoadPlayer();
    }
}