using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform _spawnPosition;

    public void SpawnEnemy(Enemy enemy, GameMode gameMode)
    {
        BaseEnemyController enemyController = Instantiate(enemy.enemyPrefab.GetComponent<BaseEnemyController>(), _spawnPosition.transform.position, enemy.enemyPrefab.transform.rotation);
        enemyController.SetProperties(enemy, gameMode);
    }

    private void Awake()
    {
        _spawnPosition = this.transform.GetChild(0);
    }
}
