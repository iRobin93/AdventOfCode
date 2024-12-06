int total = 0;
bool lastLineDo = true;
bool lastDont = false;

void ReadFile(string filepath, bool exludeDonts)
{
    string line;
    string newNextLine = "";
    lastLineDo = true;
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
            newNextLine = nextline;
            lastDont = false;
            if (exludeDonts)
                do
                {
                    nextline = newNextLine;
                    newNextLine = RemoveTextAfterDont(nextline);
                }while (nextline != newNextLine);
                    
            while (nextline != null)
            {
                nextline = GetMulls(nextline);         
            }


                
            
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


string RemoveTextAfterDont(string line)
{
    int firstdont = -1;
    int firstdo = -1;
    string lineBeforeDont = "";
    string afterDont = "";
    string lineAfterDo = "";
    string returnString = "";
    if (lastLineDo)
    {
        firstdont = line.IndexOf("don't()") + "don't()".Length;
        if (firstdont == 6)
            return line;
        lastLineDo = false;
        lineBeforeDont = line.Substring(0, firstdont - "don't()".Length);
        afterDont = line.Substring(firstdont, line.Length - firstdont);
        firstdo = afterDont.IndexOf("do()") + "do()".Length;
        if (firstdo == 3)
        {
            lastDont = true;
            return lineBeforeDont;
        }
            
        lastLineDo = true;
        lineAfterDo = afterDont.Substring(firstdo, afterDont.Length - firstdo);
        returnString = lineBeforeDont + lineAfterDo;
    }
    
   else
    {
        if (lastDont)
            return line;
        firstdo = line.IndexOf("do()") + "do()".Length;
        if (firstdo == 3)
            return "";
        lastLineDo = true;
        lineAfterDo = line.Substring(firstdo, line.Length - firstdo);
        returnString = lineAfterDo;
    }



    //25122114
    //50175920
    //70478672



    return returnString;
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

ReadFile("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day3\\String.txt", false);
total = 0;
ReadFile("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day3\\String.txt", true);