using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyController : MonoBehaviour
{
    [SerializeField]
    private int enemyMaxHealth;

    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private int enemyIndex;

    [SerializeField]
    private float turnSpeed;

    private float _turnVelocity;

    private int _enemyHealth;

    private NavMeshAgent _navMeshAgent;

    private GameObject _playerObject;

    private bool _playerVisible;

    public void SetProperties(Enemy enemy)
    {
        enemyMaxHealth = enemy.enemyMaxHealth;
        _enemyHealth = enemyMaxHealth;
    }

    public void DamageEnemy(int amount)
    {
        _enemyHealth -= amount;

        if(_enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject GetPlayer()
    {
        return _playerObject;
    }

    public void AssignPlayer(GameObject player)
    {
        _playerObject = player;

        if(player == null)
        {
            _playerVisible = false;
            CancelInvoke("StartAttack");
        }
        else
        {
            InvokeRepeating("StartAttack", 2, attackCooldown);
            _playerVisible = true;
        }
    }

    private void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(_playerObject != null)
        {
            SetEnemyDestination(_playerObject.transform.position);
        }


    }

    private void Rotation()
    {
        if(_playerObject == null)
        {
            _navMeshAgent.updateRotation = true;
        }
        else
        {
            _navMeshAgent.updateRotation = false;

            float targetAngle = Mathf.Atan2(_playerObject.transform.position.x, _playerObject.transform.position.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnVelocity, turnSpeed);

            this.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void SetEnemyDestination(Vector3 enemyDestination)
    {
        _navMeshAgent.SetDestination(enemyDestination);
    }

    private void StartAttack()
    {
        if (enemyIndex == 1)
        {
            this.GetComponent<ScoutEnemyController>().PrimaryFire();
        }
    }
}
