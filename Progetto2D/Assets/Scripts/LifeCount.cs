using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeCount : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaning;

    public void LoseLife()
    {
        if (livesRemaning == 3)
            return;
        livesRemaning--;
        lives[livesRemaning].enabled = false;
        if (livesRemaning == 3)
        {
            Debug.Log("HAI FINITO LE VITE, FROCIO !");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
            LoseLife();
    }

}
