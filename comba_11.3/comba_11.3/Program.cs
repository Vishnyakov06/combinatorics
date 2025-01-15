using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class MultisetPartitioner
{
    public static IEnumerable<List<int>> IntegerPartitions(int n, int k, int min = 1, List<int> current = null)
    {
        if (current == null)
        {
            current = new List<int>();
        }

        if (k == 0)
        {
            if (n == 0)
            {
                yield return current;
            }
            yield break;
        }

        for (int i = min; i <= n; i++)
        {
            foreach (var part in IntegerPartitions(n - i, k - 1, i, new List<int>(current) { i }))
                yield return part;
        }
    }



    public static IEnumerable<List<List<char>>> GenerateCharacterPartitions(int n, int k)
    {
        foreach (var intPartition in IntegerPartitions(n, k))
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
                writer.WriteLine(string.Join(" ", partition.Select(sublist => $"{{{string.Join("", sublist)}}}")));
                count++;
            }

            Console.WriteLine($"All partitions: {count}");
        }
    }


    public static void Main(string[] args)
    {
        int n = 10;
        int k = 5;
        string filename = "output.txt";

        var charPartitions = GenerateCharacterPartitions(n, k);


        var uniquePartitions = new HashSet<string>(
         charPartitions.Select(p => string.Join(" ", p.Select(x => "{" + string.Join("", x) + "}")))
         );
        WritePartitionsToFile(uniquePartitions.Select(str => str.Split(" ").Select(x => x.Trim('{', '}').ToList()).ToList()), filename);



    }
}