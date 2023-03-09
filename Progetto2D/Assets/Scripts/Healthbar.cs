using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image fillBar;
    public float health;

    public void LoseHealth(int value)
    {
        if (health <= 0)
            return;
        health -= value;
        fillBar.fillAmount = health / 100;
        if(health <= 0)
        {
            Debug.Log("RICCHIONE !!");
            LoseLife();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            LoseHealth(25);
    }
}
