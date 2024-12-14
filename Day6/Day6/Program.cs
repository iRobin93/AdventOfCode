/* Strategi
 * 
 * 1. Lese inn til en string array.
 * 2. Lete etter starten.
 * 3. Lage funksjonene gå mot nord - øst - sør og vest - i funksjonen lage en x der du har gått.
 * 4. når man treffer havner utenfor mapet er spillet slutt.
 * 5. tell opp antall x'er
 * 
 */


using System.Security;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

string direction = "north";

void ReadFile(string filepath)
{
    int total = 0;
    string line;
    int i = 0;
    string[] rowsArray = new string[10];
    int[] currentPos = new int[2];
    try
    {
        //Pass the file path and file name to the StreamReader constructor
        StreamReader sr = new StreamReader(filepath);
        //Read the first line of text
        line = sr.ReadLine();
        //Continue to read until you reach end of file
        while (line != null)
        {
            
            //write the line to console window
            //Read line, and split it by whitespace into an array of strings
            rowsArray[i++] = line;


            //Read the next line
            line = sr.ReadLine();
          
        }
        //close the file
        sr.Close();
        FindStart(rowsArray, currentPos);
        while (currentPos[0] != -1)
            Walk(rowsArray, currentPos);
        total = CountXes(rowsArray);
        Console.WriteLine(total);
        Console.ReadLine();
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: " + e.Message);
    }
    finally
    {
        Console.WriteLine("Executing finally block.");
    }
}

void FindStart(string[] rowsArray, int[] currentPos)
{
    for (int i = 0; i < rowsArray.Length; i++)
    {
        for (int j = 0; j < rowsArray[i].Length; j++)
        {
            if (rowsArray[i][j] == '^')
            {
                currentPos[0] = i;
                currentPos[1] = j;
                return;
            }
                
        }

    }
}

void Walk(string[] rowsArray, int[] currentPos)
{
    if (direction == "north")
        WalkNorth(rowsArray, currentPos);
    else if (direction == "east")
        WalkEast(rowsArray, currentPos);
    else if (direction == "south")
        WalkSouth(rowsArray, currentPos);
    else 
        WalkWest(rowsArray, currentPos);
}
void WalkNorth(string[] rowsArray, int[] currentPos)
{
   
    StringBuilder stringCopy = new StringBuilder(rowsArray[currentPos[0]]);
    stringCopy[currentPos[1]] = 'X';
    rowsArray[currentPos[0]] = stringCopy.ToString();
    
    currentPos[0] = currentPos[0] - 1;
    if (currentPos[0] == -1)
        currentPos[1] = -1;

}

void WalkEast(string[] rowsArray, int[] currentPos) 
{

}

void WalkSouth(string[] rowsArray, int[] currentPos)
{

}

void WalkWest(string[] rowsArray, int[] currentPos)
{

}

int CountXes(string[] rowsArray)
{
    int count = 0;
    for (int i = 0; i < rowsArray.Length; i++)
        for (int j = 0; j < rowsArray[i].Length; j++)
            if (rowsArray[i][j] == 'X')
                count++;
    return count;
}


ReadFile("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day6\\Day6\\path.txt");