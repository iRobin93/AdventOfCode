/*
 * 1. Lese inn regler i et Map
 * 2. Lese inn en og en linje fra updates - 
 *    Vi leser inn en linje med split funksjon inn i en array av string - må gjøres om til array av int
 *    Sjekke om de er gyldige - while: Hvis tallet er det første i mapet så skal vi feile hvis det andre tallet er foran på linja, hvis tallet er andre i mapet skal vi feile hvis det første tallet er etter på linja
 *    Hvis gyldig, ta vare på tallet i midten
 * 
 * 
 * 
 * Part 2: 
 * 1. For alle linjer som feiler: for hvert tall i pageNumbers flytt første tall som skal være foran i henhold til regel foran i newPageNumbers.
 * 
 */

List<KeyValuePair<int, int>> rulePairs = new List<KeyValuePair<int, int>>();
int total = 0;
int total2 = 0;
void ReadFileRules(string filepath)
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
            //Read line, and split it by whitespace into an array of strings
            int[] pageNumbers = Array.ConvertAll(line.Split('|'), int.Parse);
            rulePairs.Add(new KeyValuePair<int, int>(pageNumbers[0], pageNumbers[1]));

            //Read the next line
            line = sr.ReadLine();
        }
        //close the file
        sr.Close();
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

void ReadFileUpdates(string filepath)
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
            //Read line, and split it by whitespace into an array of strings
 
            var pageNumberArray = line.Split(',');
            int[] pageNumbers = Array.ConvertAll(pageNumberArray, int.Parse);

            for (int i = 0; i < pageNumbers.Length; i++) 
            {
                foreach (var rulePair in rulePairs) 
                {
                    if (pageNumbers[i] == rulePair.Key)
                    {
                        if (!CheckIfKeyValid(pageNumbers, rulePair.Value, i))
                        {
                            
                               total2 += MakeValid(pageNumbers);
                            goto End;
                        }
                            
                    }
                    else if (pageNumbers[i] == rulePair.Value)
                    {
                        if (!CheckIfValueIsValid(pageNumbers, rulePair.Key, i))
                        {
                            
                                total2 += MakeValid(pageNumbers);
                            goto End;
                        }
                            
                    }

                }
            }
            //Finn miderste verdi.
            total += pageNumbers[(int)Math.Ceiling(((decimal)pageNumbers.Length / 2) - 1)];
End:

            //Read the next line
            line = sr.ReadLine();
        }
        //close the file
        sr.Close();
        Console.WriteLine("Total part 1: " + total);
        Console.WriteLine("Total part 2: " + total2);
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

int MakeValid(int[] pageNumbers)
{
    var nullvariabel = new KeyValuePair<int, int>(0, 0);
    for (int i = 0; i < pageNumbers.Length; i++)
    {
        for (int j = i + 1; j < pageNumbers.Length; j++)
        {
            if (!(rulePairs.Find(rulePair => rulePair.Key == pageNumbers[j] && rulePair.Value == pageNumbers[i])).Equals(nullvariabel))
            {
                int tempVar = pageNumbers[i];
                pageNumbers[i] = pageNumbers[j];
                pageNumbers[j] = tempVar;
                i--;
                break;
            }
        }
            
    }
    return pageNumbers[(int)Math.Ceiling(((decimal)pageNumbers.Length / 2) - 1)];
}

bool CheckIfValueIsValid(int[] pageNumbers, int key, int index)
{
    if (index == pageNumbers.Length - 1) return true;
    for (int i = index + 1; i < pageNumbers.Length; i++)
        if (pageNumbers[i] == key)
        { return false; }
    return true;
}

bool CheckIfKeyValid(int[] pageNumbers, int value, int index)
{

    //   
    //   65|51
    //KeyPair
    //51,64,65,16,71
    for (int i = 0; i < index; i++)
        if (pageNumbers[i] == value)
            { return false; }
    return true;
}



ReadFileRules("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day5\\Rules.txt");
ReadFileUpdates("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day5\\Updates.txt");


