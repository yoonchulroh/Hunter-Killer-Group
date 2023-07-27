using System.Collections.Generic;

public class RadarData
{
    public float range;

    public List<string> enemyTags;

    public RadarData(float range, List<string> enemyTags)
    {
        this.range = range;
        this.enemyTags = enemyTags;
    }
}

public class RadarInitialData
{
    public static RadarData Escort()
    {
        return new RadarData(2f, new List<string> {"Uboat"} );
    }

    public static RadarData Uboat()
    {
        return new RadarData(4f, new List<string> {"Convoy", "Escort"} );
    }

    public static RadarData Stationary()
    {
        return new RadarData(10f, new List<string> {"Uboat"} );
    }
}