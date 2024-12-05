int total = 0;
void ReadFile(string filepath)
{
    string line;

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

            string nextline = line;

            while (nextline != null)
                nextline = GetMulls(nextline);
            
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

string GetMulls(string line)
{
    int first = line.IndexOf("mul(") + "mul(".Length;
    if (first == 3)
        return null;
    var subline = line.Substring(first, line.Length - first);
    string[] newstring = subline.Split(',');
    string[] newstring2 = newstring[1].Split(')');
    if (!(newstring[0].Contains(" ") || newstring2[0].Contains(" ")))
    {
        int i = 0;
        bool result1 = int.TryParse(newstring[0], out i);
        bool result2 = int.TryParse(newstring2[0], out i);
        if ((result1 && result2))
        {
            total += (int.Parse(newstring[0]) * int.Parse(newstring2[0]));
        }
            
        
    }
        

    return subline;
}

ReadFile("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day3\\String.txt");