using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public EditManager editManager { get; private set; }
    public UIManager uIManager { get; private set; }

    public MovingEntityManager convoyManager { get; private set; }
    public MovingEntityManager uboatManager { get; private set; }
    public MovingEntityManager escortManager { get; private set; }
    public PortManager portManager { get; private set; }
    public SeawayManager seawayManager { get; private set; }
    public DetectionManager detectionManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        editManager = GetComponentInChildren<EditManager>();
        uIManager = GetComponentInChildren<UIManager>();

        convoyManager = GetComponentInChildren<MovingEntityManager>();
        uboatManager = GetComponentInChildren<MovingEntityManager>();
        escortManager = GetComponentInChildren<MovingEntityManager>();
        portManager = GetComponentInChildren<PortManager>();
        seawayManager = GetComponentInChildren<SeawayManager>();
        detectionManager = GetComponentInChildren<DetectionManager>();
    }
}