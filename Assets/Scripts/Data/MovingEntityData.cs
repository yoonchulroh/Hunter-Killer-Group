public class MovingEntityData
{
    public int id;

    public int hp;

    public int fullHP;

    public float speed;

    public int attack;

    public float attackPeriod;

    public float attackRange;

    public float attackHitChance;

    public int defense;
    public float defenseHitChance;

    public MovingEntityData(int id, int hp, float speed, int attack, float attackPeriod, float attackRange, float attackHitChance, int defense, float defenseHitChance)
    {
        this.id = id;
        this.hp = hp;
        this.fullHP = hp;
        this.speed = speed;
        this.attack = attack;
        this.attackPeriod = attackPeriod;
        this.attackRange = attackRange;
        this.attackHitChance = attackHitChance;
        this.defense = defense;
        this.defenseHitChance = defenseHitChance;
    }
}

public class MovingEntityInitialData
{
    public static MovingEntityData Convoy(int convoyID)
    {
        return new MovingEntityData(id: convoyID, hp: 500, speed: 1f, attack: 5, attackPeriod: 1f, attackRange: 1f, attackHitChance: 0.5f, defense: 1, defenseHitChance: 1f);
    }

    public static MovingEntityData Escort(int escortID)
    {
        return new MovingEntityData(id: escortID, hp: 300, speed: 2f, attack: 40, attackPeriod: 1f, attackRange: 1f, attackHitChance: 0.8f, defense: 4, defenseHitChance: 1f);
    }

    public static MovingEntityData Uboat(int uboatID)
    {
        return new MovingEntityData(id: uboatID, hp: 100, speed: 3f, attack: 20, attackPeriod: 0.25f, attackRange: 2f, attackHitChance: 0.9f, defense: 1, defenseHitChance: 0.2f);
    }
}


