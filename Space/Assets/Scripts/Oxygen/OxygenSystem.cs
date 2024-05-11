using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OxygenSystem : MonoBehaviour
{
    OxygenSystem(float _airConsumedPerCrew = 1.0f, int amountOfAirBoxes = 1, float amountOfBoxAir = 100.0f)
    {
        m_oxygen = (amountOfAirBoxes * amountOfBoxAir);
        m_consumedPerCrew = _airConsumedPerCrew;
        m_amountOfAirBoxes = amountOfAirBoxes;
        m_amountOfBoxAir = amountOfBoxAir;
    }

    float GetFill()
    {
        return m_oxygen;
    }

    // TODO -> get bool maybe =? ask for a bool
    void Consume(int _amountOfCrew = 1)
    {
        var tmpOxygen = (int)(m_oxygen - (m_consumedPerCrew * _amountOfCrew));
        m_oxygen = tmpOxygen < 0 ? 0 : tmpOxygen;

        Debug.Log("OxygenSystem::Consumed::" + m_oxygen);
    }

    void RefillOxygen(float _air)
    {
        var tmpMaxOxygen = (m_amountOfAirBoxes * m_amountOfBoxAir);

        var tmpNewOxygen = m_oxygen + _air;

        m_oxygen = tmpNewOxygen > tmpMaxOxygen ? tmpMaxOxygen : tmpNewOxygen;

        Debug.Log("OxygenSystem::RefillOxygen::" + m_oxygen);
    }




    [SerializeField]
    float m_oxygen;

    [SerializeField]
    float m_consumedPerCrew;

    [SerializeField]
    int m_amountOfAirBoxes;

    [SerializeField]
    float m_amountOfBoxAir;



    //
    // DEBUG::TEST 
    //
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Debug::Test::Presed+");
            this.RefillOxygen(10f);
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Debug::Test::Presed-");
            this.Consume();
        }
    }

}
