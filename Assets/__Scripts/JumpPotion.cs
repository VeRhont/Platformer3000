using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JumpPotion : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _jumpForceIncreasing;
    [SerializeField] private float _duration;

    private void Update()
    {
        
    }

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
