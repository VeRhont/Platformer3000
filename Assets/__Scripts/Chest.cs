using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Chest : MonoBehaviour
{
    [SerializeField] private bool _isClosed;

    [SerializeField] private List<GameObject> _dropObjects;
    [SerializeField] private TextMeshProUGUI _hint;
    [SerializeField] private GameObject _openedChest;

    [SerializeField] private AudioSource _openChestSound;

    private bool _isActive = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isActive = true;
            _hint.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (_isActive)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OpenChest();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isActive = false;
        _hint.gameObject.SetActive(false);
    }

    private void OpenChest()
    {
        _openChestSound.Play();
        _openedChest.SetActive(true);

        foreach (var obj in _dropObjects)
        {
            var dropObject = Instantiate(obj, transform.position, Quaternion.identity);
            dropObject.transform.position += new Vector3(Random.value, Random.value, dropObject.transform.position.z);
        }

        _hint.gameObject.SetActive(false);

        Destroy(this);
    }
}