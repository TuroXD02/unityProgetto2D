using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{
    [Header("Lives")]
    public Image[] lives;
    public int livesRemaning;

    [Header("Health bar")]
    public Image fillBar;
    public float health;

    public void LoseLife()
    {
        if (livesRemaning == 0)
            return;
        livesRemaning--;
        lives[livesRemaning].enabled = false;
        if (livesRemaning == 0)
        {
            Debug.Log("HAI FINITO LE VITE, FROCIO !");
        }
    }

    public void LoseHealth(int value)
    {
        if (health <= 0)
            return;
        health -= value;
        fillBar.fillAmount = health / 100;
        if (health <= 0)
        {
            Debug.Log("RICCHIONE !!");
            LoseLife();
            health = 100;
            fillBar.fillAmount = health;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            LoseHealth(25);
    }

}
