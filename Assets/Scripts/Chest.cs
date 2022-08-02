using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<GameObject> _dropObjects;
    [SerializeField] private TextMeshProUGUI _pressR;
    [SerializeField] private GameObject _openedChest;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _pressR.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                OpenChest();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _pressR.gameObject.SetActive(false);
    }

    private void OpenChest()
    {
        _openedChest.SetActive(true);

        foreach (var obj in _dropObjects)
        {
            Instantiate(obj, transform.position, Quaternion.identity);
        }

        _pressR.gameObject.SetActive(false);

        Destroy(this);
    }
}