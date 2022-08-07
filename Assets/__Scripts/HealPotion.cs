using System.Collections;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    [SerializeField] private float _heal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Heal(_heal);

            Destroy(gameObject);
        }
    }
}