using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GestioneScene : MonoBehaviour
{
    public static GestioneScene Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progresbarr;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            _progresbarr.fillAmount = scene.progress;

        } while (scene.progress <.9f);

        _loaderCanvas.SetActive(false);
        scene.allowSceneActivation = false;


    }
}
