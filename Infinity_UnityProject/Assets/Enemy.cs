using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;

    public int enemyMaxHealth;

    public int enemyDamageModifier = 1;

    public int enemyLevel;

    public bool hasShield;

    public EnemySpawner spawner;

    public int spawnTime;
}
