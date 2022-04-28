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
        [BoxGroup("Data Basic")]
        [SerializeField]
        private string strName;
        [BoxGroup("Data Basic")]
        [SerializeField]
        [Range(1f, 100f)]
        private float health;
        [BoxGroup("Data Basic")]
        [SerializeField]
        [Range(1f, 100f)]
        private float attackpower;
        private float auxAttack;

        [BoxGroup("Data Level")]
        [Range(1, 100)]
        public int XpToNextLevel;
        [BoxGroup("Data Level")]
        [Range(1, 100)]
        [Header("Xp Points per victory")]
        public int XpPointsPerVictory;
        [BoxGroup("Data Level")]
        [Header("Increment Attack and Life per level - n%")]
        [Range(1, 100)]
        public int PercentIncrementPerLevel;

        [BoxGroup("Dynamic Vars")]
        [SerializeField]
        private int xp;
        [BoxGroup("Dynamic Vars")]
        [SerializeField]
        private int level;

        [BoxGroup("Image")]
        [Header("Icon player")]
        public Sprite _imageMenu;

        //Getters and Setters
        public float Health { get => health; set => health = value; }
        public float AttackPower { get => attackpower; set => attackpower = value; }
        public int Level { get => level; set => level = value; }
        public string StrName { get => strName; set => strName = value; }
        public int Xp { get => xp; set => xp = value; }
        public float AuxAttack { get => auxAttack; set => auxAttack = value; }

        public void CalculateLogicalPlayer()
        {
            Level = Mathf.RoundToInt(Xp / XpToNextLevel);

            AuxAttack = AttackPower;
            for (var i = 0; i < Level; i++)
                AuxAttack += AuxAttack * PercentIncrementPerLevel / 100;
        }
    }
}