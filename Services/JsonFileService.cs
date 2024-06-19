using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TestMaker.Models;

namespace TestMaker.Services
{
    public static class JsonFileService
    {
        private const string FilePath = "tests.json";

        public static List<Test> LoadTests()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Test>>(json);
            }
            else
            {
                return new List<Test>();
            }
        }

        public static void SaveTests(List<Test> tests)
        {
            var json = JsonConvert.SerializeObject(tests, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public static void RemoveTest(Test testToRemove)
        {
            var tests = LoadTests();
            tests.RemoveAll(t => t.Name == testToRemove.Name && t.Category == testToRemove.Category);
            SaveTests(tests);
        }

        public static void UpdateTest(Test testToUpdate)
        {
            List<Test> tests = LoadTests();
            tests.RemoveAll(t => t.Name == testToUpdate.Name && t.Category == testToUpdate.Category);
            tests.Add(testToUpdate);
            SaveTests(tests);
        }

    }
}
