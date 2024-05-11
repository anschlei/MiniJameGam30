using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCargo : MonoBehaviour
{
    [Serializable]
    public struct Cargo
    {
        public int Persons;
        public int Material;
    }

    [SerializeField]
    Cargo _cargo;
    [SerializeField]
    GameObject _interior;

    public void Scan(ShipCargo otherShipCargo)
    {
        otherShipCargo.DisplayInterior();
    }

    public void LootShip(ShipCargo otherShipCargo)
    {
        Cargo otherCargo = otherShipCargo.GetCargo();
        Debug.Log("The other ship has: " + otherCargo.Persons + " persons and: " + otherCargo.Material + " materials on board!");

        _cargo.Persons += otherCargo.Persons;
        _cargo.Material += otherCargo.Material;

        Debug.Log("You now have: " + _cargo.Persons + " persons and: " + _cargo.Material + " materials on board!");

        otherCargo.Persons = 0;
        otherCargo.Material = 0;
    }

    public void UnloadPersons()
    {
        Debug.Log("You unloaded " + _cargo.Persons + " persons at the base");
        _cargo.Persons = 0;
    }

    public void Upgrade()
    {
        Debug.Log("You upgraded your cargo and can hold more persons and/or materials!");
    }

    public void DisplayInterior()
    {
        _interior?.SetActive(true);
    }

    public Cargo GetCargo()
    {
        return _cargo;
    }
}
