using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = this.GetComponent<GameManager>();
    }


    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        foreach(GameObject controller in _gameManager.playerArray)
        {
            controller.transform.GetChild(1).GetComponent<PlayerController>().RevivePlayer();
        }
    }
}
