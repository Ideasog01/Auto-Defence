using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutEnemyController : BaseEnemyController
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private Transform projectileSpawn1;

    [SerializeField]
    private Transform projectileSpawn2;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private int projectileDamage;


    public void PrimaryFire()
    {
        GameObject projectile1 = Instantiate(projectilePrefab, projectileSpawn1.position, this.transform.rotation);
        projectile1.GetComponent<ProjectileMovement>().SetupProjectile(projectileSpeed, projectileDamage, false);

        GameObject projectile2 = Instantiate(projectilePrefab, projectileSpawn2.position, this.transform.rotation);
        projectile2.GetComponent<ProjectileMovement>().SetupProjectile(projectileSpeed, projectileDamage, false);
    }
}
