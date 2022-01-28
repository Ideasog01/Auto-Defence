using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameMode gameMode;

    private Wave currentWave;

    public List<PlayerInterface> playerInterfaceArray = new List<PlayerInterface>();

    private int enemyCount;

    private GameManager _gameManager;

    private bool _gameModeStarted;

    public void AddPlayerInterface(PlayerInterface playerInterface)
    {
        playerInterfaceArray.Add(playerInterface);
    }

    private void Update()
    {
        if(gameMode != null)
        {
            if (gameMode.startGameMode && _gameManager.playerArray.Count > 0 && !_gameModeStarted)
            {
                StartGameMode();
                _gameModeStarted = true;
            }
        }
        else
        {
            Debug.Log("Game Mode = Null");
        }
        
    }

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

        foreach(PlayerInterface player in playerInterfaceArray)
        {
            player.UpdateEnemyText(enemyCount);
        }


        if (enemyCount == 0)
        {
            gameMode.waveIndex++;
            currentWave = null;

            if (gameMode.waveIndex < gameMode.gameModeWaves.Length)
            {
                foreach (PlayerInterface player in playerInterfaceArray)
                {
                    player.DisplayConditionMessage(2);
                }
                Invoke("StartWave", 7);
            }
            else
            {
                Debug.Log("Game Mode Complete!");
                StopGame();

                foreach (PlayerInterface player in playerInterfaceArray)
                {
                    player.DisplayConditionMessage(0);
                }
            }
        }
    }

    private IEnumerator GameModeCountDown()
    {
        yield return new WaitForSeconds(1);

        foreach(PlayerInterface player in playerInterfaceArray)
        {
            player.GetCountdownText().gameObject.SetActive(true);
            player.GetCountdownText().text = "5";
            player.GetCountdownText().GetComponent<Animator>().SetBool("active", true);
        }

        yield return new WaitForSeconds(1.5f);

        foreach (PlayerInterface player in playerInterfaceArray)
        {
            player.GetCountdownText().text = "4";
        }
        yield return new WaitForSeconds(1.5f);

        foreach (PlayerInterface player in playerInterfaceArray)
        {
            player.GetCountdownText().text = "3";
        }

        yield return new WaitForSeconds(1.5f);

        foreach (PlayerInterface player in playerInterfaceArray)
        {
            player.GetCountdownText().text = "2";
        }

        yield return new WaitForSeconds(1.5f);

        foreach (PlayerInterface player in playerInterfaceArray)
        {
            player.GetCountdownText().text = "1";
        }

        yield return new WaitForSeconds(1.5f);

        foreach (PlayerInterface player in playerInterfaceArray)
        {
            player.GetCountdownText().text = "Defend!";
            player.GetCountdownText().GetComponent<Animator>().SetBool("active", false);
        }

        yield return new WaitForSeconds(1.5f);

        foreach(PlayerInterface player in playerInterfaceArray)
        {
            player.GetCountdownText().gameObject.SetActive(false);
        }

        StartWave();
    }

    private void StartWave()
    {
        if(gameMode.waveIndex < gameMode.gameModeWaves.Length)
        {
            currentWave = gameMode.gameModeWaves[gameMode.waveIndex];
            Debug.Log("New Wave!");

            enemyCount = gameMode.gameModeWaves[gameMode.waveIndex].enemyType.Length;

            foreach (PlayerInterface player in playerInterfaceArray)
            {
                player.UpdateWaveText(gameMode.waveIndex + 1);
                player.UpdateEnemyText(enemyCount);
            }

            

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
        _gameManager = this.GetComponent<GameManager>();
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
