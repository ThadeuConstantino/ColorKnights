using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GranGames.Hud
{
    public class DataPlate : MonoBehaviour
    {
        public TextMeshProUGUI _name;
        public TextMeshProUGUI _level;
        public TextMeshProUGUI _attackPower;
        public TextMeshProUGUI _xp;
        
        public void LoadData(Character player)
        {
            _name.text = player.StrName;
            _level.text = player.Level.ToString();
            _attackPower.text = player.AuxAttack.ToString();
            _xp.text = player.Xp.ToString();
        }
    }
}