using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public EditManager editManager { get; private set; }
    public UIManager uIManager { get; private set; }

    public ConvoyManager convoyManager { get; private set; }
    public PortManager portManager { get; private set; }
    public SeawayManager seawayManager { get; private set; }
    public UboatManager uboatManager { get; private set; }
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

        convoyManager = GetComponentInChildren<ConvoyManager>();
        portManager = GetComponentInChildren<PortManager>();
        seawayManager = GetComponentInChildren<SeawayManager>();
        uboatManager = GetComponentInChildren<UboatManager>();
        detectionManager = GetComponentInChildren<DetectionManager>();
    }
}