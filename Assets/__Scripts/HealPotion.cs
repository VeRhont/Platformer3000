using System.Collections;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    [SerializeField] private float _heal;

    public void UseHealPotion()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().Heal(_heal);

        Destroy(gameObject);
    }
}