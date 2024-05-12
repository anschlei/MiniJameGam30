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
    [SerializeField]
    GameObject _controls;

    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void Settings()
    {
        _settings.SetActive(true);
        _main.SetActive(false);
    }

    public void Credits()
    {
        _controls.SetActive(true);
        _main.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackFromSettings()
    {
        _settings.SetActive(false);
        _main.SetActive(true);
    }

    public void BackFromControls()
    {
        _controls.SetActive(false);
        _main.SetActive(true);
    }
}
