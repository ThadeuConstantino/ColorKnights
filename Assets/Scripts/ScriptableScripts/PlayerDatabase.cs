using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace GranGames.Scriptable
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerDatabase", menuName = "GranGames/PlayerDatabase", order = 1)]
    public class PlayerDatabase : SerializedScriptableObject
    {
        public List<Character> _listPlayers;

        [Header("Total Players in the Battle Group (1-4)")]
        [Range(1, 4)]
        public int totalPlayersGroup;

        [BoxGroup("Battles")]
        [Header("Minimum battles for new player - 'TotalBattles/BattlesNewPlayer'")]
        [Range(1, 100)]
        public int BattlesNewPlayer;

        [BoxGroup("Battles")]
        [Range(1, 4)]
        [Header("Start Players in Battle")]
        public int StartPlayersBattle;

        [BoxGroup("Battles")]
        [Header("Total of Battles")]
        [SerializeField]
        private int totalBattles;

        [Header("Players Selected")]
        public List<Character> _selectedPlayers;

        public int TotalBattles { get => totalBattles; set => totalBattles = value; }
    }

}