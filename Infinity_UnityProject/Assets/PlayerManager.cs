using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private GameObject playerObject;

    private GameManager _gameManager;

    private int playerCount;

    private bool _gameSceneLoaded;

    private bool _once;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        SceneManager.sceneLoaded += GameSceneLoaded;
    }

    public void PlayerJoin()
    {
        if(!_gameSceneLoaded && playerCount < 4)
        playerCount++;
    }

    public void PlayerLeave()
    {
        if(!_gameSceneLoaded && playerCount > 0)
        playerCount--;
    }

    public void GameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameScene_Single" && !_once)
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _gameSceneLoaded = true;

            Debug.Log("SceneLoaded");
            Debug.Log(playerCount);

            _once = true;
        }
        
    }


}
