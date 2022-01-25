using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField]
    private Slider playerHealthSlider;

    public void DisplayPlayerHealth(int maxHealth, int health)
    {
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = health;
    }
}
