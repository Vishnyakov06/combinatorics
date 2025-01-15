using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Partition
{
    public static IEnumerable<List<int>> Partitions(int n, int k)
    {
        if (k == 0)
        {
            if (n == 0)
                yield return new List<int>();
            yield break;
        }

        for (int i = 1; i <= n; i++)
        {
            foreach (var part in Partitions(n - i, k - 1))
            {
                List<int> result = new List<int> { i };
                result.AddRange(part);
                yield return result;
            }
        }
    }

    public static IEnumerable<List<List<char>>> GeneratePartitions(int n, int subsets)
    {
        foreach (var intPartition in Partitions(n, subsets))
        {
            List<List<char>> charPartition = intPartition.Select(count => Enumerable.Repeat('a', count).ToList()).ToList();
            yield return charPartition;
        }
    }


    public static void WritePartitionsToFile(IEnumerable<List<List<char>>> partitions, string filename = "output.txt")
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            int count = 0;
            foreach (var partition in partitions)
            {
                writer.WriteLine(string.Join(" ", partition.Select(sublist => $"{{{string.Join(",", sublist)}}}")));
                count++;
            }
            Console.WriteLine($"All partitions: {count}");
        }
    }

    public static void Main(string[] args)
    {
        int n = 10;
        int k = 5;
        string filename = "comb11_04.txt";

        var partitions = GeneratePartitions(n, k);
        WritePartitionsToFile(partitions, filename);
    }
}