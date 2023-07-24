using UnityEngine;

public enum CreateMode
{
    Convoys,
    SeawaysOrigin,
    SeawaysDestination,
    Radar
}

public class CreateManager : MonoBehaviour
{
    private CreateMode _createMode;
    public CreateMode createMode => _createMode;
    
    public int seawayOrigin;
    public int seawayDestinationCandidate;

    void Start()
    {
        _createMode = CreateMode.SeawaysOrigin;
    }

    public void SwitchCreateMode(CreateMode target)
    {
        _createMode = target;
    }
}
