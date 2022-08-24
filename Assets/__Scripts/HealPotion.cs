using System.Collections;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    [SerializeField] private float _heal;

    private ParticleSystem _healParticles;
    private GameObject _player;

    private void Start()
    {
        _healParticles = GameObject.FindGameObjectWithTag("HealPotionParticles").GetComponent<ParticleSystem>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UseHealPotion()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().Heal(_heal);

        _healParticles.transform.position = _player.transform.position;
        _healParticles.Play();

        Destroy(gameObject);
    }
}