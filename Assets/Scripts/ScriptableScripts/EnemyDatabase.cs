using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace GranGames.Scriptable
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyDatabase", menuName = "GranGames/EnemyDatabase", order = 1)]
    public class EnemyDatabase : SerializedScriptableObject
    {
        [SerializeField]
        private Enemy bossSelected;

        public List<Enemy> _listEnemies;

        public Enemy BossSelected { get => bossSelected; set => bossSelected = value; }
    }
}