using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void Settings()
    {
        // todo 
    }

    public void Creddits()
    {
        // todo
    }
}
