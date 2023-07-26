using UnityEngine;

public class DetectorForUboatBehaviour : MonoBehaviour
{
    private UboatBehaviour _uboatBehaviour;
    private GameObject _parent;
    
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
}
