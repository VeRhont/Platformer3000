using UnityEngine;
using TMPro;

public class Crate : MonoBehaviour
{
    [SerializeField] private Transform _parentObject;
    [SerializeField] private TextMeshProUGUI _hint;

    private bool _isActive = false;

    private void LateUpdate()
    {
        if (_isActive)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                var player = GameObject.FindWithTag("Player");
                transform.SetParent(player.transform);
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                _isActive = false;
                transform.parent = _parentObject;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isActive = true;
            _hint.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _hint.gameObject.SetActive(false);
    }
}