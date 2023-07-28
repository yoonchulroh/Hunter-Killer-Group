using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;

public class MovingEntityBehaviour : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected GameObject _labelPrefab;
    [SerializeField] protected GameObject _destroyedEntityPrefab;

    protected Rigidbody2D _rigidBody2D;

    protected GameObject _destroyedEntityCollectionObject;

    protected GameObject _hpText;

    protected Vector3 _destination;

    protected MovingEntityData _movingEntityData;
    public MovingEntityData movingEntityData => _movingEntityData;

    protected string _identification;
    public string identification => _identification;
    protected string _role;
    public string role => _role;

    public virtual void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _destroyedEntityCollectionObject = GameObject.FindWithTag("DestroyedEntityCollection");
        
        /*
        _hpText = Instantiate<GameObject>(_labelPrefab, new Vector3(-2, 0, 0), Quaternion.identity);
        _hpText.transform.SetParent(gameObject.transform, false);
        _hpText.GetComponent<LabelTextBehaviour>().SetHpLabel(_movingEntityData.hp);
        */
    }

    public void SetMovingEntityProperties(MovingEntityData movingEntityData)
    {
        _movingEntityData = movingEntityData;
    }

    public void SetDestinationRandomly()
    {
        var xLimit = GameManager.Instance.cameraManager.gameObjectXLimit;
        var yLimit = GameManager.Instance.cameraManager.gameObjectYLimit;
        _destination = new Vector3(Random.Range(-xLimit, xLimit), Random.Range(-yLimit, yLimit), 0f);
    }

    public virtual void CheckArrivedAtDestination(float allowedRange)
    {
        if (Math.Abs(transform.position.x - _destination.x) < allowedRange && Math.Abs(transform.position.y - _destination.y) < allowedRange)
        {
            SetDestinationRandomly();
        }
    }

    public void Attacked(int damage)
    {
        _movingEntityData.hp -= damage;
        //_hpText.GetComponent<LabelTextBehaviour>().SetHpLabel(_movingEntityData.hp);
        if (_movingEntityData.hp <= 0)
        {
            Destroyed();
        }
    }

    public virtual void Destroyed()
    {
        var destroyedEntity = Instantiate<GameObject>(_destroyedEntityPrefab, transform.position, Quaternion.identity);
        destroyedEntity.transform.SetParent(_destroyedEntityCollectionObject.transform, false);
        Destroy(gameObject);
    }

    public Vector3 CurrentPosition()
    {
        return transform.position;
    }

    void OnMouseDown()
    {
        GameManager.Instance.editManager.SwitchEditMode(EditMode.Select, gameObject);
        GameManager.Instance.editManager.selectedObject = gameObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        /*
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (GameManager.Instance.editManager.editMode == EditMode.Select && GameManager.Instance.editManager.selectedObject != null && GameManager.Instance.editManager.selectedObject.tag == "Escort")
            {
                if (gameObject.tag != "Uboat")
                {
                    GameManager.Instance.editManager.selectedObject.GetComponent<EscortBehaviour>().FollowFriendlyOrder(gameObject);
                }
            }
        }
        */
    }
}
