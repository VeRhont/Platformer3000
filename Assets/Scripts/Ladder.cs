using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {       
            if (Input.GetAxis("Vertical") != 0)
            {
                other.GetComponent<Rigidbody2D>().gravityScale = 0;

                var direction = Mathf.RoundToInt(Input.GetAxis("Vertical"));
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, direction * _speed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
