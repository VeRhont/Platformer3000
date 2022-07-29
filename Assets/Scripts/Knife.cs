using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float _speed;
    [SerializeField, Range(1, 10)] private float _lifeTime;
    [SerializeField] private float _damage;

    private void FixedUpdate()
    {
        if (_lifeTime <= 0)
            Destroy(gameObject);

        transform.Translate(Vector2.right * _speed * Time.deltaTime);

        _lifeTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }
}
