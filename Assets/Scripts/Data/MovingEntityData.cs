public class MovingEntityData
{
    public int id;

    public int hp;

    public float speed;

    public int attack;

    public float attackPeriod;

    public float attackRange;

    public MovingEntityData(int id, int hp, float speed, int attack, float attackPeriod, float attackRange)
    {
        this.id = id;
        this.hp = hp;
        this.speed = speed;
        this.attack = attack;
        this.attackPeriod = attackPeriod;
        this.attackRange = attackRange;
    }
}

public class MovingEntityInitialData
{
    public static MovingEntityData Convoy(int convoyID)
    {
        return new MovingEntityData(convoyID, 50, 2f, 5, 1f, 1f);
    }

    public static MovingEntityData Escort(int escortID)
    {
        return new MovingEntityData(escortID, 300, 5f, 20, 1f, 2f);
    }

    public static MovingEntityData Uboat(int uboatID)
    {
        return new MovingEntityData(uboatID, 100, 3f, 5, 0.25f, 2f);
    }
}


