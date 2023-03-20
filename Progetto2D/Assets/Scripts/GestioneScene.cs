using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class GestioneScene : MonoBehaviour
{
    public static GestioneScene Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progresbarr;
    float target;

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
        _progresbarr.fillAmount = 0;
        target= 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);

            target = scene.progress;

        } while (scene.progress <.9f);

        await Task.Delay(1000);


        _loaderCanvas.SetActive(false);
        scene.allowSceneActivation = true;


    }

    private void Update()
    {
        _progresbarr.fillAmount = Mathf.MoveTowards(_progresbarr.fillAmount, target, 3 * Time.deltaTime);
    }
}
