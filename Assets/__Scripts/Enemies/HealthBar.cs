using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Vector3 _offset = new Vector3(0, 1, 0);

    public float MaxHealth;

    private void Update()
    {
        _healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offset);
    }

    public void SetHealth(float health)
    {
        _healthBar.gameObject.SetActive(health < MaxHealth);
        _healthBar.value = health;
        _healthBar.maxValue = health;
    }
}
