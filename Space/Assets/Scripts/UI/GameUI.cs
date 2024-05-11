using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject Notification;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            Notification.SetActive(!Notification.activeSelf);
        }
    }
}
