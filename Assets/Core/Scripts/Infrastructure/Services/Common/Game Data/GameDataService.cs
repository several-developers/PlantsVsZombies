using Core.Infrastructure.Data;

namespace Core.Infrastructure.Services.Common
{
    public class GameDataService : IGameDataService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public GameDataService() =>
            _gameData = new();

        // FIELDS: --------------------------------------------------------------------------------

        private GameData _gameData;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetGameData(GameData gameData) =>
            _gameData = gameData;
        
        public GameData GetGameData() => _gameData;

        public PlayerData GetPlayerData() => _gameData.PlayerData;
    }
}