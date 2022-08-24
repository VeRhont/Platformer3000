using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy's stats")]
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float _maxHealth;

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
        Destroy(gameObject);
    }
}