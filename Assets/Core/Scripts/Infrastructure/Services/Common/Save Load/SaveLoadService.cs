using System;
using System.IO;
using Core.Infrastructure.Data;
using Sirenix.Utilities;
using UnityEngine;

namespace Core.Infrastructure.Services.Common
{
    public interface ISaveLoadService
    {
        event Action OnLocalDataSaved;
        event Action OnLocalDataLoaded;
        event Action OnLocalDataDeleted;
        void SaveLocalData();
        void LoadLocalData();
        void DeleteLocalData();
    }

    public class SaveLoadService : ISaveLoadService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public SaveLoadService(IGameDataService gameDataService) =>
            _gameDataService = gameDataService;

        // FIELDS: --------------------------------------------------------------------------------

        public event Action OnLocalDataSaved;
        public event Action OnLocalDataLoaded;
        public event Action OnLocalDataDeleted;

        private readonly IGameDataService _gameDataService;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SaveLocalData()
        {
            TrySaveLocalData();
            OnLocalDataSaved?.Invoke();
        }

        public void LoadLocalData()
        {
            TryLoadData();
            OnLocalDataLoaded?.Invoke();
        }

        public void DeleteLocalData()
        {
            TryDeleteData();
            TryLoadData();
            OnLocalDataDeleted?.Invoke();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void TryLoadData()
        {
            string path = GetDataPath();
            bool isFileExists = File.Exists(path);

            if (!isFileExists)
            {
                GameData gameData = new();
                string json = JsonUtility.ToJson(gameData);
                File.WriteAllText(path, json);
            }

            string data = File.ReadAllText(path);
            TrySetData(data);
        }

        private void TrySetData(string data)
        {
            if (data.IsNullOrWhitespace())
                return;

            GameData gameData = JsonUtility.FromJson<GameData>(data);
            _gameDataService.SetGameData(gameData);
        }

        private void TrySaveLocalData()
        {
            GameData gameData = _gameDataService.GetGameData();
            string path = GetDataPath();
            string data = JsonUtility.ToJson(gameData);
            File.WriteAllText(path, data);
        }

        private void TryDeleteData()
        {
            string path = GetDataPath();

            if (!File.Exists(path))
                return;

            File.Delete(path);
        }

        private static string GetDataPath() =>
            Application.persistentDataPath + "/Game Data.json";
    }
}