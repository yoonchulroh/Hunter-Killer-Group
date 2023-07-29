using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftBehaviour : MonoBehaviour
{
    private GameObject _airfield;

    private float _flyingRange;
    private float _attackRange;

    private int _attack;
    private float _attackPeriod;

    private float _targetXCoordinate;
    private float _targetYCoordinate;

    private float _airfieldXCoordinate;
    private float _airfieldYCoordinate;

    void Start()
    {
        transform.localScale = new Vector3(_attackRange * 2, _attackRange * 2, _attackRange * 2);

        StartCoroutine(AttackUboatsInRange());
    }

    public void SetAirfield(GameObject airfield)
    {
        _airfield = airfield;
        _airfieldXCoordinate = airfield.GetComponent<AirfieldBehaviour>().xCoordinate;
        _airfieldYCoordinate = airfield.GetComponent<AirfieldBehaviour>().yCoordinate;
    }

    public void SetRange(float flyingRange, float attackRange)
    {
        _flyingRange = flyingRange;
        _attackRange = attackRange;
    }

    public void SetAttack(int attack, float attackPeriod)
    {
        _attack = attack;
        _attackPeriod = attackPeriod;
    }

    public void SetTargetCoordinates(float xPos, float yPos)
    {
        _targetXCoordinate = xPos;
        _targetYCoordinate = yPos;

        MoveToPosition(xPos, yPos);
    }

    public void MoveToPosition(float xPos, float yPos)
    {
        transform.position = new Vector3(xPos, yPos, 1);
    }

    void OnMouseDown()
    {
        _airfield.GetComponent<AirfieldBehaviour>().AttackRangeMouseDown(_flyingRange);
    }

    void OnMouseUp()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
        if (Vector2.Distance(mousePosition, new Vector2(_airfieldXCoordinate, _airfieldYCoordinate) ) < _flyingRange)
        {
            SetTargetCoordinates(mousePosition.x, mousePosition.y);
        }
        _airfield.GetComponent<AirfieldBehaviour>().AttackRangeMouseUp();
    }

    private IEnumerator AttackUboatsInRange()
    {
        List<GameObject> uboatsInRange = null;
        while (true)
        {
            uboatsInRange = GameManager.Instance.uboatManager.EntitiesInRange(_targetXCoordinate, _targetYCoordinate, _attackRange);
            foreach(GameObject uboat in uboatsInRange)
            {
                uboat.GetComponent<MovingEntityBehaviour>().Attacked(_attack);
            }
            yield return new WaitForSeconds(_attackPeriod);
        }
    }
}
