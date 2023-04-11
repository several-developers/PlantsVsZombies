using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Infrastructure.Data
{
    [Serializable]
    public class GameData
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public GameData() =>
            _playerData = new();

        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup(DataTitle)]
        [BoxGroup(PlayerDataTitle, showLabel: false), SerializeField]
        private PlayerData _playerData;

        // PROPERTIES: ----------------------------------------------------------------------------

        public PlayerData PlayerData => _playerData;
        
        // FIELDS: --------------------------------------------------------------------------------
        
        private const string DataTitle = "Data";
        private const string PlayerDataTitle = DataTitle + "Player Data";
    }
}