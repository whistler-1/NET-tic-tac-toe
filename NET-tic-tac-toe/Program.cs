using NET_tic_tac_toe;

// Simple tic-tac-toe game in .NET, using the console.

const string x = "X";
const string o = "O";

string[] board;     //Using a regular array, as it makes some actions easier for now.
GameState state;
string currentPlayer;


Initialize();
return;


void Initialize()
{
    currentPlayer = x;
    state = GameState.NewGame;
    board = ["0", "1", "2", "3", "4", "5", "6", "7", "8"];
    
    Console.CursorVisible = false;
    Console.Title = "Tic-Tac-Toe";
    Console.Beep();

    var tic = DateTime.Now;
    var oneSecond = new TimeSpan(0, 0, 1);
    
    while (state != GameState.GameClose)
    {
        if (DateTime.Now.Subtract(tic) >= oneSecond) continue;
        
        Update();
        
        switch (state)
        {
            case GameState.NewGame:
                NewGameScreen();
                break;
            case GameState.XTurn:
            //case GameState.OTurn:
                PlayScreen();
                break;
        }
        
        Console.Clear();
        tic = DateTime.Now;
    }
}

void Update()
{
    currentPlayer = state switch
    {
        GameState.XTurn => x,
        GameState.OTurn => o,
        _ => currentPlayer
    };
}

void PlayScreen()
{
    ConsolePainter.DrawBox(40, 11);

    Console.SetCursorPosition(5, 2);
    Console.Write($"It's {currentPlayer}'s turn!");
    
    ConsolePainter.DrawGrid(board, 5, 4);
        
    Console.SetCursorPosition(5, 10);
    Console.Write("Enter a number to place your piece.");
    
    if (Console.ReadLine() is not null)
    {
        state = GameState.OTurn;
    }
}

void NewGameScreen()
{
    
    ConsolePainter.DrawBox(40, 7);

    //Console.Clear();
    Console.SetCursorPosition(5, 2);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Welcome to Tic-Tac-Toe!");
    Console.ResetColor();

    Console.SetCursorPosition(5, 2 + 2);
    Console.Write("Enter any key to begin.");
    
    Console.SetCursorPosition(5, 2 + 3);
    
    if (Console.ReadLine() is not null)
    {
        state = GameState.XTurn;
    }
}

void GameOverScreen(string winner)
{
    winner = x;

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"Game over - {winner} won!");
    Console.ResetColor();
}
