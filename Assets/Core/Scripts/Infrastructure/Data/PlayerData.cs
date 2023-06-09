using System;
using UnityEngine;

namespace Core.Infrastructure.Data
{
    [Serializable]
    public class PlayerData : DataBase
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Min(0)]
        private int _money;
    }
}