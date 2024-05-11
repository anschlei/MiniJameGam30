using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorldBuilderSystem : MonoBehaviour
{
    [SerializeField]
    List<GameObject> m_listOfGeneratedShips;

    [SerializeField]
    WorldBuilderValues m_worldMapValues;

    [SerializeField]
    GameObject m_ShipPrefeab;

    float GetRandomFloat()
    {
        return Random.Range(-m_worldMapValues.worldMapArea, m_worldMapValues.worldMapArea);
    }



    /* Mathe mage
     *   https://stackoverflow.com/questions/5837572/generate-a-random-point-within-a-circle-uniformly
     *   t = 2*pi*random()
     *   u = random()+random()
     *   r = if u>1 then 2-u else u
     *   [r*cos(t), r*sin(t)]
     */

    private float GetT()
    {
        return 2 * Mathf.PI * GetRandomFloat();
    }
    private float GetU()
    {
        return GetRandomFloat() + GetRandomFloat();
    }

    private float GetR()
    {
        float u = GetU();
        return u > 1 ? 2 - u : u;
    }

    private Vector3 CreateRandomVector3()
    {
        float r = GetR();
        float t = GetT();
        
        return new Vector3(r * Mathf.Cos(t), 0, r * Mathf.Sin(t));
    }



    //
    // generate random space ships around the base 
    //
    void Start()
    {
        for (int index = 0; index < m_worldMapValues.amountOfAvailableEscapeShips; ++index)
        {
            Vector3 spawnPosition = CreateRandomVector3();

            if (m_listOfGeneratedShips.Count == 0)
            {
                while (Vector3.Distance(new Vector3(), spawnPosition) < m_worldMapValues.baseArea)
                {
                    spawnPosition = CreateRandomVector3();
                }
                m_listOfGeneratedShips.Append(Instantiate(m_ShipPrefeab, spawnPosition, Quaternion.identity));
            }
            else
            {
                for (int shipListIndex = 0; shipListIndex < m_listOfGeneratedShips.Count; ++shipListIndex)
                {
                    while (
                        Vector3.Distance(m_listOfGeneratedShips.ElementAt(shipListIndex).transform.position, spawnPosition) < m_worldMapValues.minDistanceShipToShip
                        || Vector3.Distance(new Vector3(), spawnPosition) < m_worldMapValues.baseArea
                        )
                    {
                        spawnPosition = CreateRandomVector3();
                    }
                }

                m_listOfGeneratedShips.Append(Instantiate(m_ShipPrefeab, spawnPosition, Quaternion.identity));
            }
        }
    }
}
