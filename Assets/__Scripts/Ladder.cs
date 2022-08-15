using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _playerRb;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {       
            if (Input.GetAxis("Vertical") != 0)
            {
                _playerRb = other.GetComponent<Rigidbody2D>();
                _playerRb.gravityScale = 0;
                _playerRb.isKinematic = true;

                var direction = Mathf.RoundToInt(Input.GetAxis("Vertical"));
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, direction * _speed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerRb.isKinematic = false;
        _playerRb.gravityScale = 1;
    }
}