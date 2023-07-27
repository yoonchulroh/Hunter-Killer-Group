public class RadarData
{
    public float range;

    public RadarData(float range)
    {
        this.range = range;
    }
}

public class RadarInitialData
{
    public static RadarData Escort()
    {
        return new RadarData(2f);
    }

    public static RadarData Uboat()
    {
        return new RadarData(4f);
    }

    public static RadarData Stationary()
    {
        return new RadarData(4f);
    }
}