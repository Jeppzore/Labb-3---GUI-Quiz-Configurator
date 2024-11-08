using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.ViewModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labb_3___GUI_Quiz.Services
{
    internal class QuizManagerService
    {
        private static readonly string _dataDirectory;
        private static readonly JsonSerializerOptions _options;

        static QuizManagerService()
        {
            // Skapa sökvägen till Local AppData
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dataDirectory = Path.Combine(appDataPath, "JespersLabb3");

            _options = new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true
            };

            // Skapa mappen om den inte finns
            if (!Directory.Exists(_dataDirectory))
            {
                Directory.CreateDirectory(_dataDirectory);
            }
        }

        // Spara QuestionPacks till JSON-fil
        public static async void SaveQuestionPacks(List<QuestionPack> questionPacks)
        {
            string filePath = Path.Combine(_dataDirectory, "QuestionPacks.json");

            var options = new JsonSerializerOptions { WriteIndented = true };

            string json = JsonSerializer.Serialize(questionPacks, options);

            await File.WriteAllTextAsync(filePath, json);
        }

        //Ladda QuestionPacks från JSON-fil
        public static async Task<List<QuestionPack>> LoadQuestionPacks(string fileName)
        {
            string filePath = Path.Combine(_dataDirectory, fileName);

            if (!File.Exists(filePath))
            {
                var firstPack = new QuestionPack("Default Question Pack", Difficulty.Medium, timeLimitInSeconds: 30);
                SaveQuestionPacks(new List<QuestionPack> { firstPack });
            }

            using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            var questionPacks = await JsonSerializer.DeserializeAsync<List<QuestionPack>>(stream) ?? new List<QuestionPack>();

            if (questionPacks.Count <= 0)
            {
                var firstPack = new QuestionPack("Default Question Pack", Difficulty.Medium, timeLimitInSeconds: 30);
                questionPacks.Add(firstPack);
            }

            return questionPacks;
        }
    }
}
