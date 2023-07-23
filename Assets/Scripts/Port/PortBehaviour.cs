using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _seawayPrefab;
    private GameObject _seawayCandidate;

    private GameObject _convoySpawner;
    private GameObject _seawaySpawner;
    public Vector3 coordinate;

    private int _id;
    public int id => _id;

    public char alphabetID;

    void Awake()
    {
        _convoySpawner = GameObject.FindWithTag("ConvoySpawner");
        _seawaySpawner = GameObject.FindWithTag("SeawaySpawner");
    }

    public void SetCoordinate(Vector3 portCoordinate)
    {
        transform.position = portCoordinate;
        coordinate = portCoordinate;
    }

    public void SetID(int id)
    {
        _id = id;
        alphabetID = (char) (id+65);
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.createManager.createMode == CreateMode.Convoys)
        {
            _convoySpawner.GetComponent<ConvoySpawner>().SpawnConvoyOnPort(_id);
        }
        else if (GameManager.Instance.createManager.createMode == CreateMode.SeawaysOrigin)
        {
            GameManager.Instance.createManager.seawayOrigin = _id;
            GameManager.Instance.createManager.seawayDestinationCandidate = _id;
            GameManager.Instance.createManager.SwitchCreateMode(CreateMode.SeawaysDestination);

            _seawayCandidate = Instantiate<GameObject>(_seawayPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            _seawayCandidate.GetComponent<LineRenderer>().SetPosition(0, coordinate);
            _seawayCandidate.GetComponent<LineRenderer>().SetPosition(1, coordinate);
        }
    }

    void OnMouseUp()
    {
        if (GameManager.Instance.createManager.createMode == CreateMode.SeawaysDestination)
        {
            if (GameManager.Instance.createManager.seawayDestinationCandidate != GameManager.Instance.createManager.seawayOrigin)
            {
                _seawaySpawner.GetComponent<SeawaySpawner>().SpawnSeaway(_id, GameManager.Instance.createManager.seawayDestinationCandidate);
            }
            Destroy(_seawayCandidate);
            GameManager.Instance.createManager.SwitchCreateMode(CreateMode.SeawaysOrigin);
        }
    }

    void OnMouseDrag()
    {
        if (GameManager.Instance.createManager.createMode == CreateMode.SeawaysDestination)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
            _seawayCandidate.GetComponent<LineRenderer>().SetPosition(1, mousePosition);
        }
    }

    void OnMouseEnter()
    {
        if (GameManager.Instance.createManager.createMode == CreateMode.SeawaysDestination)
        {
            if (GameManager.Instance.createManager.seawayOrigin != _id)
            {
                GameManager.Instance.createManager.seawayDestinationCandidate = _id;
            }
        }
    }

    void OnMouseExit()
    {
        if (GameManager.Instance.createManager.createMode == CreateMode.SeawaysDestination)
        {
            GameManager.Instance.createManager.seawayDestinationCandidate = GameManager.Instance.createManager.seawayOrigin;
        }
    }
}
