using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> playerArray = new List<GameObject>();

    [SerializeField]
    private GameObject playerPrefab;

    private SpawnManager _spawnManager;

    private int playersDefeated;

    private void Awake()
    {
        _spawnManager = this.GetComponent<SpawnManager>();
    }

    public void PlayerJoined(GameObject player)
    {
        playerArray.Add(player);
        _spawnManager.AddPlayerInterface(player.transform.GetChild(1).GetComponent<PlayerInterface>());
    }

    public void PlayerDefeated()
    {
        playersDefeated++;

        Debug.Log("Players Defeated = " + playersDefeated);

        if(playersDefeated >= playerArray.Count)
        {
            _spawnManager.StopGame();

            foreach(PlayerInterface player in _spawnManager.playerInterfaceArray)
            {
                player.DisplayConditionMessage(1);
            }
            
        }
    }

    public void PlayerRevived()
    {
        playersDefeated--;
    }
}
