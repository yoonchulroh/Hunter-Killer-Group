using UnityEngine;

public class DestroyedEntityBehaviour : MonoBehaviour
{
    void Start()
    {
        Invoke("RemoveGameObject", 10f);
    }

    void RemoveGameObject()
    {
        Destroy(gameObject);
    }
}
