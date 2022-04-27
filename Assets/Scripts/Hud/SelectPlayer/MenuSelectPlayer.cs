using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GranGames.Scriptable;

namespace GranGames.Hud
{
    public class MenuSelectPlayer : MonoBehaviour
    {
        [SerializeField]
        private List<ButtonSelectPlayer> listButtonPlayer;
        [SerializeField]
        private TextMeshProUGUI _textTotalPlayers;
        [SerializeField]
        private GameObject _buttonStartBattle;

        [SerializeField]
        private PlayerDatabase _playerDatabase;

        private void Start()
        {
            int i = 0;
            foreach(var _hudPlayer in listButtonPlayer)
            {
                Character _player = _playerDatabase._listPlayers[i];
                _hudPlayer.Player = _player;
                
                //SaveLoad
                _hudPlayer.DataPlayer(_playerDatabase._selectedPlayers.Contains(_player));
                _hudPlayer._Start();
                _hudPlayer.EnableBg(i < (_playerDatabase.TotalBattles / _playerDatabase.BattlesNewPlayer));

                i++;
            }

            UpdateData();
        }

        private void UpdateData()
        {
            _textTotalPlayers.text = "Total of warriors in the group: " + _playerDatabase._selectedPlayers.Count + "/" + _playerDatabase.totalPlayersGroup;
            _buttonStartBattle.SetActive(_playerDatabase._selectedPlayers.Count > 0);
        }

        public bool SelectedPlayer(Character player, bool add)
        {
            if (_playerDatabase._selectedPlayers.Count == _playerDatabase.totalPlayersGroup && add)
            {
                StartCoroutine(DelayWarning());
                return false;
            }

            if(add)
                _playerDatabase._selectedPlayers.Add(player);
            else
                _playerDatabase._selectedPlayers.Remove(player);

            UpdateData();
            return true;
        }

        IEnumerator DelayWarning()
        {
            _textTotalPlayers.color = Color.red;
            yield return new WaitForSeconds(1f);
            _textTotalPlayers.color = Color.white;
        }
    }
}
