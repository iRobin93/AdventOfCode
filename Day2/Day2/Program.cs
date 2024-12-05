using static System.Runtime.InteropServices.JavaScript.JSType;

void ReadFile(string filepath, bool part2)
{
    string line;
    int total = 0;
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
            string[] tokens = line.Split(" ");
            
            total += CheckSafety(tokens, part2);
 
            //Read the next line
            line = sr.ReadLine();
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
}

int CheckSafety(string[] token, bool OneErrorAllowed)
{

    bool GoesDown;
    if (int.Parse(token[0]) > int.Parse(token[1]))
    {
        GoesDown = true;

    }
    else GoesDown = false;

    for (int i = 0; i < token.Length - 1; i++)
    {
    if (GoesDown)
        {
            if (int.Parse(token[i]) <= int.Parse(token[i + 1]))
            {
                if (OneErrorAllowed)
                    return TryAgain(token, i, true);
                
                return 0;
            }
                
        }

    else
        {
            if (int.Parse(token[i]) >= int.Parse(token[i + 1]))
            {
                if (OneErrorAllowed)
                    return TryAgain(token, i, true);
                return 0;
            }
        }

        if ((Math.Abs(Int32.Parse(token[i]) - Int32.Parse(token[i + 1])) > 3) || (Math.Abs(Int32.Parse(token[i]) - Int32.Parse(token[i + 1])) == 0))
        {
            if (OneErrorAllowed)
                return TryAgain(token, i, true);
            return 0;
        }
            
            
    }

 
        
    return 1;
}

int TryAgain(string[] token, int i, bool firstLevel)
{
    {
        string[] newToken = new string[token.Length - 1];
        for (int j = 0; j < newToken.Length; j++)
            if (j >= i)
                newToken[j] = token[j + 1];
            else newToken[j] = token[j];
         int safe = CheckSafety(newToken, false);

        if (safe == 0 && firstLevel)
        {
            safe = TryAgain(token, i + 1, false);

            if (safe == 0)
            {
                safe = TryAgain(token, i - 1, false);
                return safe;
            }
            else return safe;

        }      
        else return safe;

          

    }
}

ReadFile("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day2\\Day2\\path.txt", false);
ReadFile("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day2\\Day2\\path.txt", true);