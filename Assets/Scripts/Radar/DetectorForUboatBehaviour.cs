using UnityEngine;

public class DetectorForUboatBehaviour : MonoBehaviour
{
    private UboatBehaviour _uboatBehaviour;
    private GameObject _parent;
    private GameObject _backgroundClickDetector;
    
    void Start()
    {
        _backgroundClickDetector = GameObject.FindWithTag("BackgroundClickDetector");
    }

    void Update()
    {
        transform.position = _parent.transform.position;
    }

    public void SetParent(GameObject parent)
    {
        _parent = parent;
        _uboatBehaviour = parent.GetComponent<UboatBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Convoy" || collision.gameObject.tag == "Escort")
        {
            GameManager.Instance.detectionManager.AddFriendly(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Convoy" || collision.gameObject.tag == "Escort")
        {
            GameManager.Instance.detectionManager.RemoveFriendly(collision.gameObject);
        }
    }

    void OnMouseDown()
    {
        _backgroundClickDetector.GetComponent<BackgroundClickDetector>().PassMouseDown();
    }
}
