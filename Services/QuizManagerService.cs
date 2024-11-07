﻿using Labb_3___GUI_Quiz.Model;
using Labb_3___GUI_Quiz.ViewModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labb_3___GUI_Quiz.Services
{
    internal class QuizManagerService
    {
        private readonly string filePath;
        private MainWindowViewModel? _mainWindowViewModel;
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };

        public QuizManagerService(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            var appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuizApp");
            Directory.CreateDirectory(appDataFolder);
            filePath = Path.Combine(appDataFolder, "Questionpacks.json");
        }

        public void SaveQuestions(List<Question> questions, string packName)
        {
            var existingPacks = LoadQuestionPacks();

            var packToUpdate = existingPacks.FirstOrDefault(p => p.Name == packName);
            if (packToUpdate != null)
            {
                packToUpdate.Questions = questions;
            }
            else
            {
                var newPack = new QuestionPack(packName) { Questions = questions };
                existingPacks.Add(newPack);
            }

            var json = JsonSerializer.Serialize(new { QuestionPacks = existingPacks }, options);
            File.WriteAllText(filePath, json);
        }

        public void SaveQuestionPacks(List<QuestionPack> newPacks)
        {
            var existingPacks = LoadQuestionPacks();

            foreach (var newPack in newPacks)
            {
                var existingPack = existingPacks.FirstOrDefault(p => p.Name == newPack.Name);
                if (existingPack != null)
                {
                    existingPacks.Remove(existingPack);
                }
                existingPacks.Add(newPack);
            }
            if (existingPacks.Count == 0)
            {
                var defaultPack = new QuestionPack("Default Pack", Difficulty.Easy, 30);
                existingPacks.Add(defaultPack);
            }
            var json = JsonSerializer.Serialize(new { QuestionPacks = existingPacks }, options);
            File.WriteAllText(filePath, json);
        }

        public List<QuestionPack> LoadQuestionPacks()
        {
            try
            {
                var json = File.ReadAllText(filePath);
                var result = JsonSerializer.Deserialize<RootObject>(json, options);
                if (result == null || result.QuestionPacks == null)
                {
                    return new List<QuestionPack>();
                }
                return result.QuestionPacks;
            }

            catch (FileNotFoundException)
            {
                return new List<QuestionPack> { new QuestionPack("Default Pack", Difficulty.Easy, 30) };
            }
        }
        public void RemoveQuestionPack(List<QuestionPack> packs, QuestionPack packToRemove)
        {

            if (packToRemove != null && packToRemove.Name != "Default Pack")
            {
                packs.Remove(packToRemove);
                var json = JsonSerializer.Serialize(new { QuestionPacks = packs }, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            else
            {
                Console.WriteLine("Default Pack cannot be removed.");
            }
        }
        public class RootObject
        {
            public List<QuestionPack> QuestionPacks { get; set; } = new List<QuestionPack>();
        }
    }
}
