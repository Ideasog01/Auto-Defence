using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameMode gameMode;

    private Wave currentWave;

    private int enemyCount;

    [SerializeField]
    private PlayerInterface playerInterface;

    [SerializeField]
    private Animator countdownAnimator;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    public void StopGame()
    {
        gameMode.gameInProgress = false;
    }

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

    public void EnemyDefeated()
    {
        enemyCount--;

        playerInterface.UpdateEnemyText(enemyCount);


        if (enemyCount == 0)
        {
            gameMode.waveIndex++;
            currentWave = null;

            if (gameMode.waveIndex < gameMode.gameModeWaves.Length)
            {
                playerInterface.DisplayConditionMessage(2);
                Invoke("StartWave", 7);
            }
            else
            {
                Debug.Log("Game Mode Complete!");
                StopGame();
                playerInterface.DisplayConditionMessage(0);
            }
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

            playerInterface.UpdateWaveText(gameMode.waveIndex + 1);
            enemyCount = gameMode.gameModeWaves[gameMode.waveIndex].enemyType.Length;
            playerInterface.UpdateEnemyText(enemyCount);

            SpawnEnemy();

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
                Debug.Log("All enemies spawned");
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
            spawner.SpawnEnemy(enemyType, gameMode);
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
