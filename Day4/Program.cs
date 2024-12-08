/*
 * 1. Finne :
 * XMAS:
 *  A: Horisontal mot høyre *
 *  B: Horisontal mot venstre *
 *  C: Vertical ned *
 *  D: Vertical opp *
 *  E: På kryss høyre nedover *
 *  F: På kryss høyre oppover *
 *  G: På kryss venstre nedover *
 *  H: På kryss venstre oppover *
 *  
 *  
 *  2. Strategi:
 *  a og b: en og en linje
 *  starte med de fire første - så gå en og en til høyre - finne xmas eller samx
 *  resten: lese inn fire og fire linjer hvor man går ned en linje om gangen
 */

int total = 0;

void ReadFile(string filepath, bool readOneAndOne)
{
    string line;
    string[] lineArray = new string[4];
    try
    {
        //Pass the file path and file name to the StreamReader constructor
        StreamReader sr = new StreamReader(filepath);
        //Read the first line of text
        line = sr.ReadLine();
        //Continue to read until you reach end of file
        if (!readOneAndOne)
            for (int i = 0; i < 4; i++)
            {
                lineArray[i] = line;
                if (i<3) line = sr.ReadLine();
            }
                
        while (line != null)
        {
            if (readOneAndOne)
                total += GetHorisontal(line);
            else
            {
                
                
                
                total += GetVerticals(lineArray);

            }
                
                
            //Read the next line
            line = sr.ReadLine();
            if (!readOneAndOne)
            {
                lineArray[0] = lineArray[1];
                lineArray[1] = lineArray[2];
                lineArray[2] = lineArray[3];
                lineArray[3] = line;
            }
            

        }
        //close the file
        sr.Close();
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

    return;
}

int GetVerticals(string[] lineArray)
{
    int count = 0;
    count += GetVerticalDown(lineArray);
    count += GetVerticalUp(lineArray);
    count += GetVerticalDownRight(lineArray);
    count += GetVerticalDownLeft(lineArray);
    count += GetVerticalUpRight(lineArray);
    count += GetVerticalUpLeft(lineArray);
    return count;
}


int GetVerticalUpLeft(string[] lineArray)
{
    int count = 0;
    for (int i = 3; i < lineArray[0].Length; i++)
        if (lineArray[0][i - 3] == 'S' && lineArray[1][i - 2] == 'A' && lineArray[2][i - 1] == 'M' && lineArray[3][i] == 'X')
            count++;

    return count;
}

int GetVerticalUpRight(string[] lineArray)
{
    int count = 0;
    for (int i = 0; i < lineArray[0].Length - 3; i++)
        if (lineArray[3][i] == 'X' && lineArray[2][i + 1] == 'M' && lineArray[1][i + 2] == 'A' && lineArray[0][i + 3] == 'S')
            count++;

    return count;
}


int GetVerticalDownLeft(string[] lineArray)
{
    int count = 0;
    for (int i = 3; i < lineArray[0].Length; i++)
        if (lineArray[0][i] == 'X' && lineArray[1][i - 1] == 'M' && lineArray[2][i - 2] == 'A' && lineArray[3][i - 3] == 'S')
            count++;

    return count;
}
int GetVerticalDownRight(string[] lineArray)
{
    int count = 0;
    for (int i = 0; i < lineArray[0].Length - 3; i++)
        if (lineArray[0][i] == 'X' && lineArray[1][i+1] == 'M' && lineArray[2][i+2] == 'A' && lineArray[3][i+3] == 'S')
            count++;
    return count;
}




int GetVerticalUp(string[] lineArray)
{
    int count = 0;
    for (int i = 0; i < lineArray[0].Length; i++)
        if (lineArray[0][i] == 'S' && lineArray[1][i] == 'A' && lineArray[2][i] == 'M' && lineArray[3][i] == 'X')
            count++;
    return count;
}

int GetVerticalDown(string[] lineArray)
{
    int count = 0;

    for (int i = 0; i < lineArray[0].Length; i++)
        if (lineArray[0][i] == 'X' && lineArray[1][i] == 'M' && lineArray[2][i] == 'A' && lineArray[3][i] == 'S')
            count++;
    return count;
}

int GetHorisontal(string line)
{
    int count = 0;
    string token = "";
    for (int i = 0; i < line.Length - 3; i++)
    {
        token = line.Substring(i, 4);
        if (token == "XMAS" || token == "SAMX")
            count++;
    }


    return count;
}

void PartOne(string filepath)
{
    ReadFile(filepath, true);
    ReadFile(filepath, false);
}

PartOne("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day4\\path.txt");