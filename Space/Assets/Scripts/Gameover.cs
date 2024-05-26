using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    [SerializeField]
    TMP_Text Textfield;

    private void Start()
    {
        Textfield.text = $"You have successfully rescued {AudioSystem.rescuedPersons} persons!"; 
    }
}
