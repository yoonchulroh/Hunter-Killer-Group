using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBehaviour : StationaryEntityBehaviour
{
    [SerializeField] private GameObject _seawayPrefab;
    [SerializeField] private GameObject _labelPrefab;
    private GameObject _seawayCandidate;

    private GameObject _convoySpawner;
    private GameObject _seawaySpawner;

    public char alphabetID;

    private PortType _portType;
    public PortType portType => _portType;
    private ResourceType _resourceType;
    public ResourceType resourceType => _resourceType;
    private GameObject _resourceNeedLabel;
    private float _resourceNeed = 0f;
    public float resourceNeed => _resourceNeed;
    private float resourceFillSpeed = 1f;

    private float _convoySpawnPeriod = 5f;

    void Awake()
    {
        _convoySpawner = GameObject.FindWithTag("ConvoySpawner");
        _seawaySpawner = GameObject.FindWithTag("SeawaySpawner");
    }

    void Start()
    {
        /*
        var identificationLabel = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        identificationLabel.transform.SetParent(gameObject.transform, false);
        identificationLabel.GetComponent<LabelTextBehaviour>().SetIdentificationLabel(gameObject);

        var roleLabel = Instantiate<GameObject>(_labelPrefab, new Vector3(0, -1, 0), Quaternion.identity);
        roleLabel.transform.SetParent(gameObject.transform, false);
        roleLabel.GetComponent<LabelTextBehaviour>().SetRoleLabel(gameObject);
        */

        if (_portType == PortType.Consumer)
        {
            var resourceNeedLabel = Instantiate<GameObject>(_labelPrefab, new Vector3(1.5f, 0, 0), Quaternion.identity);
            resourceNeedLabel.transform.SetParent(gameObject.transform, false);
            resourceNeedLabel.GetComponent<LabelTextBehaviour>().SetResourceAmountLabel(_resourceNeed);

            _resourceNeedLabel = resourceNeedLabel;

            StartCoroutine(SpawnConvoyPeriodically());
        } else if (_portType == PortType.Producer)
        {
            transform.localScale = new Vector3(2, 2, 2);
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

    public override void SetID(int id)
    {
        base.SetID(id);
        alphabetID = (char) (id+64);
    }

    public void SetPortRole(PortType portType, ResourceType resourceType)
    {
        _portType = portType;
        _resourceType = resourceType;
        GetComponent<SpriteRenderer>().color = ResourceData.ResourceTypeToColor(resourceType);
    }

    public void ResourceFilled(float amount)
    {
        _resourceNeed -= amount;
        if (_resourceNeed < 0)
        {
            _resourceNeed = 0;
        }
    }

    private IEnumerator SpawnConvoyPeriodically()
    {
        while (true)
        {
            if (GameManager.Instance.portManager.FindNextPort(_id, GameManager.Instance.portManager.producerPortDict[_resourceType]) > 0)
            {
                _convoySpawner.GetComponent<ConvoySpawner>().SpawnConvoyOnPort(GameManager.Instance.portManager.producerPortDict[_resourceType], _id, _resourceType);
            }
            yield return new WaitForSeconds(_convoySpawnPeriod);
        }
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.CreateConvoys && _portType == PortType.Producer)
        {
            //_convoySpawner.GetComponent<ConvoySpawner>().SpawnConvoyOnPort(_id, _resourceType);
        }
        else if (GameManager.Instance.editManager.editMode == EditMode.CreateSeawaysOrigin)
        {
            GameManager.Instance.editManager.seawayOrigin = _id;
            GameManager.Instance.editManager.seawayDestinationCandidate = _id;
            GameManager.Instance.editManager.SwitchEditMode(EditMode.CreateSeawaysDestination);

            _seawayCandidate = Instantiate<GameObject>(_seawayPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            _seawayCandidate.GetComponent<LineRenderer>().SetPosition(0, Coordinate());
            _seawayCandidate.GetComponent<LineRenderer>().SetPosition(1, Coordinate());
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
