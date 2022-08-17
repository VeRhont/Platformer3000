using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    [SerializeField] private GameObject _shownText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _shownText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _shownText.SetActive(false);
        }
    }
}
