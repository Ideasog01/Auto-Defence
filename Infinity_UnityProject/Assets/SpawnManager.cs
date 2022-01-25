using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    private GameMode gameMode;

    private Wave currentWave;

    [SerializeField]
    private Animator countdownAnimator;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    public void StartGameMode()
    {
        gameMode.gameInProgress = true;
        gameMode.waveIndex = 0;
        
        for(int i = 0; i < gameMode.gameModeWaves.Length; i++)
        {
            gameMode.gameModeWaves[i].enemyIndex = 0;
        }

        if(gameMode.skipCountdown)
        {
            StartWave();
        }
        else
        {
            StartCoroutine(GameModeCountDown());
        } 
    }

    private IEnumerator GameModeCountDown()
    {
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(true);
        countdownAnimator.SetBool("active", true);
        countdownText.text = "5";
        yield return new WaitForSeconds(1.5f);
        countdownText.text = "4";
        yield return new WaitForSeconds(1.5f);
        countdownText.text = "3";
        yield return new WaitForSeconds(1.5f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1.5f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1.5f);
        countdownText.text = "Defend!";
        countdownAnimator.SetBool("active", false);
        yield return new WaitForSeconds(1.5f);
        countdownText.gameObject.SetActive(false);
        StartWave();
    }

    private void StartWave()
    {
        if(gameMode.waveIndex < gameMode.gameModeWaves.Length)
        {
            currentWave = gameMode.gameModeWaves[gameMode.waveIndex];
            Debug.Log("New Wave!");
            SpawnEnemy();

        }
        else
        {
            Debug.Log("Game Mode Complete!");
        }
    }

    private void SpawnEnemy()
    {
        if(currentWave != null)
        {
            if(currentWave.enemyIndex < currentWave.enemyType.Length)
            {
                Enemy enemy = currentWave.enemyType[currentWave.enemyIndex];
                StartCoroutine(SpawnWait(enemy));
                currentWave.enemyIndex++;
                Debug.Log("Waiting for Enemy Spawn...");
            }
            else
            {
                gameMode.waveIndex++;
                currentWave = null;
                StartWave();
            }
        }
        else
        {
            Debug.LogError("Wave is null!");
        }
    }

    private void Awake()
    {
        gameMode = GameObject.Find("GameModeSettings").GetComponent<GameMode>();
    }

    private void Start()
    {
        if(gameMode != null)
        {
            if(gameMode.startGameMode)
            {
                StartGameMode();
            }
        }
        else
        {
            Debug.LogError("Gamemode is null!");
        }
    }

    private IEnumerator SpawnWait(Enemy enemyType)
    {
        yield return new WaitForSeconds(enemyType.spawnTime);
        EnemySpawner spawner = enemyType.spawner;

        if(spawner != null)
        {
            spawner.SpawnEnemy(enemyType);
            Debug.Log("Enemy Spawned!");
        }
        else
        {
            Debug.LogError("Spawner is null!");
        }

        //Repeat
        SpawnEnemy();
    }
}
