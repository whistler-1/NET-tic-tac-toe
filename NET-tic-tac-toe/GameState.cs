namespace NET_tic_tac_toe;

public enum GameState
{
    NewGame, //While in a new game, wait for user to begin transition to midgame
    XTurn,   
    OTurn,
    GameOver
}