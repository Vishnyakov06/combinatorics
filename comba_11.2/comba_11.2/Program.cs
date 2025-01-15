using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SetPartitionerPythonic
{
    public static IEnumerable<List<HashSet<char>>> PartitionSet(HashSet<char> set, int numPartitions)
    {
        return PartitionSetRecursive(set, numPartitions);
    }
    private static IEnumerable<List<HashSet<char>>> PartitionSetRecursive(HashSet<char> set, int numPartitions, List<HashSet<char>> currentPartition = null)
    {
        if (currentPartition == null)
        {
            currentPartition = new List<HashSet<char>>();
        }
        if (numPartitions == 0)
        {
            if (set.Count == 0)
            {
                yield return currentPartition;
            }
            yield break;
        }
        for (int i = 1; i <= set.Count - numPartitions + 1; ++i)
        {

            foreach (var firstSubset in Combinations(set, i))
            {
                HashSet<char> remainingSet = new HashSet<char>(set.Except(firstSubset));
                foreach (var rest in PartitionSetRecursive(remainingSet, numPartitions - 1, new List<HashSet<char>>(currentPartition) { new HashSet<char>(firstSubset) }))
                {
                    yield return rest;
                }
            }
        }
    }
    private static IEnumerable<HashSet<char>> Combinations(HashSet<char> set, int k)
    {

        if (k == 0)
        {
            yield return new HashSet<char>();
            yield break;
        }
        if (k > set.Count)
        {
            yield break;
        }
        char[] setArray = set.ToArray();
        int[] indices = Enumerable.Range(0, k).ToArray();


        while (true)
        {
            HashSet<char> result = new HashSet<char>();
            for (int i = 0; i < indices.Length; ++i)
            {
                result.Add(setArray[indices[i]]);
            }
            yield return result;

            int j = k - 1;
            while (j >= 0 && indices[j] == setArray.Length - k + j)
                j--;

            if (j < 0)
                break;

            indices[j]++;
            for (int l = j + 1; l < k; ++l)
            {
                indices[l] = indices[j] + l - j;
            }
        }

    }
    public static void WritePartitionsToFile(IEnumerable<List<HashSet<char>>> partitions, string filename = "output.txt")
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var partition in partitions)
            
                writer.WriteLine(string.Join(" ", partition.Select(set => "{" + string.Join(",", set) + "}")));
            
        }
    }
    public static void Main(string[] args)
    {
        HashSet<char> A = new HashSet<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k' };
        int k = 5;
        string filename = "comb11_02.txt";
        var partitions = PartitionSet(A, k);
        WritePartitionsToFile(partitions, filename);
        Console.WriteLine($"All partitions have been written to the file '{filename}'");
        Console.WriteLine($"Total number of partitions: {partitions.Count()}");
    }
}