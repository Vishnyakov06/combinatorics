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
            char[] alpabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k' };
            int n = alpabet.Length;
            Console.WriteLine("Введите длину слова:");
            int k = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество повторений для одной буквы:");
            int povtor = Convert.ToInt32(Console.ReadLine());
            if (k < 4 + povtor) Console.WriteLine("При таких данных данная задача не может быть решена");
            else if (k > 4 + povtor + 7) Console.WriteLine("При таких данных данная задача не может быть решена");
            else if (povtor < 0 && k < 0) Console.WriteLine("При таких данных данная задача не может быть решена");
            else
            {
                List<string> allCombinations = e.Ce(alpabet, k, n);
                List<string> goodCombination = new List<string>();
                string outputFile = "words.txt";
                foreach (string combination in allCombinations)
                {
                    Dictionary<char, int> map = new Dictionary<char, int>();
                    foreach (char ch in combination)
                    {
                        if (map.ContainsKey(ch)) map[ch]++;
                        else map[ch] = 1;
                    }
                    int twoCount = 0;
                    int kCount = 0;
                    int oneCount = 0;
                    foreach (int cnt in map.Values)
                    {
                        if (cnt == 2) { 
                            twoCount++;
                            if (twoCount == 3) { twoCount--; if (cnt == povtor) kCount++; }
                        }
                        else if (cnt == povtor)
                        {
                            kCount++;
                            if (kCount == 2) kCount--;
                        }
                        else if (cnt == 1) oneCount++;
                        else
                        {
                            twoCount = -1;
                            break;
                        }
                    }
                    if (twoCount == 2 && kCount == 1) goodCombination.Add(combination);
                }
                Console.WriteLine($"Слова записаны в файл: {goodCombination.Count}");
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    foreach (string word in goodCombination) writer.WriteLine(word);
                    writer.WriteLine($"Количество слов: {goodCombination.Count}");
                }
                Console.WriteLine($"Слова записаны в файл: {outputFile}");
            }
        }
    }
}




