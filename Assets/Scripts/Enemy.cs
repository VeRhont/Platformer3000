using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy's stats")]
    [SerializeField] private float _maxHealth;
    private float _health;

    protected Rigidbody2D _enemyRb;
    protected Animator _enemyAnimator;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemyAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _enemyAnimator.SetTrigger("TakeDamage");

        _health = Mathf.Max(0, _health - damage);

        UpdateHealth();

        if (_health == 0)
            Die();
    }

    public void Die()
    {
        Debug.Log(name + " is dead");
        Destroy(gameObject);
    }

    public void UpdateHealth()
    {

    }
}