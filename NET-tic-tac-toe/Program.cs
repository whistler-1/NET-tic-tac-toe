using NET_tic_tac_toe;


// Simple tic-tac-toe game in .NET, printed to the console.


const string none = " ";
const string x = "X";
const string o = "O";

string[,] board = new string[3, 3]
{
    { none, none, none },
    { none, none, none },
    { none, none, none }
};


var gameState = GameState.NewGame;

void NewGameScreen()
{
    Console.CursorVisible = false;
        
    ConsolePainter.DrawBox(40, 7);
        
    //Console.Clear();
    Console.SetCursorPosition(5, 2);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Welcome to Tic-Tac-Toe!");
    Console.ResetColor();
        
    Console.SetCursorPosition(5, 2+2);
    Console.Write($"Press the X key to begin.");

    if (Console.ReadLine() is not null)
    {
        
    }

    Console.ReadKey();
    Console.Clear();
    gameState = GameState.NewGame;
    return;

}


void PlayScreen()
{
    var currentPlayer = x;
    
    if (gameState == GameState.OTurn)
    {
        currentPlayer = o;
    }
    
    Console.WriteLine($"It's {currentPlayer}'s turn!");
    
    
}

void GameOverScreen(string winner)
{
    winner = x;
    
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"Game over - {winner} won!");
    Console.ResetColor();
    
    
}

void GameLoop()
{
    switch(gameState) 
    {
        case GameState.NewGame:
            NewGameScreen();
            break;
        case GameState.XTurn:
        case GameState.OTurn:
            PlayScreen();
            break;
        case GameState.GameOver:
            GameOverScreen(x);
            break;
        default:
            // code block
            break;
    }
    if (gameState == GameState.NewGame)
    {
        
    }
    
    var printableBoard = $"""
                               {board[0,0]}  ||  {board[0,1]}  ||  {board[0,2]} 
                              =================
                               {board[0,0]}  ||  {board[0,1]}  ||  {board[0,2]} 
                              =================
                               {board[0,0]}  ||  {board[0,1]}  ||  {board[0,2]} 
                          """;

}

void Initialize()
{
    Console.Title = "Tic-Tac-Toe";
    Console.Beep();
    while (true)
    {
        GameLoop();
    }
}

Initialize();

