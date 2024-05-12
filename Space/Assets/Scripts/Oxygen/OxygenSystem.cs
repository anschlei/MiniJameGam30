using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    bool Consume(int _amountOfCrew = 1)
    {
        var tmpOxygen = (int)(m_oxygen - (m_consumedPerCrew * _amountOfCrew));
        
        if(tmpOxygen > 0)
        {
            m_oxygen = tmpOxygen;

            float tmpMaxOxygen = (m_amountOfAirBoxes * m_amountOfBoxAir);
            float delta = (m_oxygen) / tmpMaxOxygen;
            float newOxygenWidth = m_oiriginOxygenSpriteWidth * delta;

            // change color from state
            if ( (delta <= 0.5f && (m_oxygenState == (int)States.OK) || ( delta > 0.1f) && m_oxygenState == (int)States.CRIT)  )
            {
                m_Oxygen.color = new Color(0.8f, 0.81f, 0.29f);
                m_oxygenState = (int)States.WARNING;
            }
            else if( delta <= 0.1f && m_oxygenState == (int)States.WARNING)
            {
                m_Oxygen.color = new Color(0.48f, 0.10f, 0.04f);
                m_oxygenState = (int)States.CRIT;
            }
            
            m_Oxygen.transform.localScale = new Vector2(newOxygenWidth, m_Oxygen.transform.localScale.y);

            return true;
        }
        else
        {
            return false;
        }
    }

    void RefillOxygen(float _air)
    {
        var tmpMaxOxygen = (m_amountOfAirBoxes * m_amountOfBoxAir);

        var tmpNewOxygen = m_oxygen + _air;

        m_oxygen = tmpNewOxygen > tmpMaxOxygen ? tmpMaxOxygen : tmpNewOxygen;

        Debug.Log("OxygenSystem::RefillOxygen::" + m_oxygen);
    }

    private void Start()
    {
        m_Oxygen =  GameObject.FindGameObjectWithTag("GameUI").transform.GetChild(0).GetChild(1).GetComponent<Image>();
        if(m_Oxygen == null)
        {
            Debug.Log("Debug::Oxygen:: GameUI not found!");
        }
        else
        {
            m_oiriginOxygenSpriteWidth = m_Oxygen.transform.localScale.x;
            m_oxygen = (m_amountOfAirBoxes * m_amountOfBoxAir);
            m_oxygenState = (int)States.OK;
        }
    }


    [SerializeField]
    float m_oxygen;

    [SerializeField]
    float m_consumedPerCrew;

    [SerializeField]
    int m_amountOfAirBoxes;

    [SerializeField]
    float m_amountOfBoxAir;

    [SerializeField]
    Image m_Oxygen = null;
    float m_oiriginOxygenSpriteWidth;
    int m_oxygenState;

    enum States : int
    {
        OK,
        WARNING,
        CRIT
    }

    

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
