using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menuCanvasArray;

    [SerializeField]
    private TextMeshProUGUI[] playerStatusTextArray;

    [SerializeField]
    private Image[] playerStatusIcon;

    private void Start()
    {
        LoadMainMenu();
    }

    public void DisplayPlayer(int index, string status)
    {
        playerStatusTextArray[index].text = status;
    }

    public void LoadSingleGameOptions()
    {
        menuCanvasArray[2].SetActive(false);
        menuCanvasArray[5].SetActive(true);
    }

    public void StartLocalGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void StartSingleGame()
    {
        SceneManager.LoadScene("GameScene_Single");
    }

    public void LoadGameSettings()
    {
        menuCanvasArray[0].SetActive(false);
        menuCanvasArray[2].SetActive(true);
    }

    public void LoadMainMenu()
    {
        menuCanvasArray[0].SetActive(true);

        for(int i = 1; i < menuCanvasArray.Length; i++)
        {
            menuCanvasArray[i].SetActive(false);
        }

        DeviceManager.joinScreenActive = false;
    }

    public void LoadLocalGameOptions()
    {
        menuCanvasArray[2].SetActive(false);
        menuCanvasArray[3].SetActive(true);

        DeviceManager.joinScreenActive = true;
    }

    public void LoadNetworkGameOptions()
    {
        menuCanvasArray[2].SetActive(false);
        menuCanvasArray[4].SetActive(true);
    }

    public void DisplayOptionsMenu(bool isActive)
    {
        if(isActive)
        {
            menuCanvasArray[0].SetActive(false);
            menuCanvasArray[1].SetActive(true);
        }
        else
        {
            menuCanvasArray[0].SetActive(true);
            menuCanvasArray[1].SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
