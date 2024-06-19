using System;
using System.IO;
using System.Text.Json;

namespace TestMaker.Services
{
    public static class ImportExportService
    {
        /// <summary>
        /// Eksportuje obiekt do formatu JSON i zapisuje go w pliku.
        /// </summary>
        /// <typeparam name="T">Typ eksportowanego obiektu.</typeparam>
        /// <param name="folderPath">Ścieżka do folderu, w którym zostanie zapisany plik.</param>
        /// <param name="fileName">Nazwa pliku, do którego zapisany zostanie JSON.</param>
        /// <param name="obj">Obiekt do eksportu.</param>
        public static void ExportToJson<T>(string folderPath, string fileName, T obj)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("Ścieżka do folderu nie może być pusta", nameof(folderPath));

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Nazwa pliku nie może być pusta", nameof(fileName));

            string filePath = Path.Combine(folderPath, fileName);
            string jsonString = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(filePath, jsonString);
        }

        /// <summary>
        /// Importuje obiekt z pliku JSON.
        /// </summary>
        /// <typeparam name="T">Typ importowanego obiektu.</typeparam>
        /// <param name="filePath">Ścieżka do pliku JSON.</param>
        /// <returns>Zaimportowany obiekt.</returns>
        public static T ImportFromJson<T>(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Ścieżka do pliku nie może być pusta", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Plik nie został znaleziony", filePath);

            string jsonString = File.ReadAllText(filePath);
            T obj = JsonSerializer.Deserialize<T>(jsonString);

            return obj;
        }
    }
}
