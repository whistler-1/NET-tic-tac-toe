using NET_tic_tac_toe;

// Simple tic-tac-toe game in .NET, using the console.

const string x = "x"; //"✖", 𝐱 𝑥 𝒙 𝕩  𝗑
const string o = "o"; //"⭕";  𝒐   𝐨 𝕠  𝗈

string[] board;     //Using a regular array, as it makes some actions easier for now.
GameState state;
string currentPlayer;
string? winner;

Initialize();
return;


void Initialize()
{
    currentPlayer = x;
    state = GameState.NewGame;
    board = ["1", "2", "3", "4", "5", "6", "7", "8", "9"];
    
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
            case GameState.OTurn:
                PlayScreen();
                break;
            case GameState.GameOver:
                GameOverScreen();
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
    
    Console.SetCursorPosition(5, 11);
    
    var input = Console.ReadLine();
    if (input is null) return;

    string resultMessage;
    
    if (int.TryParse(input, out var number) && number is >= 1 and <= 9 )
    {  
        //User is given the option to enter number 1-9 as it's more intuitive than the 0-8 used by the array.
        //This also has the side effect of reducing confusion between 0 and O. (hide unintuitive logic within a method?)
        
        number--;
        
        if (ValidMove(number))
        {
            board[number] = currentPlayer;
            
            state = state switch
            {
                GameState.XTurn => GameState.OTurn,
                GameState.OTurn => GameState.XTurn,
                _ => state
            };
            
            resultMessage = "Piece placed!";
            
            winner = CheckBoardState();
            if (winner is not null)
            {
                resultMessage = "Game ended! ";
                if (winner != "tie")
                {
                    resultMessage += winner + " is the winner!";
                }
                else
                {
                    resultMessage += "It's a tie!";
                }
                 
                state = GameState.GameOver;
            }

        }
        else
        {
            resultMessage = "Invalid move! Try again.";
        }
    }
    else
    {
        resultMessage = $"Could not read input \"{input}\"";
    }
    
    
    
    Console.Clear();
    ConsolePainter.DrawBox(40, 11);
    Console.SetCursorPosition(5, 2);
    Console.Write(resultMessage);
    Console.SetCursorPosition(5, 3);
    Console.Write("Press [Enter] to Continue.");
    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { /*Do nothing while we wait*/}
    
}



bool ValidMove(int position)
{
    return board[position] != x && board[position] != o && position >= 0 && position < board.Length;
}



string? CheckBoardState()
{
    if (board.All(cell => !char.IsDigit(cell[0])))
    {
        return "tie";
    }
    
    // Define all winning combinations as arrays of board indices
    int[][] winningPatterns =
    [
        // Rows
        [0, 1, 2],
        [3, 4, 5], 
        [6, 7, 8],
        
        // Columns  
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        
        // Diagonals
        [0, 4, 8],
        [2, 4, 6]
    ];

    foreach (var pattern in winningPatterns)
    {
        // gets the value in the gameboard's square that corresponds to the location listed in the set of winning patterns.
        var first = board[pattern[0]];
        var second = board[pattern[1]];
        var third = board[pattern[2]];
        
        //If the first char is a digit, means that space hasn't been set.
        //We're looking for same values, so we don't need to check the others.
        if (char.IsDigit(first[0]))
        {   
            continue;
        }
        // If all three positions have the same value
        if (first == second && second == third)
        {
            return first; // Return the winner ("x" or "o")
        }
    }

    return null;
}

void NewGameScreen()
{
    ConsolePainter.DrawBox(40, 11);
    
    Console.ResetColor();
    
    Console.SetCursorPosition(5, 2);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Welcome to Tic-Tac-Toe!");
    Console.ResetColor();

    Console.SetCursorPosition(5, 2 + 2);
    Console.Write("Press [Enter] to begin.");
    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { /*Do nothing while we wait*/}
    
    state = GameState.XTurn;
    
}

void GameOverScreen()
{
    ConsolePainter.DrawBox(40, 11);
    
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.SetCursorPosition(5, 2 + 2);
    
    Console.WriteLine("Would you like to play again? Y/N");
    ConsoleKeyInfo keyInfo;
    do
    {
        keyInfo = Console.ReadKey(true);
    } while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N);

    if (keyInfo.Key == ConsoleKey.Y)
    {
        Console.Clear();
        Initialize();
    }
    else if (keyInfo.Key == ConsoleKey.N)
    {
        Console.Clear();
        state = GameState.GameClose;
    }

    Console.ResetColor();
}
