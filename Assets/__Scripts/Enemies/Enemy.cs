using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Health
    {
        get { return _health; }
        set { _health = value; }
    }
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [Header("Enemy's stats")]
    [SerializeField] protected HealthBar _healthBar;
    [SerializeField] private float _maxHealth;

    [SerializeField] private GameObject _xpPointPrefab;
    [SerializeField] private int _xpCount = 1;

    private float _health;

    protected Rigidbody2D _enemyRb;
    protected Animator _enemyAnimator;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();

        _enemyAnimator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        _health = _maxHealth;
        _healthBar.MaxHealth = _maxHealth;
        _healthBar.SetHealth(_health);
    }

    public void TakeDamage(float damage)
    {
        _enemyAnimator.SetTrigger("TakeDamage");

        _health = Mathf.Max(0, _health - damage);
        _healthBar.SetHealth(_health);

        if (_health == 0)
            Die();
    }

    public virtual void Die()
    {
        for (int i = 0; i < _xpCount; i++)
        {
            var xp = Instantiate(_xpPointPrefab, transform.position, Quaternion.identity);
            var direction = new Vector2(transform.position.x + Random.value, transform.position.y + Random.value);
            xp.GetComponent<Rigidbody2D>().AddForce(direction);
        }

        _healthBar.enabled = false;
    }
}