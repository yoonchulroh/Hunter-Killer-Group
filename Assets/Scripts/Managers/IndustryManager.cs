using System;
using UnityEngine;

public class IndustryManager : MonoBehaviour
{
    private double _industrialCapacity;
    public int industrialCapacity => Convert.ToInt32(Math.Round(_industrialCapacity, 0));

    private double _industrialCapacityGeneration;
    public double industrialCapacityGeneration => _industrialCapacityGeneration;

    void Start()
    {
        _industrialCapacity = 500;
        _industrialCapacityGeneration = 30;
    }

    void Update()
    {
        _industrialCapacity += _industrialCapacityGeneration * Time.deltaTime;
    }

    public bool UseIndustrialCapacity(int amount)
    {
        if (Convert.ToDouble(amount) > _industrialCapacity)
        {
            return false;
        }
        else 
        {
            _industrialCapacity -= Convert.ToDouble(amount);
            return true;
        }
    }
}
