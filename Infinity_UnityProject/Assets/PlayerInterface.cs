using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField]
    private Slider playerHealthSlider;

    [SerializeField]
    private TextMeshProUGUI enemyCountText;

    [SerializeField]
    private TextMeshProUGUI waveCountText;

    [SerializeField]
    private GameObject[] conditionMessages;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    public TextMeshProUGUI GetCountdownText()
    {
        return countdownText;
    }

    public void DisplayPlayerHealth(int maxHealth, int health)
    {
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = health;
    }

    public void UpdateWaveText(int waveNumber)
    {
        waveCountText.text = "Wave: " + waveNumber;
    }

    public void UpdateEnemyText(int enemyCount)
    {
        enemyCountText.text = "Enemies Remaining: " + enemyCount;
    }

    public void DisplayConditionMessage(int index)
    {
        conditionMessages[index].SetActive(true);

        if(index < 2)
        {
            conditionMessages[index].transform.GetChild(1).GetChild(0).GetComponent<Button>().Select();
        }
        

        if(index == 2)
        {
            Invoke("RemoveDisplayMessage", 7);
        }
    }

    private void RemoveDisplayMessage()
    {
        for(int i = 0; i < conditionMessages.Length; i++)
        {
            conditionMessages[i].SetActive(false);
        }
    }
}
