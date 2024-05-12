using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewQuarter : MonoBehaviour
{
    [SerializeField]
    GameObject _crewPrefab;

    [SerializeField]
    ShipCargo _cargo;

    [SerializeField]
    float _crewCooldown;

    public float _remainingCrewCooldown;

    public List<CrewBehaviour> _crew;
    public List<CrewBehaviour> _availableCrew;

    bool _isLoading;

    ShipCargo _targetShip;
    ShipCargo.Cargo _targetCargo;

    void Start()
    {
        _crew = new List<CrewBehaviour>();
        _availableCrew = new List<CrewBehaviour>();

        for (int i = 0; i < 3; i++)
        {
            GameObject crew = Instantiate(_crewPrefab, this.transform);
            CrewBehaviour behaviour = crew.GetComponent<CrewBehaviour>();
            behaviour.SetHomeQuarter(this);
            behaviour.SetHomeCargo(_cargo);
            _crew.Add(behaviour);
            crew.SetActive(false);
        }
    }

    public bool GetIsLoading()
    {
        return _isLoading;
    }

    public void SendCrew(ShipCargo otherShipCargo)
    {
        _targetShip = otherShipCargo;
        _targetCargo = _targetShip.GetCargo();

        foreach (var crewMember in _crew)
        {
            _availableCrew.Add(crewMember);
        }

        _isLoading = true;
    }

    public void AddAvailableCrewMember(CrewBehaviour crew)
    {
        _availableCrew.Add(crew);
        crew.gameObject.SetActive(false);
        crew.transform.localPosition = new Vector3();
    }

    void Update()
    {
        if (_isLoading)
        {
            _remainingCrewCooldown -= Time.deltaTime;

            if ((_targetCargo.Persons > 0 || _targetCargo.Material > 0) && _remainingCrewCooldown <= 0.0f && _availableCrew.Count > 0)
            {
                _remainingCrewCooldown = _crewCooldown;

                _availableCrew[0].gameObject.SetActive(true);

                if (_targetCargo.Persons > 0)
                {
                    _availableCrew[0].Gather(CrewBehaviour.Loot.Person, _targetShip.transform);
                    _targetCargo.Persons--;
                }
                else
                {
                    _availableCrew[0].Gather(CrewBehaviour.Loot.Material, _targetShip.transform);
                    _targetCargo.Material--;
                }
                
                _availableCrew.RemoveAt(0);
                //send crew member to the other ship
            }
            else
            {
                if (_availableCrew.Count == _crew.Count)
                {
                    //set to false once all crew members are back
                    _isLoading = false;
                    _availableCrew.Clear();
                }
            }
        }
    }
}
