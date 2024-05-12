using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject _settings;
    [SerializeField]
    GameObject _main;

    public void Play()
    {
        //DEBUG::TEST
        SceneManager.LoadScene(3, LoadSceneMode.Single);
        //SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void Settings()
    {
        _settings.SetActive(true);
        _main.SetActive(false);
    }

    public void Credits()
    {
        // todo
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        _settings.SetActive(false);
        _main.SetActive(true);
    }
}
