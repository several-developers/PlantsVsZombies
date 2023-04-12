using Core.Infrastructure.Data;

namespace Core.Infrastructure.Services.Common
{
    public interface IGameDataService
    {
        void SetGameData(GameData gameData);
        GameData GetGameData();
        PlayerData GetPlayerData();
    }
}