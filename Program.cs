using Newtonsoft.Json;
namespace TestInvestec;

class Program
{


    static async Task Main(string[] args)
    {
        //Work out what the top 3 highest scoring anagrams in this list of scrabble words: https://raw.githubusercontent.com/benjamincrom/scrabble/master/scrabble/dictionary.json
        //Scoring is as follows:
        //1 Point - A, E, I, L, N, O, R, S, T and U.
        //2 Points - D and G.
        //3 Points - B, C, M and P.
        //4 Points - F, H, V, W and Y.
        //5 Points - K.
        //8 Points - J and X.
        //10 Points - Q and Z.
        //To be anagram, there must be at least 2 words that have the same letters in the list e.g. in the example below, far is the only word with an f in it, so can't be an anagram of any other word in the list
        //The word 'aha' scores 6 as it is A = 1, H = 4 and A = 1 as does 'aah' as it is A = 1, H = 4 and A = 1 and they both have the same letters in them.
        //Attempt to balance efficiency with clear code
        //It's not an OO structuring question. You don't need to use interfaces.
        //It should be a console app

        //var result = CalculateScore("aha");
        //Console.WriteLine($"Score for aha: {result}");
        
        //Read file
        var wordList = await LoadWordListAsync("C:\\Users\\abhijit.achare\\source\\repos\\TestInvestecApp\\dictionary.json");
           
        // Calculate scores for each word
        var wordScores = wordList.ToDictionary(word => word, CalculateScore);

        // Find the top 3 anagrams
        var top3Anagrams = wordScores.OrderByDescending(x => x.Value).Take(3);

        // Print the results
        Console.WriteLine("Top 3 Anagrams:");
        foreach (var (word, score) in top3Anagrams)
        {
            Console.WriteLine($"{word} (Score: {score})");
        }
    }

    static async Task<List<string>> LoadWordListAsync(string filePath)
    {
        try
        {
            var jsonContent = await File.ReadAllTextAsync(filePath);
            var words = JsonConvert.DeserializeObject<List<string>>(jsonContent);
            return words;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the word list: {ex.Message}");
            return null;
        }
    }


    private static int CalculateScore(string word)
    {
        var keyValuePairs = new Dictionary<char, int>
        {
            {'A', 1}, {'E', 1}, {'I', 1}, {'L', 1}, {'N', 1}, {'O', 1},
            {'R', 1}, {'S', 1}, {'T', 1}, {'U', 1},
            {'D', 2}, {'G', 2},
            {'B', 3}, {'C', 3}, {'M', 3}, {'P', 3},
            {'F', 4}, {'H', 4}, {'V', 4}, {'W', 4}, {'Y', 4},
            {'K', 5},
            {'J', 8}, {'X', 8},
            {'Q', 10}, {'Z', 10}
        };
        
        var letters = word.ToUpper().ToArray();
        var sum = 0;

        for (var i = 0; i <= letters.Length - 1; i++)
        {
            keyValuePairs.TryGetValue(letters[i], out var num);
            sum += num;
        }
        return sum;
    }
}
