using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PermutationGenerator
{
    public static IEnumerable<List<int>> IntPartitions(int n, int k)
    {
        if (k == 0)
        {
            if (n == 0)
                yield return new List<int>();
            yield break;
        }

        if (k == 1)
        {
            if (n > 0)
            {
                yield return new List<int> { n };
            }
            yield break;
        }

        for (int i = 1; i <= n; i++)
        {
            foreach (var part in IntPartitions(n - i, k - 1))
            {
                List<int> result = new List<int> { i };
                result.AddRange(part);
                yield return result;
            }
        }
    }


    public static IEnumerable<List<List<int>>> GenerateCycles(List<int> sizes, List<int> digits)
    {
        if (sizes == null || sizes.Count == 0)
        {
            yield return new List<List<int>>();
            yield break;
        }


        int first = sizes[0];
        List<int> remain = sizes.Skip(1).ToList();


        foreach (var elements in Combinations(digits, first))
        {
            List<int> remainDigits = digits.Except(elements).ToList();
            foreach (var perm in Permutations(elements))
            {
                List<int> cycle = perm.ToList();
                foreach (var rest in GenerateCycles(remain, remainDigits))
                {
                    List<List<int>> result = new List<List<int>> { cycle };
                    result.AddRange(rest);
                    yield return result;
                }
            }
        }
    }

    public static List<int> PartCycles(List<List<int>> cycles)
    {
        int maxDigit = cycles.Count == 0 ? -1 : cycles.SelectMany(cycle => cycle).Max();

        List<int> perm = Enumerable.Range(0, maxDigit + 1).ToList();

        foreach (var cycle in cycles)
        {
            for (int i = 0; i < cycle.Count; i++)
            {
                perm[cycle[i]] = cycle[(i + 1) % cycle.Count];
            }
        }
        return perm;
    }



    public static IEnumerable<List<int>> GeneratePermutations(List<int> digits, int countCycles)
    {

        HashSet<List<int>> allPerm = new HashSet<List<int>>(new ListComparer());
        foreach (var sizes in IntPartitions(digits.Count, countCycles))
        {
            foreach (var cycles in GenerateCycles(sizes, digits))
            {
                var permutation = PartCycles(cycles);
                allPerm.Add(permutation);
            }
        }

        foreach (var perm in allPerm)
        {
            yield return perm;
        }
    }



    public static void WritePartitionsToFile(IEnumerable<List<int>> partitions, string filename = "output.txt")
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            int count = 0;
            foreach (var partition in partitions)
            {
                writer.WriteLine(string.Join(", ", partition));
                count++;
            }
            Console.WriteLine($"All partitions: {count}");
        }
    }


    private static IEnumerable<List<T>> Combinations<T>(List<T> set, int k)
    {
        if (k == 0)
        {
            yield return new List<T>();
            yield break;
        }

        if (k > set.Count)
        {
            yield break;
        }


        T[] setArray = set.ToArray();
        int[] indices = Enumerable.Range(0, k).ToArray();

        while (true)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < indices.Length; i++)
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

            for (int l = j + 1; l < k; l++)
            {
                indices[l] = indices[j] + l - j;
            }

        }
    }

    private static IEnumerable<List<T>> Permutations<T>(List<T> items)
    {
        if (items.Count == 0)
        {
            yield return new List<T>();
            yield break;
        }
        if (items.Count == 1)
        {
            yield return items;
            yield break;
        }


        for (int i = 0; i < items.Count; i++)
        {
            T current = items[i];
            List<T> remaining = items.Where((item, index) => index != i).ToList();

            foreach (var permutation in Permutations(remaining))
            {
                List<T> result = new List<T> { current };
                result.AddRange(permutation);
                yield return result;
            }
        }
    }

    public class ListComparer : IEqualityComparer<List<int>>
    {
        public bool Equals(List<int> x, List<int> y)
        {
            if (x == null || y == null)
                return x == y;

            if (x.Count != y.Count)
                return false;

            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] != y[i])
                    return false;
            }

            return true;
        }

        public int GetHashCode(List<int> obj)
        {
            if (obj == null) return 0;
            int hash = 17;
            foreach (int item in obj)
            {
                hash = hash * 31 + item.GetHashCode();
            }
            return hash;
        }
    }


    public static void Main(string[] args)
    {
        List<int> digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int cyc = 5;
        string filename = "comb11_05.txt";
        var result = GeneratePermutations(digits, cyc);
        WritePartitionsToFile(result, filename);
        /*
        foreach(var part in result.Take(10)) {
             Console.WriteLine(string.Join(",", part));
         }
      */
    }
}