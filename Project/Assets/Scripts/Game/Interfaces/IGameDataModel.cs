public interface IGameDataModel
{
    float GameTime{ get; set;}
    int CountMisses { get; set; }  
    int GameScore { get; set; }
   
    EnumGameState GameState { get; set; }

}
