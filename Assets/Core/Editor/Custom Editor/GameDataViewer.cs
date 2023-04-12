using System;
using Core.Infrastructure.Data;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
namespace Core.Editor.CustomEditor
{
    [Serializable]
    public class GameDataViewer
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------
        
        public GameDataViewer() =>
            _gameData = new();

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, HideLabel, Space(-5)]
        private GameData _gameData;
    }
}
#endif