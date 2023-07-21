using UnityEngine;

public enum CreateMode
{
    Convoys,
    SeawaysOrigin,
    SeawaysDestination
}

public class CreateManager : MonoBehaviour
{
    private CreateMode _createMode;
    public CreateMode createMode => _createMode;
    
    public int seawayOrigin;

    void Start()
    {
        _createMode = CreateMode.Convoys;
    }

    public void SwitchCreateMode(CreateMode target)
    {
        _createMode = target;
    }
}
