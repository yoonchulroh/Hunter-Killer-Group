using UnityEngine;

public class ResourceData
{
    public static Color ResourceTypeToColor(ResourceType resourceType)
    {
        Color color;
        switch (resourceType)
        {
            case ResourceType.Red:
                color = new Color(255, 0, 0);
                break;
            case ResourceType.Green:
                color = new Color(0, 255, 0);
                break;
            case ResourceType.Blue:
                color = new Color(0, 0, 255);
                break;
            case ResourceType.Yellow:
                color = new Color(255, 255, 0);
                break;
            case ResourceType.Purple:
                color = new Color(255, 0, 255);
                break;
            case ResourceType.Teal:
                color = new Color(0, 255, 255);
                break;
            case ResourceType.White:
                color = new Color(255, 255, 255);
                break;
            default:
                color = new Color(0, 0, 0);
                break;
        }
        return color;
    }
}

public enum PortType
{
    Producer,
    Consumer,
    Waypoint
}

public enum ResourceType
{
    Red,
    Green,
    Blue,
    Yellow,
    Purple,
    Teal,
    White
}
