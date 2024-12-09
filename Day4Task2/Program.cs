/*
 * Finne en a på rad 2 og så kryss sjekke om det er m og s diagonalt begge veier
 * 
 * 
 * 
 */



int total = 0;

void ReadFile(string filepath)
{
    string line;
    string[] lineArray = new string[3];
    try
    {
        //Pass the file path and file name to the StreamReader constructor
        StreamReader sr = new StreamReader(filepath);
        //Read the first line of text
        line = sr.ReadLine();
        //Continue to read until you reach end of file
            for (int i = 0; i < 3; i++)
            {
                lineArray[i] = line;
                if (i < 2) line = sr.ReadLine();
            }

        while (line != null)
        {
            //Run code here
            total += GetMas(lineArray);

            //Read the next line
            line = sr.ReadLine();     
            lineArray[0] = lineArray[1];
            lineArray[1] = lineArray[2];
            lineArray[2] = line;
            


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


int GetMas(string[] lineArray){
    int count = 0;

    for (int i = 1; i < lineArray[1].Length - 1; i++) 
    {
        if (lineArray[1][i] == 'A')
            if ((CheckDiagonalDownRight(lineArray, i) || CheckDiagonalUpLeft(lineArray, i)) && (CheckDiagonalDownLeft(lineArray, i) || CheckDiagonalUpRight(lineArray, i)))
                count++;

    }


    return count;
}



bool CheckDiagonalDownLeft(string[] lineArray, int i)
{
    return (lineArray[2][i - 1] == 'S' && lineArray[0][i + 1] == 'M');
}

bool CheckDiagonalUpRight(string[] lineArray, int i)
{
    return (lineArray[2][i - 1] == 'M' && lineArray[0][i + 1] == 'S');
}

bool CheckDiagonalUpLeft(string[] lineArray, int i)
{
    return (lineArray[0][i - 1] == 'S' && lineArray[2][i + 1] == 'M');
}


bool CheckDiagonalDownRight(string[] lineArray, int i)
{
    return (lineArray[0][i - 1] == 'M' && lineArray[2][i + 1] == 'S');
}

ReadFile("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day4\\path.txt");
