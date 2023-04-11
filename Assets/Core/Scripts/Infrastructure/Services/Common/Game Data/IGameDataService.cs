using Core.Infrastructure.Data;

namespace Core.Infrastructure.Services.Common
{
    public interface IGameDataService
    {
        GameData GetGameData();
        PlayerData GetPlayerData();
    }
}