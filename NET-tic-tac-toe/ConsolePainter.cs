namespace NET_tic_tac_toe;

public class ConsolePainter
{

    //Use negative number to move cursor up or left
    private static void ShiftCursor(int horizontal, int vertical)
    {
        Console.SetCursorPosition(Console.CursorLeft + horizontal,Console.CursorTop + vertical);
    }
    
    //Used to write a character and then stay at that location
    private static void WriteInPlace(string character)
    {
        Console.Write(character); 
        ShiftCursor(-1,0);
    }

    //Use negative number to move cursor up or left
    private static void DrawLine(string character, int length, int horizontal, int vertical)
    {
        for (int i = 0; i < length; i++)
        {
            WriteInPlace(character); 
            ShiftCursor(horizontal,vertical);
        }
    }
    
    public static void DrawGrid(string[] b, int originLeft = 0, int originTop = 0)
    {
        //Hacky, but it works. Will replace with a proper loop later.
        Console.SetCursorPosition(originLeft, originTop);
        Console.Write($" {b[0]} | {b[1]} | {b[2]} ");
        
        Console.SetCursorPosition(originLeft, originTop+1);
        Console.Write("═══════════");
        
        Console.SetCursorPosition(originLeft, originTop+2);
        Console.Write($" {b[3]} | {b[4]} | {b[5]} ");
        
        Console.SetCursorPosition(originLeft, originTop+3);
        Console.Write("═══════════");
        
        Console.SetCursorPosition(originLeft, originTop+4);
        Console.Write($" {b[6]} | {b[7]} | {b[8]} ");
        
        //   X | O | X 
        //  ═══════════
        //   O | X | O 
        //  ═══════════
        //   X | O | X 
    }
    
    public static void DrawBox( int width, int height, int originLeft = 0, int originTop = 0)
    {
        //  ─ ┐ │ └ ┘
        
        originLeft = Math.Max(0, originLeft);
        originTop = Math.Max(0, originTop);
        width = Math.Min(Console.WindowWidth, width);
        height = Math.Min(Console.WindowHeight, height);
        
        //Set origin
        Console.SetCursorPosition(originLeft, originTop);           //Begin in first corner
        WriteInPlace("┌");                                         //Write corner char
        
        ShiftCursor(1, 0);                          //Shift cursor to prepare for the first line
        DrawLine("─", width,1,0);     //Draw line towards the right
        WriteInPlace("┐"); 
        
        ShiftCursor(0,1);                           //Move down
        DrawLine("│",height,0,1);     //Draw line down
        WriteInPlace("┘"); 
        
        ShiftCursor(-1, 0);                         //Move left
        DrawLine("─", width,-1,0);    //Draw line towards the left
        WriteInPlace("└"); 
        
        ShiftCursor(0, -1);                         //Move up
        DrawLine("│", height,0,-1);   //Draw line up
        
    }
}