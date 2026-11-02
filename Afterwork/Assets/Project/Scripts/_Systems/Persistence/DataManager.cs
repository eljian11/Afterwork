using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts._Systems.Persistence
{
    public sealed class DataManager : MonoBehaviour
    {
        private PersistenceStorage _persistenceStorage;

        private void Awake()
        {
           this._persistenceStorage = new PersistenceStorage();
        }

        public async Task SaveDataAsync<T>(string key, T data) where T : class, IGameData
        {
            await this._persistenceStorage.SaveAsync(key, data);
        }

        public async Task<T> LoadDataAsync<T>(string key) where T : class, IGameData, new()
        {
            return await this._persistenceStorage.LoadAsync<T>(key);
        }
    }
    
    public interface IGameData
    {
        // Marker interface for saveable game data structures
    }
}