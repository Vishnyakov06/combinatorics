using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class EquationSolver
{
    public static List<int[]> GenerateSolutions()
    {
        List<int[]> solutions = new List<int[]>();
        int[] x = new int[7];
        Generate(solutions, x, 0, 60);
        return solutions;
    }
    private static void Generate(List<int[]> solutions, int[] currentX, int level, int remainingSum)
    {
        if (level == 7)
        {
            if (remainingSum == 0)
            {
                if (currentX[0] < 9 &&
                    currentX[1] > 8 &&
                    currentX[2] < 7 &&
                    currentX[3] > 6 &&
                    currentX[4] < 5 &&
                    currentX[5] > 3 &&
                    currentX[6] < 3)
                    solutions.Add((int[])currentX.Clone());
            }
            return;
        }
        int start = 0;
        int end = remainingSum;
        if (level == 0)
        {
            start = 0;
            end = Math.Min(remainingSum, 8);
        }
        if (level == 1)
        {
            start = 9;
            end = remainingSum;
        }
        if (level == 2)
        {
            start = 0;
            end = Math.Min(remainingSum, 6);
        }
        if (level == 3)
        {
            start = 7;
            end = remainingSum;
        }
        if (level == 4)
        {
            start = 0;
            end = Math.Min(remainingSum, 4);
        }
        if (level == 5)
        {
            start = 4;
            end = remainingSum;
        }
        if (level == 6)
        {
            start = 0;
            end = Math.Min(remainingSum, 2);
        }
        for (int i = start; i <= end; i++)
        {
            currentX[level] = i;
            Generate(solutions, currentX, level + 1, remainingSum - i);
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            List<int[]> solutions = EquationSolver.GenerateSolutions();
            string outputFile = "words.txt";
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (int[] solution in solutions)
                    writer.WriteLine(string.Join(", ", solution));
                writer.WriteLine($"Количество решений: {solutions.Count}");
            }
            Console.WriteLine($"Решения записаны в файл: {outputFile}");
        }
    }
}