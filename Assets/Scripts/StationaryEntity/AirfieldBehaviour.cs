using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirfieldBehaviour : StationaryEntityBehaviour
{
    [SerializeField] private GameObject _airfieldAttackRangePrefab;
    [SerializeField] private GameObject _airfieldFlyingRangePrefab;
    private GameObject _airfieldAttackRange;
    private GameObject _airfieldFlyingRange;

    private float _targetXCoordinate;
    private float _targetYCoordinate;

    private float _flyingRange;
    private float _attackRange;

    private int _attack;
    private float _attackPeriod;

    void Start()
    {
        _airfieldAttackRange = Instantiate<GameObject>(_airfieldAttackRangePrefab, new Vector3(_targetXCoordinate, _targetYCoordinate, 1), Quaternion.identity);
        _airfieldAttackRange.GetComponent<AirfieldAttackRangeBehaviour>().SetParent(gameObject);
        _airfieldAttackRange.transform.SetParent(gameObject.transform);
        _airfieldAttackRange.transform.localScale = new Vector3(_attackRange, _attackRange, _attackRange);

        StartCoroutine(AttackUboatsInRange());
    }

    public void SetTargetCoordinates(float xPos, float yPos)
    {
        _targetXCoordinate = xPos;
        _targetYCoordinate = yPos;

        if (_airfieldAttackRange != null)
        {
            _airfieldAttackRange.GetComponent<AirfieldAttackRangeBehaviour>().MoveToPosition(xPos, yPos);
        }
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

    public void AttackRangeMouseDown()
    {
        _airfieldFlyingRange = Instantiate<GameObject>(_airfieldFlyingRangePrefab, transform.position, Quaternion.identity);
        _airfieldFlyingRange.transform.SetParent(transform);
        _airfieldFlyingRange.transform.localScale = new Vector3(_flyingRange * 2, _flyingRange * 2, _flyingRange * 2);
    }

    public void AttackRangeMouseUp()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
        if (Vector2.Distance(mousePosition, new Vector2(_xCoordinate, _yCoordinate) ) < _flyingRange)
        {
            SetTargetCoordinates(mousePosition.x, mousePosition.y);
        }
        Destroy(_airfieldFlyingRange);
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
