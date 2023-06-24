using System;
using UnityEngine;

namespace Core.Infrastructure.Data
{
    [Serializable]
    public class PlantsData : DataBase
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField, Min(0)]
        private int _plantsAmount;
    }
}