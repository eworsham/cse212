using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        List<string> symmetricWords = new List<string>();
        HashSet<string> wordsSet = new HashSet<string>(words);

        // Loop through words, reverse word and check in set if reversed word exists
        foreach (string word in words)
        {
            char firstLetter = word[0];
            char secondLetter = word[1];

            // If the letters are the same, then it does not match anything else
            if (firstLetter == secondLetter) continue;

            string reversedWord = secondLetter.ToString() + firstLetter.ToString();
            if (wordsSet.Contains(reversedWord))
            {
                symmetricWords.Add($"{word} & {reversedWord}");
                wordsSet.Remove(word);
            }
        }


        return symmetricWords.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            string degreeEarned = fields[3];
            if (degrees.ContainsKey(degreeEarned))
            {
                degrees[degreeEarned] += 1;
            }
            else
            {
                degrees[degreeEarned] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // create dictionary for word1
        Dictionary<char, int> word1Dict = new Dictionary<char, int>();

        // Make word1 all lowercase
        string word1Lower = word1.ToLower();

        // Convert to dictionary
        foreach (char letter in word1Lower)
        {
            // Ignore spaces
            if (letter == ' ') continue;

            if (word1Dict.ContainsKey(letter))
            {
                word1Dict[letter] += 1;
            }
            else
            {
                word1Dict[letter] = 1;
            }
        }

        // create dictionary for word2
        Dictionary<char, int> word2Dict = new Dictionary<char, int>();

        // Make word2 all lowercase
        string word2Lower = word2.ToLower();

        // Convert to dictionary
        foreach (char letter in word2Lower)
        {
            // Ignore spaces
            if (letter == ' ') continue;

            if (word2Dict.ContainsKey(letter))
            {
                word2Dict[letter] += 1;
            }
            else
            {
                word2Dict[letter] = 1;
            }
        }

        // Compare the 2 dictionaries of word1 and word2
        foreach (var item in word1Dict)
        {
            if (!word2Dict.ContainsKey(item.Key))
            {
                return false;
            }
            if (!(word2Dict[item.Key] == item.Value))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.
        string[] earthquakes = new string[featureCollection.Features.Length];

        for (int i = 0; i < featureCollection.Features.Length; i++)
        {
            earthquakes[i] = $"{featureCollection.Features[i].Properties.Place} - Mag {featureCollection.Features[i].Properties.Mag}";
        }
        
        return earthquakes;
    }
}