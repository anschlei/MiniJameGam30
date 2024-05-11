using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}
