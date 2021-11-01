public abstract class SuperFruitDm
{
    protected SuperFruitDm(FruitEnumeration id, int score)
    {
        Id = id;
        Score = score;
    }

    public readonly FruitEnumeration Id;
    public int Score;
}
