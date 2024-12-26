/* Strategi
 * 
 * 1. Lese inn til en string array.
 * 2. Lete etter starten.
 * 3. Lage funksjonene gå mot nord - øst - sør og vest - i funksjonen lage en x der du har gått.
 * 4. når man treffer havner utenfor mapet er spillet slutt.
 * 5. tell opp antall x'er
 * 
 * 
 * 
 * Del 2:
 *  For hvert felt vakten kommer til, sett en 0
 *  sjekk om det er en evig loop, måten vi sjekker er om han møter 0'en en gang til med samme direction
 *  hvis ikke går vi til han går ut av spillet.
 */


using System.Security;
using System.Security.AccessControl;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

string direction = "north";
int ZeroHit = 0;
int infinityLoops = 0;
string lastZeroDirection = "north";

void ReadFile(string filepath, bool part2)
{
    int total = 0;
    string line;
    int i = 0;
    List<string> rowsList = new List<string>();
    int[] currentPos = new int[2];
    int[] startPos = new int[2];
    int[] lastZeroPos = new int[2];
    int numberOfWalks = 0;
    List<string> earlierZeroes = new List<string>();
    
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
            rowsList.Add(line);


            //Read the next line
            line = sr.ReadLine();
          
        }
        //close the file
        sr.Close();
        FindStart(rowsList, currentPos);
        lastZeroPos[0] = currentPos[0];
        lastZeroPos[1] = currentPos[1];
        startPos[0] = currentPos[0];
        startPos[1] = currentPos[1];
        if (part2)
        {
            AddZero(rowsList, lastZeroPos, lastZeroDirection);
            while (lastZeroPos[0] != -1)
            {
                Console.WriteLine(lastZeroPos[0]);
                Console.WriteLine(lastZeroPos[1]);

                currentPos[0] = startPos[0];
                currentPos[1] = startPos[1];
                direction = "north";
                numberOfWalks = 0;
                
                while (currentPos[0] != -1)
                {
                    
                    numberOfWalks++;
                    Walk(rowsList, currentPos);
                    if (numberOfWalks == 10000)
                    {
                        string lastZeroPosString = ("00" + lastZeroPos[0].ToString()) + ("00" + lastZeroPos[1].ToString());
                        if (earlierZeroes.Contains(lastZeroPosString))
                            break;
                        infinityLoops++;
                        earlierZeroes.Add(lastZeroPosString);
                        break;
                    }
                }
                ZeroHit = 0;
                RemoveZero(rowsList, lastZeroPos);
                AddZero(rowsList, lastZeroPos, lastZeroDirection);
            }
            
           
        }
        else    
            while (currentPos[0] != -1)
        {
            Walk(rowsList, currentPos);
                
        }
        
        total = CountXes(rowsList);
        if(!part2)
            Console.WriteLine("Total i part 1 er: " + total);
        else
        {
            Console.WriteLine("Infinity loops er: " + infinityLoops);
            Console.ReadLine();
        }
           
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: " + e.Message);
    }
}

void RemoveZero(List<string> rowsList, int[] lastZeroPos)
{
    Console.WriteLine("Remove 0");
    AddCharacter(rowsList, lastZeroPos, '.');

}

void ZeroWalkNorth(List<string> rowsList, int[] lastZeroPos)
{
    Console.WriteLine("Checking North");
    var nextNorthChar = rowsList[lastZeroPos[0] - 1][lastZeroPos[1]];
    Console.WriteLine(nextNorthChar);
    if (nextNorthChar == '#' || nextNorthChar == '0')
    {
        lastZeroDirection = "east";
        ZeroWalkEast(rowsList, lastZeroPos);
        return;
    }
        
    lastZeroPos[0] = lastZeroPos[0] - 1;
    if (lastZeroPos[0] == -1)
        lastZeroPos[1] = -1;
    else
        AddCharacter(rowsList, lastZeroPos, '0');
}

void ZeroWalkEast(List<string> rowsList, int[] lastZeroPos)
{
    Console.WriteLine("Checking East");
    var nextEastChar = rowsList[lastZeroPos[0]][lastZeroPos[1] + 1];
    Console.WriteLine(nextEastChar);
    if (nextEastChar == '#' || nextEastChar == '0')
    {
        lastZeroDirection = "south";
        ZeroWalkSouth(rowsList, lastZeroPos);
        return;
    }
        
    lastZeroPos[1] = lastZeroPos[1] + 1;
    if (lastZeroPos[1] > rowsList[lastZeroPos[0]].Length - 1)
    {
        lastZeroPos[0] = -1;
        lastZeroPos[1] = -1;
    }
    else
        AddCharacter(rowsList, lastZeroPos, '0');
}

void ZeroWalkSouth(List<string> rowsList, int[] lastZeroPos)
{
    Console.WriteLine("Checking South");
    if (lastZeroPos[0] < rowsList.Count - 1)
    {

        var nextSouthChar = rowsList[lastZeroPos[0] + 1][lastZeroPos[1]];
        Console.WriteLine(nextSouthChar);
        if (nextSouthChar == '#' || nextSouthChar == '0')
        {
            lastZeroDirection = "west";
            ZeroWalkWest(rowsList, lastZeroPos);
            return;
        }
    }
        
    lastZeroPos[0] = lastZeroPos[0] + 1;
    Console.WriteLine("Last zero pos er: " + lastZeroPos[0]);
    Console.WriteLine("RowslistCount -1 er" + (rowsList.Count - 1));
    if (lastZeroPos[0] > rowsList.Count - 1)
    {
        lastZeroPos[0] = -1;
        lastZeroPos[1] = -1;
        return;
    }
    AddCharacter(rowsList, lastZeroPos, '0');
}

