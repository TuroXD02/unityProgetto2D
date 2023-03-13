using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    Vector3 playerInitPosition;

    void Start()
    {
        playerInitPosition = FindObjectOfType<Player>().transform.position;
    }

    public void Restart()
    {
        Debug.Log("Hai finito le vite, RICCHIONE !!!");

        FindObjectOfType<Player>().transform.position = playerInitPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
