using UnityEngine;
using Sirenix.OdinInspector;

namespace GranGames
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "Character", menuName = "GranGames/Character", order = 3)]
    public class Character : SerializedScriptableObject
    {
        [Header("Data")]
        [SerializeField]
        private string strName;
        [SerializeField]
        [Range(1, 10)]
        private int health;
        [SerializeField]
        [Range(1, 10)]
        private int attackpower;
        [SerializeField]
        [Range(1, 10)]
        private int xpNewLevel;

        [Header("Dynamic Vars")]
        [SerializeField]
        private int level;
        [SerializeField]
        private int xp;

        [Header("Icon player")]
        public Sprite _imageMenu;

        //Getters and Setters
        public int Health { get => health; set => health = value; }
        public int AttackPower { get => attackpower; set => attackpower = value; }
        public int Level { get => level; set => level = value; }
        public string StrName { get => strName; set => strName = value; }
        public int Xp { get => xp; set => xp = value; }
    }
}