using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class SetPartitioner
{
    public static IEnumerable<List<HashSet<char>>> PartitionSet(HashSet<char> s, int k)
    {
        return PartitionSet(s, k, new List<HashSet<char>>(), 0);
    }

    private static IEnumerable<List<HashSet<char>>> PartitionSet(HashSet<char> s, int k, List<HashSet<char>> currentPartition, int index)
    {
        if (index == s.Count)
        {
            if (currentPartition.Count == k)
            {
                yield return currentPartition.Select(set => new HashSet<char>(set)).ToList();
            }
            yield break;
        }

        char element = s.ElementAt(index);

        if (currentPartition.Count < k)
        {
            for (int i = 0; i < currentPartition.Count + 1; i++)
            {
                List<HashSet<char>> newPartition = currentPartition.Select(set => new HashSet<char>(set)).ToList();
                if (i < currentPartition.Count)
                {
                    newPartition[i].Add(element);
                    foreach (var result in PartitionSet(s, k, newPartition, index + 1))
                    {
                        yield return result;
                    }
                }
                else
                {
                    newPartition.Add(new HashSet<char> { element });
                    foreach (var result in PartitionSet(s, k, newPartition, index + 1))
                    {
                        yield return result;
                    }
                }

            }
        }
        else
        {
            for (int i = 0; i < currentPartition.Count; i++)
            {
                List<HashSet<char>> newPartition = currentPartition.Select(set => new HashSet<char>(set)).ToList();
                newPartition[i].Add(element);
                foreach (var result in PartitionSet(s, k, newPartition, index + 1))
                {
                    yield return result;
                }
            }
        }

    }

    public static void WritePartitionsToFile(IEnumerable<List<HashSet<char>>> partitions, string filename = "output.txt")
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var partition in partitions)
            {
                writer.WriteLine(string.Join(" ", partition.Select(set => "{" + string.Join(",", set) + "}")));
            }
        }
    }

    public static void Main(string[] args)
    {
        HashSet<char> A = new HashSet<char> { 'a', 'b', 'c','d','e','f','g','j','h','k'};
        int k = 5;
        string filename = "output.txt";


        var partitions = new HashSet<string>();
        foreach (var partition in PartitionSet(A, k))
        {
            string str = string.Join(" ", partition.Select(set => "{" + string.Join(",", set.OrderBy(c => c)) + "}"));
            partitions.Add(str);

        }

        WritePartitionsToFile(partitions.Select(str => str.Split(" ").Select(x => x.Trim('{', '}')
                                                      .Split(",").Select(y => char.Parse(y)).ToHashSet()).ToList()), filename);
        Console.WriteLine($"Все разбиения записаны в файл '{filename}'");
        Console.WriteLine($"Всего способов: {partitions.Count}");
    }
}