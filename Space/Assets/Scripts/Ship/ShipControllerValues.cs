using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShipControllerValues", order = 1)]
public class ShipControllerValues : ScriptableObject
{
    [Range(0f, 100f)]
    public float maxSpeed = 10f;

    [Range(0f, 100f)]
    public float maxAcceleration = 10f;

    [Range(0f, 100f)]
    public float maxTurnSpeed = 0.5f;

    [Range(0f, 1f)]
    public float bounciness = 0.5f;
    
    public Rect allowedArea = new Rect(-50f, -50f, 100f, 100f);
}