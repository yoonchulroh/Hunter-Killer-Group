using UnityEngine;

public class DetectorForUboatBehaviour : MonoBehaviour
{
    private UboatBehaviour _uboatBehaviour;
    private GameObject _parent;

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
        _uboatBehaviour.CollisionEnter(collision);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _uboatBehaviour.CollisionExit(collision);
    }
}
