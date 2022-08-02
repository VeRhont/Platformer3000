using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private void Update()
    {
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        _lifeTime -= Time.deltaTime;

        transform.position += transform.right * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}
