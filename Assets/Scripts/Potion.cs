using System.Collections;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        Debug.Log("Start");
        yield return new WaitForSecondsRealtime(10);
        Debug.Log("Finish");
        Destroy(gameObject);
    }
}
