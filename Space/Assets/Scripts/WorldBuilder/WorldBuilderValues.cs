using UnityEngine;


[CreateAssetMenu(fileName = "WorldMapValues", menuName = "ScriptableObjects/WorldBuilderValues", order = 1)]
public class WorldBuilderValues : ScriptableObject
{
    public float minDistanceShipToShip = 100f;
    public float baseArea = 200f;
    public float worldMapArea = 2000f;

    public int amountOfAvailableEscapeShips = 25;

}
