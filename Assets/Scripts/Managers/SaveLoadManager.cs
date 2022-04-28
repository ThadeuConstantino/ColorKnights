using GranGames.Scriptable;
using GranGames.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GranGames.Managers
{
    public class SaveLoadManager : Singleton<SaveLoadManager>
    {
        [SerializeField]
        private PlayerDatabase _playerDatabase;

        public void NewGame()
        {
            foreach(var player in _playerDatabase._listPlayers)
            {
                player.Xp = 0;
                player.Level = 0;
                
            }
            _playerDatabase._selectedPlayers.Clear();
            _playerDatabase.TotalBattles = 0;

            SceneManager.LoadScene(DataGame.SELECTPLAYER_SCENE, LoadSceneMode.Single); 

            ES3.Save<PlayerDatabase>("PlayerDatabase", _playerDatabase);
        }

        public void Continue()
        {
            _playerDatabase = ES3.Load<PlayerDatabase>("PlayerDatabase");
            string strScene = ES3.Load<string>("ActiveScene") == DataGame.MENUSTART_SCENE ? DataGame.SELECTPLAYER_SCENE : ES3.Load<string>("ActiveScene");
            SceneManager.LoadScene(strScene, LoadSceneMode.Single);
        }

        private void OnApplicationQuit()
        {
            ES3.Save<string>("ActiveScene", SceneManager.GetActiveScene().name);
            ES3.Save<PlayerDatabase>("PlayerDatabase", _playerDatabase);
        }
    }
}