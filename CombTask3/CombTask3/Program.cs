public class Combinatorica
{
    public List<string> GenerateWords(char[] alphabet, int wordLength, int uniqueLetters)
    {
        List<string> validWords = new List<string>();
        int n = alphabet.Length;

        Queue<string> queue = new Queue<string>();
        queue.Enqueue("");

        while (queue.Count > 0)
        {
            string currentWord = queue.Dequeue();
            int level = currentWord.Length;


            if (level == wordLength)
            {
                HashSet<char> uniqueChars = new HashSet<char>(currentWord);

                if (uniqueChars.Count == uniqueLetters)
                {
                    validWords.Add(currentWord);
                }

                continue;
            }

            for (int i = 0; i < n; i++)
            {
                string nextWord = currentWord + alphabet[i];

                HashSet<char> uniqueChars = new HashSet<char>(nextWord);
                if (uniqueChars.Count <= uniqueLetters)
                {
                    queue.Enqueue(nextWord);
                }
            }


        }


        return validWords;
    }
    public class Program()
    {
        static void Main(string[] args)
        {
            Combinatorica e = new Combinatorica();
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k' };
            int wordLength = 10;
            int uniqueLetters = 4;
            string outputFile = "words.txt";

            List<string> validWords = e.GenerateWords(alphabet, wordLength, uniqueLetters);

            Console.WriteLine(validWords.Count);

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