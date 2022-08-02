using System.Collections;
using UnityEngine;

public class JumpPotion : MonoBehaviour
{
    [SerializeField] private float _jumpForceIncreasing;
    [SerializeField] private float _duration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();

            StartCoroutine(IncreaseJumpForce(player));

            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator IncreaseJumpForce(PlayerController player)
    {
        Debug.Log("Start");

        player.JumpForce += _jumpForceIncreasing;

        yield return new WaitForSeconds(_duration);

        Debug.Log("Stop");
        player.JumpForce -= _jumpForceIncreasing;

        Destroy(gameObject);
    }
}
