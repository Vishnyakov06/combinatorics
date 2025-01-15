using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PermutationGenerator
{
     public static bool NextSet(char[] a)
        {
            int n = a.Length;
            int j = n - 2;
            while (j != -1 && a[j] >= a[j + 1]) j--;
            if (j == -1)
               return false;
            int k = n - 1;
            while (a[j] >= a[k]) k--;
            Swap(a, j, k);
            int l = j + 1, r = n - 1;
            while (l < r)
                Swap(a, l++, r--);
            return true;
       }

      private static void Swap(char[] a, int i, int j)
      {
         (a[i], a[j]) = (a[j], a[i]);
      }

    public static void GeneratePermutations(string word, string outputFile)
    {
        char[] chars = word.ToCharArray();
        Array.Sort(chars);
        HashSet<string> uniquePermutations = new HashSet<string>();

        do
        {
            uniquePermutations.Add(new string(chars));
        } while (NextSet(chars));

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            foreach (string permutation in uniquePermutations)
            {
                writer.WriteLine(permutation);
            }
             writer.WriteLine($"Количество перестановок: {uniquePermutations.Count}");
        }
         Console.WriteLine($"Перестановки записаны в файл: {outputFile}");
    }
    public class Program
    {
        static void Main(string[] args)
        {
            string word = "ABCDD";
            string outputFile = "permutations.txt";
           PermutationGenerator.GeneratePermutations(word, outputFile);
        }
    }
}