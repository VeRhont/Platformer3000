using UnityEngine;

public class ShootingPlant : Enemy
{
    [SerializeField] private float _startTime = 0;
    [SerializeField] private float _timeBetweenAttack = 5;

    [SerializeField] private GameObject[] _projectilesPrefabs;
    [SerializeField] private Transform[] _shootingPositions;

    protected override void Start()
    {
        base.Start();

        InvokeRepeating("Shoot", _startTime, _timeBetweenAttack);
    }

    public override void Die()
    {
        base.Die();

        _enemyAnimator.SetBool("IsDead", true);

        Destroy(gameObject, 1);
        Destroy(this);

        Destroy(_healthBar.gameObject);
    }

    private void Shoot()
    {
        _enemyAnimator.SetTrigger("Attack");

        foreach (var shootingPosition in _shootingPositions)
        {
            var projectile = _projectilesPrefabs[Random.Range(0, _projectilesPrefabs.Length)];
            projectile.transform.rotation = shootingPosition.rotation;

            Instantiate(projectile, shootingPosition.position, shootingPosition.transform.rotation);        
        }
    }
}
