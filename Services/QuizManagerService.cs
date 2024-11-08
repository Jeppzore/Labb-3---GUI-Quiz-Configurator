using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.ViewModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labb_3___GUI_Quiz.Services
{
    internal class QuizManagerService
    {
        private readonly string _dataDirectory;
        private readonly JsonSerializerOptions _options;

        public QuizManagerService()
        {
            // Skapa sökvägen till Local AppData
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dataDirectory = Path.Combine(appDataPath, "QuizApp");

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

        // Spara ett QuestionPack till JSON-fil
        public async void SaveQuestionPack(QuestionPack questionPack)
        {
            string filePath = Path.Combine(_dataDirectory, $"{questionPack.Name}.json");

            var options = new JsonSerializerOptions { WriteIndented = true };

            string json = JsonSerializer.Serialize(questionPack, options);

            await File.WriteAllTextAsync(filePath, json);
        }

        // Ladda ett QuestionPack från JSON-fil
        //public async QuestionPack LoadQuestionPack(string fileName)
        //{
        //    string filePath = Path.Combine(_dataDirectory, fileName);

        //    if (!File.Exists(filePath))
        //    {
        //        throw new FileNotFoundException("The specified question pack file was not found.", filePath);
        //    }

        //    string json = await File.ReadAllTextAsync(filePath);

        //    return JsonSerializer.Deserialize<QuestionPack>(json) ?? throw new InvalidOperationException("Failed to deserialize QuestionPack.");
        //}
    }
}
