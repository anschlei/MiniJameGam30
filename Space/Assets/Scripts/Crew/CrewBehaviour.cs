using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewBehaviour : MonoBehaviour
{
    public enum Loot
    {
        Person = 0,
        Material = 1,
        None = 10
    }

    [SerializeField]
    float _maxSpeed;
    [SerializeField]
    float _maxAcceleration;

    private bool _wasAtTarget = false;

    CrewQuarter _homeQuarter;
    ShipCargo _cargo;
    public Loot _currentLoot = Loot.None;

    public Transform _targetLocation;

    Vector3 _velocity;
    
    Vector3 direction;

    public void SetHomeQuarter(CrewQuarter quarter)
    {
        _homeQuarter = quarter;
    }

    public void SetHomeCargo(ShipCargo cargo)
    {
        _cargo = cargo;
    }

    public void Gather(Loot loot, Transform target)
    {
        _currentLoot = loot;
        //move towards the target ship
        _targetLocation = target;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _wasAtTarget && !other.isTrigger)
        {
            ReturnToQuarter();
            _wasAtTarget = false;
            _velocity = new Vector3();
        }

        if (other.tag == "Ship")
        {
            _wasAtTarget = true;
            ReturnToShip();
        }
    }

    void ReturnToShip()
    {
        //TODO: spawn material or person prefab?
        //move towards player ship
        _targetLocation = _homeQuarter.transform;
    }

    void ReturnToQuarter()
    {
        if (_currentLoot == Loot.Person)
        {
            _cargo.AddPerson();
            _homeQuarter.AddAvailableCrewMember(this);
            _currentLoot = Loot.None;
        }

        if (_currentLoot == Loot.Material)
        {
            _cargo.AddMaterial();
            _homeQuarter.AddAvailableCrewMember(this);
            _currentLoot = Loot.None;
        }
    }

    void Update()
    {
        direction = (_targetLocation.position - transform.position);
        direction.Normalize();

        Vector3 desiredVelocity = direction * _maxSpeed;

        float maxSpeedChange = _maxAcceleration * Time.deltaTime;

        _velocity.x = Mathf.MoveTowards(_velocity.x, desiredVelocity.x, maxSpeedChange);
        _velocity.z = Mathf.MoveTowards(_velocity.z, desiredVelocity.z, maxSpeedChange);

        Vector3 displacement = _velocity * Time.deltaTime;

        transform.position += displacement;
    }
}
