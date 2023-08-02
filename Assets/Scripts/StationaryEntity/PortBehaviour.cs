using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBehaviour : StationaryEntityBehaviour
{
    [SerializeField] protected GameObject _seawayPrefab;
    [SerializeField] protected GameObject _labelPrefab;
    protected GameObject _seawayCandidate;

    protected GameObject _convoySpawner;
    protected GameObject _seawaySpawner;

    public char alphabetID;

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
    }

    public override void SetID(int id)
    {
        base.SetID(id);
        alphabetID = (char) (id+64);
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.CreateSeawaysOrigin)
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
