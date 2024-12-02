/*
 * 1. lese inn listene riktig
 * 
 * 2. sortere listene.
 * 3. absolut verdi (liste a - liste b)
 * 4. plusse sammen alle verdiene
 * 
 * Oppgave 2:
 * 1. Lese inn listene riktig
 * 2. Sortere listene
 * 3. velge et tall fra venstre liste.
 * 4. finne hvor mange like tall det er i høyre liste
 * 5. repeter 3 til ferdig
 */
using System.IO;
using Day1Task1;
void ReadFile(string filepath)
{
    string line;
    int i = 0;
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
            string[] tokens = line.Split("   ");

            //Parse element 0
            int a = int.Parse(tokens[0]);
            Data.list[i] = a;
            

            //Parse element 1
            int b = int.Parse(tokens[1]);
            Data.list2[i] = b;
            i++;
            //Read the next line
            line = sr.ReadLine();
        }
        //close the file
        sr.Close();
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

void GetValue()
{
    int SumOfDiffs = 0;
    for (int i = 0; i < Data.list.Length; i++)
    {
        SumOfDiffs += Math.Abs(Data.list[i] - Data.list2[i]);
    }
    Console.WriteLine("Part 1 = " + SumOfDiffs);
        
        
}

void GetSimilarities(int[] liste1, int[] liste2)
{
    int ProductOfSimilarities = 0;
    for (int i = 0; i < liste1.Length; i++)
    {
        ProductOfSimilarities += GetNumber(liste1[i], liste2) * liste1[i];
    }

    Console.WriteLine("Part 2 = " + ProductOfSimilarities);

}

int GetNumber(int a, int[] liste2)
{
    int count = 0;
    for (int i = 0; i < liste2.Length; i++)
    {
        if (a == liste2[i])
            count++;
    }

    return count;
}

void ArrangeArray(int[] array)
{
   Array.Sort(array);
}

ReadFile("C:\\Users\\robin\\source\\repos\\AdventOfCode\\Day1Task1\\example.txt");
ArrangeArray(Data.list);
ArrangeArray(Data.list2);
GetValue();
GetSimilarities(Data.list, Data.list2);