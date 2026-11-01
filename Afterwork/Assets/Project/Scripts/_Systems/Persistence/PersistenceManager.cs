using System;
using System.IO;
using UnityEngine;

namespace Project.Scripts._Systems.Persistence
{
    public sealed class PersistenceManager
    {
        private readonly string _directoryPath;

        public PersistenceManager(string subFolder = "Saves")
        {
            this._directoryPath = Path.Combine(Application.persistentDataPath, subFolder);

            if (!Directory.Exists(this._directoryPath)) Directory.CreateDirectory(this._directoryPath);
        }

        public void Save<T>(string fileName, T data)
        {
            var filePath = Path.Combine(this._directoryPath, fileName + ".json");

            try
            {
                var jsonString = JsonUtility.ToJson(data, true);
                File.WriteAllText(filePath, jsonString);
                Debug.Log($"Successfully saved data to: {filePath}");
            }
            catch (Exception exception)
            {
                Debug.LogError($"Failed to save data to {filePath}. Error: {exception.Message}");
            }
        }

        public T Load<T>(string fileName, Func<T> defaultFactory) where T : new()
        {
            var filePath = Path.Combine(this._directoryPath, fileName + ".json");

            if (File.Exists(filePath) == false)
            {
                Debug.LogWarning($"Save file not found at {filePath}. Returning default data.");
                return defaultFactory.Invoke();
            }

            try
            {
                var jsonString = File.ReadAllText(filePath);
                var data = JsonUtility.FromJson<T>(jsonString);
                Debug.Log($"Successfully loaded data from: {filePath}");
                return data;
            }
            catch (Exception exception)
            {
                Debug.LogError($"Failed to load data from {filePath}. Error: {exception.Message}");
                return defaultFactory.Invoke();
            }
        }
    }
}