public class BombDm
{
    protected BombDm(BombEnumeration id, int score)
    {
        Id = id;
        Score = score;
    }

    public readonly BombEnumeration Id;
    public int Score;
}