void ZeroWalkWest(List<string> rowsList, int[] lastZeroPos)
{
    Console.WriteLine("Checking West");
    if (lastZeroPos[1] > 0)
    {
        var nextWestChar = rowsList[lastZeroPos[0]][lastZeroPos[1] - 1];
        Console.WriteLine(nextWestChar);
        if (nextWestChar == '#' || nextWestChar == '0')
        {
            lastZeroDirection = "north";
            ZeroWalkNorth(rowsList, lastZeroPos);
            return;
        }


    }
    lastZeroPos[1] = lastZeroPos[1] - 1;
    if (lastZeroPos[1] == -1)
        lastZeroPos[0] = -1;

    else
    AddCharacter(rowsList, lastZeroPos, '0');

}

void AddZero(List<string> rowsList, int[] lastZeroPos, string lastZeroDirection)
{
    switch (lastZeroDirection) 
    {
        
        case "north":
            ZeroWalkNorth(rowsList, lastZeroPos);
            break;
        case "east":
            ZeroWalkEast(rowsList, lastZeroPos);
            break;
        case "south":
            ZeroWalkSouth(rowsList, lastZeroPos);
            break;
        case "west":
            ZeroWalkWest(rowsList, lastZeroPos);
            break;
    }
}

void FindStart(List<string> rowsList, int[] currentPos)
{
    for (int i = 0; i < rowsList.Count; i++)
    {
        for (int j = 0; j < rowsList[i].Length; j++)
        {
            if (rowsList[i][j] == '^')
            {
                currentPos[0] = i;
                currentPos[1] = j;
                return;
            }
                
        }

    }
}

void Walk(List<string> rowsList, int[] currentPos)
{
    if (direction == "north")
        WalkNorth(rowsList, currentPos);
    else if (direction == "east")
        WalkEast(rowsList, currentPos);
    else if (direction == "south")
        WalkSouth(rowsList, currentPos);
    else 
        WalkWest(rowsList, currentPos);
}
void WalkNorth(List<string> rowsList, int[] currentPos)
{
    if (currentPos[0] > 0)
    {
        var nextNorthChar = rowsList[currentPos[0] - 1][currentPos[1]];
        if (nextNorthChar == '#' || nextNorthChar == '0')
        {
            if (nextNorthChar == '0')
            {
                ZeroHit++;
                
            }
            direction = "east";
            return;
        }
    }


    AddCharacter(rowsList, currentPos, 'X');

    currentPos[0] = currentPos[0] - 1;
    if (currentPos[0] == -1)
        currentPos[1] = -1;

}

void WalkEast(List<string> rowsList, int[] currentPos) 
{
    if (currentPos[1] < rowsList[currentPos[0]].Length - 1)
    {
        var nextEastChar = rowsList[currentPos[0]][currentPos[1] + 1];
        if (nextEastChar == '#' || nextEastChar == '0')
        {
            if (nextEastChar == '0')
            {
                ZeroHit++;
            }
            direction = "south";
            return;
        }
    }



    AddCharacter(rowsList, currentPos, 'X');

    currentPos[1] = currentPos[1] + 1;
    if (currentPos[1] > rowsList[currentPos[0]].Length - 1)
    {
        currentPos[0] = -1;
        currentPos[1] = -1;
    }
        
}

void WalkSouth(List<string> rowsList, int[] currentPos)
{
    if (currentPos[0] < rowsList.Count - 1)
    {
        var nextSouthChar = rowsList[currentPos[0] + 1][currentPos[1]];
        if (nextSouthChar == '#' || nextSouthChar == '0')
        {
            if (nextSouthChar == '0')
            {
                ZeroHit++;
            }
            direction = "west";
            return;
        }
    }

    AddCharacter(rowsList, currentPos, 'X');

    currentPos[0] = currentPos[0] + 1;
    if (currentPos[0] > rowsList.Count - 1)
    {
        currentPos[0] = -1;
        currentPos[1] = -1;
    }

}

void WalkWest(List<string> rowsList, int[] currentPos)
{
    if (currentPos[1] > 0)
    {
        var nextWestChar = rowsList[currentPos[0]][currentPos[1] - 1];
        if (nextWestChar == '#' || nextWestChar == '0')
        {
            if (nextWestChar == '0')
            {
                ZeroHit++;
            }
            direction = "north";
            return;
        }
    }



    AddCharacter(rowsList, currentPos, 'X');

    currentPos[1] = currentPos[1] - 1;
    if (currentPos[1] == -1)
        currentPos[0] = -1;
}

int CountXes(List<string> rowsList)
{
    int count = 0;
    for (int i = 0; i < rowsList.Count; i++)
        for (int j = 0; j < rowsList[i].Length; j++)
            if (rowsList[i][j] == 'X')
                count++;
    return count;
}


ReadFile("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day6\\Day6\\path.txt", false);
ReadFile("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day6\\Day6\\path.txt", true);

static void AddCharacter(List<string> rowsList, int[] currentPos, char characterToAdd)
{
    StringBuilder stringCopy = new StringBuilder(rowsList[currentPos[0]]);
    stringCopy[currentPos[1]] = characterToAdd;
    rowsList[currentPos[0]] = stringCopy.ToString();
}