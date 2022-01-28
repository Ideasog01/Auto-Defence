using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButtons : MonoBehaviour
{
    [SerializeField]
    private bool mainMenu;

    [SerializeField]
    private bool restart;

    private Button _thisButton;

    private SceneManagement _sceneManagement;

    private void Awake()
    {
        _thisButton = this.GetComponent<Button>();
        _sceneManagement = GameObject.Find("GameManager").GetComponent<SceneManagement>();

        if(mainMenu)
        {
            _thisButton.onClick.AddListener(ReturnToMenu);
        }

        if(restart)
        {
            _thisButton.onClick.AddListener(RestartGame);
        }
    }

    private void ReturnToMenu()
    {
        _sceneManagement.ReturnMainMenu();
    }

    private void RestartGame()
    {
        _sceneManagement.RestartScene();
    }
}
