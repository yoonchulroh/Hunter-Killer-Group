using UnityEngine;

public class AirfieldAttackRangeBehaviour : MonoBehaviour
{
    private GameObject _parent;

    public void SetParent(GameObject parent)
    {
        _parent = parent;
    }

    public void MoveToPosition(float xPos, float yPos)
    {
        transform.position = new Vector3(xPos, yPos, 1);
    }

    void OnMouseDown()
    {
        _parent.GetComponent<AirfieldBehaviour>().AttackRangeMouseDown();
    }

    void OnMouseUp()
    {
        _parent.GetComponent<AirfieldBehaviour>().AttackRangeMouseUp();
    }
}
