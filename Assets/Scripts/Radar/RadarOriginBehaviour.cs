using UnityEngine;

public class RadarOriginBehaviour : MonoBehaviour
{
    private GameObject _parent;

    public void SetParent(GameObject parent)
    {
        _parent = parent;
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.Delete)
        {
            _parent.GetComponent<RadarBehaviour>().RemoveRadar();
        }
    }
}
