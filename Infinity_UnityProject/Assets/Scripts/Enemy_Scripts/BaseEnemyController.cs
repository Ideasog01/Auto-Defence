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

    private GameMode currentGameMode;

    public void SetProperties(Enemy enemy, GameMode gameMode)
    {
        currentGameMode = gameMode;
        enemyMaxHealth = enemy.enemyMaxHealth;
        _enemyHealth = enemyMaxHealth;
    }

    public void DamageEnemy(int amount)
    {
        _enemyHealth -= amount;

        if(_enemyHealth <= 0)
        {
            GameObject.Find("GameManager").GetComponent<SpawnManager>().EnemyDefeated();
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
        _navMeshAgent.updateRotation = false;
    }

    private void Update()
    {
        if(_playerObject != null && currentGameMode.gameInProgress)
        {
            SetEnemyDestination(_playerObject.transform.position);
            Rotation();

            if(_playerObject.gameObject.tag != "Player")
            {
                AssignPlayer(null);
            }
        }
    }

    private void Rotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_playerObject.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, Time.deltaTime * 800);
    }

    private void SetEnemyDestination(Vector3 enemyDestination)
    {
        _navMeshAgent.SetDestination(enemyDestination);
    }

    private void StartAttack()
    {
        if(currentGameMode.gameInProgress)
        {
            if (enemyIndex == 1)
            {
                this.GetComponent<ScoutEnemyController>().PrimaryFire();
            }
        }
    }
}
