using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBehaviour : MonoBehaviour
{
    private GameObject _convoySpawner;
    private GameObject _seawaySpawner;
    public Vector3 coordinate;

    private int _id;
    public int id => _id;

    void Awake()
    {
        _convoySpawner = GameObject.FindWithTag("ConvoySpawner");
        _seawaySpawner = GameObject.FindWithTag("SeawaySpawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCoordinate(Vector3 portCoordinate)
    {
        transform.position = portCoordinate;
        coordinate = portCoordinate;
    }

    public void SetID(int id)
    {
        _id = id;
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
            GameManager.Instance.createManager.SwitchCreateMode(CreateMode.SeawaysDestination);
        }
        else if (GameManager.Instance.createManager.createMode == CreateMode.SeawaysDestination)
        {
            if (GameManager.Instance.createManager.seawayOrigin != _id)
            {
                _seawaySpawner.GetComponent<SeawaySpawner>().SpawnSeaway(GameManager.Instance.createManager.seawayOrigin, _id);
                GameManager.Instance.createManager.SwitchCreateMode(CreateMode.SeawaysOrigin);
            }
        }
    }
}
