using GranGames.Static;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using GranGames.Managers;

namespace LSW.MenuGame
{
    public class MenuInGame : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _listHealth;
        [SerializeField]
        private TextMeshProUGUI _textScore;
        [SerializeField]
        private GamePlayManager _gamePlayManager;

        private void Update()
        {
            
        }

        public void RefreshHealth(int value)
        {
            for (int i = 1; i <= _listHealth.Count; i++)
            {
                if (i <= value)
                    _listHealth[i - 1].SetActive(true);
                else
                    _listHealth[i - 1].SetActive(false);
            }
        }

        //Close Game
        public void CloseGame()
        {
           
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        }

    }
}