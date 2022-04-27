using UnityEngine;
using Sirenix.OdinInspector;

namespace GranGames.Scriptable
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "Enemy", menuName = "GranGames/Enemy", order = 3)]
    public class Enemy : SerializedScriptableObject
    {
        [Header("Data")]
        [SerializeField]
        [Range(1, 100)]
        private int health;
        [SerializeField]
        [Range(1, 10)]
        private int attackpower;
      
        //Getters and Setters
        public int Health { get => health; set => health = value; }
        public int AttackPower { get => attackpower; set => attackpower = value; }
    }
}