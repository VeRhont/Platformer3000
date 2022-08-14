using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JumpPotion : MonoBehaviour
{
    [SerializeField] private float _addingJumpForce;
    [SerializeField] private float _duration;

    private PlayerController _player;
    private float _startTime;

    public void UseJumpPotion()
    {
        _startTime = Time.time;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _player.IncreaseJumpForce(_addingJumpForce, _startTime + _duration);

        Destroy(gameObject);
    }
}