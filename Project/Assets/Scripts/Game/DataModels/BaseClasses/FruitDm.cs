public abstract class FruitDm
{
    protected FruitDm(FruitEnumeration id, int score)
    {
        Id = id;
        Score = score;
    }

    public readonly FruitEnumeration Id;
    public int Score;
}
