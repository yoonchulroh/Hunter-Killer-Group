using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirfieldBehaviour : StationaryEntityBehaviour
{
    [SerializeField] private GameObject _aircraftPrefab;
    [SerializeField] private GameObject _airfieldFlyingRangePrefab;

    private List<GameObject> _aircraftList = new List<GameObject>();
    private GameObject _airfieldFlyingRange;

    private void SpawnAircraft(float flyingRange, float attackRange, int attack, float attackPeriod)
    {
        if (GameManager.Instance.industryManager.UseIndustrialCapacity(200))
        {
            var aircraft = Instantiate<GameObject>(_aircraftPrefab, new Vector3(0, 0, 1), Quaternion.identity);
            
            aircraft.transform.SetParent(gameObject.transform, false);
            aircraft.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, attackRange * 2);

            aircraft.GetComponent<AircraftBehaviour>().SetAirfield(gameObject);
            aircraft.GetComponent<AircraftBehaviour>().SetRange(flyingRange, attackRange);
            aircraft.GetComponent<AircraftBehaviour>().SetAttack(attack, attackPeriod);
            aircraft.GetComponent<AircraftBehaviour>().SetTargetCoordinates(transform.position.x, transform.position.y);
        }
    }

    void OnMouseDown()
    {
        SpawnAircraft(30, 5, 50, 3f);
    }

    public void AttackRangeMouseDown(float flyingRange)
    {
        _airfieldFlyingRange = Instantiate<GameObject>(_airfieldFlyingRangePrefab, transform.position, Quaternion.identity);
        _airfieldFlyingRange.transform.SetParent(transform);
        _airfieldFlyingRange.transform.localScale = new Vector3(flyingRange * 2, flyingRange * 2, flyingRange * 2);
    }

    public void AttackRangeMouseUp()
    {
        Destroy(_airfieldFlyingRange);
    }
}
