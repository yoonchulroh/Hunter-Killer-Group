using UnityEngine;

public class ConvoyBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private GameObject _destinationPort;
    private Vector3 _destination;
    private float _speed;
    private int _id;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _destination = new Vector3(30,0,0);
        _speed = 15f;
    }

    private void Update()
    {
        var currentPosition = transform.position;
        var directionalVector = _speed * (_destination - currentPosition).normalized;
        _rigidBody2D.velocity = directionalVector;
    }

    private void ArrivedAtDestination()
    {

    }

    public void SetOrigin(GameObject port)
    {
        transform.position = port.GetComponent<PortBehaviour>().coordinate;
    }

    public void SetDestination(GameObject port)
    {
        _destinationPort = port;
        _destination = port.GetComponent<PortBehaviour>().coordinate;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetID(int id)
    {
        _id = id;
    }
}