using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float _damage = 100f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }
}
