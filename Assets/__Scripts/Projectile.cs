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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }
}
