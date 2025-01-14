using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public bool Consume(int _amountOfCrew = 1)
    {
        var tmpOxygen = (int)(m_oxygen) - (m_consumedPerCrew * _amountOfCrew);

        if (tmpOxygen > 0)
        {
            m_oxygen = tmpOxygen;

            float tmpMaxOxygen = (m_amountOfAirBoxes * m_amountOfBoxAir);
            float delta = (m_oxygen) / tmpMaxOxygen;
            float newOxygenWidth = m_oiriginOxygenSpriteWidth * delta;

            DecreaseBalloonOxygen(delta);

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
            if(m_Oxygen.transform.localScale.x != 0)
            {
                m_Oxygen.transform.localScale = new Vector2(0, m_Oxygen.transform.localScale.y);
            }
            SceneManager.LoadScene(4, LoadSceneMode.Single);
            
            return false;
        }
    }

    public void RefillOxygen(float _air)
    {
        float tmpMaxOxygen = (m_amountOfAirBoxes * m_amountOfBoxAir);

        float tmpNewOxygen = m_oxygen + _air;

        float delta = 1 - ( (tmpMaxOxygen - m_oxygen) / tmpMaxOxygen);

        if (tmpNewOxygen > tmpMaxOxygen)
        {
            m_oxygen = tmpMaxOxygen;
            IncreaseBalloonOxygen(1);//%;
        }
        else
        {
            m_oxygen = tmpNewOxygen;
            IncreaseBalloonOxygen( delta );
        }

        float newOxygenWidth = m_oiriginOxygenSpriteWidth * delta;

        // change color from state
        if ((delta >= 0.5f && (m_oxygenState == (int)States.WARNING) || (delta > 0.1f) && m_oxygenState == (int)States.CRIT))
        {
            m_Oxygen.color = new Color(0.45f, 0.82f, 1.0f);
            m_oxygenState = (int)States.OK;
        }
        else if (delta >= 0.1f && m_oxygenState == (int)States.CRIT)
        {
            m_Oxygen.color = new Color(0.8f, 0.81f, 0.29f);
            m_oxygenState = (int)States.WARNING;
        }

        m_Oxygen.transform.localScale = new Vector2(newOxygenWidth, m_Oxygen.transform.localScale.y);


        Debug.Log("OxygenSystem::RefillOxygen::" + m_oxygen);
    }

    private void Start()
    {        
        m_Oxygen = GameObject.FindGameObjectWithTag("GameUI").transform.GetChild(0).GetChild(1).GetComponent<Image>();

        if (m_Oxygen == null)
        {
            Debug.Log("Debug::Oxygen:: GameUI not found!");
        }
        else
        {
            m_oiriginOxygenSpriteWidth = m_Oxygen.transform.localScale.x;
            m_oxygen = (m_amountOfAirBoxes * m_amountOfBoxAir);
            m_oxygenState = (int)States.OK;
            m_originalBalloonSize = m_Ballons.ElementAt(0).transform.localScale;
        }
    }

    
    // 80 == 100%
    private void DecreaseBalloonOxygen(float _percent)
    {
        foreach (GameObject ballon in m_Ballons)
        {
            if(ballon != null)
            {
                ballon.transform.GetChild(0).transform.localScale = new Vector3(20+(80*_percent), 20+(80 * _percent), 100);
            }
        }
    }

    
    private void IncreaseBalloonOxygen(float _percent)
    {
        if(_percent > 0)
        {
            foreach (GameObject ballon in m_Ballons)
            {
                if(ballon != null)
                {
                    ballon.transform.GetChild(0).transform.localScale = new Vector3( transform.localScale.x + 100*_percent, transform.localScale.y + 100 * _percent , 100 );
                }
            }
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

    [SerializeField]
    List<GameObject> m_Ballons = new List<GameObject> { null, null, null, null};
    Vector3 m_originalBalloonSize;

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
            this.Consume(10);
        }
    }

}
