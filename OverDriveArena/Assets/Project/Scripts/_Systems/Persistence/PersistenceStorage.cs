using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts._Systems.Persistence
{
    public class PersistenceStorage
    {
        private readonly string _directoryPath;

        public PersistenceStorage()
        {
            this._directoryPath = Application.persistentDataPath;
        }

        public async Task SaveAsync<T>(string key, T data) where T : class
        {
            try
            {
                string filePath = GetFilePath(key);
                string json = JsonUtility.ToJson(data, true);

                await File.WriteAllTextAsync(filePath, json);
                Debug.Log($"[PersistenceStorage] Successfully saved data to: {filePath}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[PersistenceStorage] Failed to save data for key '{key}': {ex.Message}");
            }
        }

        public async Task<T> LoadAsync<T>(string key) where T : class, new()
        {
            string filePath = GetFilePath(key);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"[PersistenceStorage] No save file found at: {filePath}. Returning default.");
                return new T();
            }

            try
            {
                string json = await File.ReadAllTextAsync(filePath);
                T data = JsonUtility.FromJson<T>(json);
                return data;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[PersistenceStorage] Failed to load data for key '{key}': {ex.Message}");
                return new T();
            }
        }

        private string GetFilePath(string key)
        {
            return Path.Combine(_directoryPath, $"{key}.json");
        }
    }
}