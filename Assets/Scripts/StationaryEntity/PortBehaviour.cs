using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortType
{
    Producer,
    Consumer,
    Waypoint
}

public enum ResourceType
{
    Square,
    Circle,
    Triangle,
    Star
}

public class PortBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _seawayPrefab;
    [SerializeField] private GameObject _labelPrefab;
    private GameObject _seawayCandidate;

    private GameObject _convoySpawner;
    private GameObject _seawaySpawner;
    public Vector3 coordinate;

    private int _id;
    public int id => _id;
    public char alphabetID;

    private PortType _portType;
    public PortType portType => _portType;
    private ResourceType _resourceType;
    public ResourceType resourceType => _resourceType;
    private GameObject _resourceNeedLabel;
    private float _resourceNeed = 0f;
    public float resourceNeed => _resourceNeed;
    private float resourceFillSpeed = 1f;

    void Awake()
    {
        _convoySpawner = GameObject.FindWithTag("ConvoySpawner");
        _seawaySpawner = GameObject.FindWithTag("SeawaySpawner");
    }

    void Start()
    {
        var nameLabel = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        nameLabel.transform.SetParent(gameObject.transform, false);
        nameLabel.GetComponent<LabelTextBehaviour>().SetNameLabel(gameObject, ParentType.Port);

        var roleLabel = Instantiate<GameObject>(_labelPrefab, new Vector3(0, -1, 0), Quaternion.identity);
        roleLabel.transform.SetParent(gameObject.transform, false);
        roleLabel.GetComponent<LabelTextBehaviour>().SetRoleLabel(gameObject, ParentType.Port);

        if (_portType == PortType.Consumer)
        {
            var resourceNeedLabel = Instantiate<GameObject>(_labelPrefab, new Vector3(1.5f, 0, 0), Quaternion.identity);
            resourceNeedLabel.transform.SetParent(gameObject.transform, false);
            resourceNeedLabel.GetComponent<LabelTextBehaviour>().SetResourceAmountLabel(_resourceNeed);

            _resourceNeedLabel = resourceNeedLabel;
        }
    }

    void Update()
    {
        if (_portType == PortType.Consumer)
        {
            _resourceNeed += resourceFillSpeed * Time.deltaTime;
            _resourceNeedLabel.GetComponent<LabelTextBehaviour>().SetResourceAmountLabel(_resourceNeed);
        }
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

    public void SetPortRole(PortType portType, ResourceType resourceType)
    {
        _portType = portType;
        _resourceType = resourceType;
    }

    public void ResourceFilled(float amount)
    {
        _resourceNeed -= amount;
        if (_resourceNeed < 0)
        {
            _resourceNeed = 0;
        }
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.CreateConvoys && _portType == PortType.Producer)
        {
            _convoySpawner.GetComponent<ConvoySpawner>().SpawnConvoyOnPort(_id, _resourceType);
        }
        else if (GameManager.Instance.editManager.editMode == EditMode.CreateSeawaysOrigin)
        {
            GameManager.Instance.editManager.seawayOrigin = _id;
            GameManager.Instance.editManager.seawayDestinationCandidate = _id;
            GameManager.Instance.editManager.SwitchEditMode(EditMode.CreateSeawaysDestination);

            _seawayCandidate = Instantiate<GameObject>(_seawayPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            _seawayCandidate.GetComponent<LineRenderer>().SetPosition(0, coordinate);
            _seawayCandidate.GetComponent<LineRenderer>().SetPosition(1, coordinate);
        }
    }

    void OnMouseUp()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.CreateSeawaysDestination)
        {
            if (GameManager.Instance.editManager.seawayDestinationCandidate != GameManager.Instance.editManager.seawayOrigin)
            {
                _seawaySpawner.GetComponent<SeawaySpawner>().SpawnSeaway(_id, GameManager.Instance.editManager.seawayDestinationCandidate);
            }
            Destroy(_seawayCandidate);
            GameManager.Instance.editManager.SwitchEditMode(EditMode.CreateSeawaysOrigin);
        }
    }

    void OnMouseDrag()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.CreateSeawaysDestination)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
            _seawayCandidate.GetComponent<LineRenderer>().SetPosition(1, mousePosition);
        }
    }

    void OnMouseEnter()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.CreateSeawaysDestination)
        {
            if (GameManager.Instance.editManager.seawayOrigin != _id)
            {
                GameManager.Instance.editManager.seawayDestinationCandidate = _id;
            }
        }
    }

    void OnMouseExit()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.CreateSeawaysDestination)
        {
            GameManager.Instance.editManager.seawayDestinationCandidate = GameManager.Instance.editManager.seawayOrigin;
        }
    }
}
