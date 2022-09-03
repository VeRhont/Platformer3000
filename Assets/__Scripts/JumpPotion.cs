using UnityEngine;

public class JumpPotion : MonoBehaviour
{
    [SerializeField] private float _addingJumpForce;
    [SerializeField] private float _duration;

    private ParticleSystem _jumpPotionParticles;
    private AudioSource _potionSound;

    private PlayerController _player;
    private float _startTime;

    private void Start()
    {
        _jumpPotionParticles = GameObject.FindGameObjectWithTag("JumpPotionParticles").GetComponent<ParticleSystem>();
        _potionSound = GameObject.Find("PotionSound").GetComponent<AudioSource>();
    }

    public void UseJumpPotion()
    {
        _potionSound.Play();

        _startTime = Time.time;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _player.IncreaseJumpForce(_addingJumpForce, _startTime + _duration);

        _jumpPotionParticles.transform.position = _player.transform.position;
        _jumpPotionParticles.Play();
        Destroy(gameObject);
    }
}