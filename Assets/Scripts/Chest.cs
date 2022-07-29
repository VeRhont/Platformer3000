using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject[] _dropObjects;
    [SerializeField] private TextMeshProUGUI _pressR;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _pressR.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                foreach (var obj in _dropObjects)
                {
                    Instantiate(obj);
                }

                _pressR.gameObject.SetActive(false);

                Destroy(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _pressR.gameObject.SetActive(false);
    }
}
