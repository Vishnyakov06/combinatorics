
using System.Reflection.Metadata.Ecma335;

public class Combinatorica
{
    public List<string> Ce(char[] alphabet, int k, int n)
    {
        Stack<char> stack = new Stack<char>();
        List<string> combinations = new List<string>();
        void Backtrack(Stack<char> currentCombination)
        {
            if (currentCombination.Count == k)
            {
                combinations.Add(string.Join("", currentCombination.Reverse()));
                return;
            }
            for (int i = 0; i < n; i++)
            {
                currentCombination.Push(alphabet[i]);
                Backtrack(currentCombination); // Рекурсивный вызов с обновленным стеком
                currentCombination.Pop(); // Откат для следующего варианта
            }
        }
        Backtrack(stack);
        return combinations;
    }
    public class Program()
    {
        static void Main(string[] args)
        {
            Combinatorica e = new Combinatorica();
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k' };
            int n = alphabet.Length;

            Console.WriteLine("Введите длину слова:");
            int wordLength = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите значение k:");
            int k = Convert.ToInt32(Console.ReadLine());
            string outputFile = "words.txt";
            
            if (n > 3 * k + 11) Console.WriteLine("При таких данных данная задача не может быть решена");
            else if (n < 2 * k + 3) Console.WriteLine("При таких данных данная задача не может быть решена");
            else if (k < 0 || n < 0) Console.WriteLine("При таких данных данная задача не может быть решена");
            else
            {
                List<string> allCombinations = e.Ce(alphabet, wordLength, n);
                List<string> validWords = new List<string>();
                foreach (string combination in allCombinations)
                {
                    Dictionary<char, int> charCounts = new Dictionary<char, int>();
                    foreach (char c in combination)
                    {
                        if (charCounts.ContainsKey(c))
                            charCounts[c]++;
                        else
                            charCounts[c] = 1;
                    }
                    int kOrLessCount = 0;
                    int kPlusOneCount = 0;
                    int kPlusTwoOrThreeCount = 0;
                    int oneCount = 0;
                    /*kOrLessCount: Считает количество букв, повторяющихся не более k раз.
                    kPlusOneCount: Считает количество букв, повторяющихся ровно k+1 раз.
                    kPlusTwoOrThreeCount: Считает количество букв, повторяющихся либо k+2, либо k+3*/
                    foreach (int count in charCounts.Values)
                    {
                        if (count <= k) { kOrLessCount++;
                            if (kOrLessCount == 2) kOrLessCount--;
                        }
                        else if (count == k + 1) kPlusOneCount++;
                        else if (count == k + 2 || count == k + 3) kPlusTwoOrThreeCount++;
                        else if (count == 1) oneCount++;
                        else
                        {
                            kOrLessCount = -1;
                            break;
                        }
                    }

                    if (kOrLessCount == 1 && kPlusOneCount == 1 && kPlusTwoOrThreeCount == 1)
                        validWords.Add(combination);
                }

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    foreach (string word in validWords)
                    {
                        writer.WriteLine(word);
                    }
                    writer.WriteLine($"Количество слов: {validWords.Count}");
                }
                Console.WriteLine($"Слова записаны в файл: {outputFile}");

            }
        }
    }
}


