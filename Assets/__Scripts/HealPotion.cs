using UnityEngine;

public class HealPotion : MonoBehaviour
{
    [SerializeField] private float _heal;

    private ParticleSystem _healParticles;
    private AudioSource _potionSound;

    private GameObject _player;

    private void Start()
    {
        _healParticles = GameObject.FindGameObjectWithTag("HealPotionParticles").GetComponent<ParticleSystem>();
        _potionSound = GameObject.Find("PotionSound").GetComponent<AudioSource>();

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UseHealPotion()
    {
        _potionSound.Play();

        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().Heal(_heal);

        _healParticles.transform.position = _player.transform.position;
        _healParticles.Play();

        Destroy(gameObject);
    }
}