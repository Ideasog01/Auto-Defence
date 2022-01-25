using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private bool _isPlayerProjectile;

    private float _projectileSpeed;

    private int _projectileDamage;

    public void SetupProjectile(float speed, int damage, bool isPlayer)
    {
        _projectileSpeed = speed;
        _projectileDamage = damage;
        _isPlayerProjectile = isPlayer;

        Destroy(this.gameObject, 4);
    }

    void Update()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * _projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isPlayerProjectile)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<BaseEnemyController>().DamageEnemy(_projectileDamage);
                Debug.Log("Projectile Damaged Enemy");
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerController>().DamagePlayer(_projectileDamage);
                Debug.Log("Projectile Damaged Player");
            }
        }
        

        if(other.gameObject.tag != "Blaster" && other.gameObject.tag != "Trigger")
        {
            Debug.Log("Projectile Destroyed");
            Destroy(this.gameObject);
        }      
    }
}
