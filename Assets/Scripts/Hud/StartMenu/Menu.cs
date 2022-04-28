using GranGames.Managers;
using GranGames.Scriptable;
using GranGames.Static;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GranGames.Menu
{
    public class Menu : MonoBehaviour
    {
        public GameObject _buttonNewGame;
        public GameObject _buttonContinue;
        public GameObject _buttonContinueDisable;

        private void Start()
        {
            StartedData(ES3.FileExists());
        }

        private void StartedData(bool value)
        {
            _buttonNewGame.SetActive(true);
            _buttonContinue.SetActive(value);
            _buttonContinueDisable.SetActive(!value);
        }

        public void NewGame()
        {
            SaveLoadManager.Instance.NewGame();
            
        }

        public void Continue()
        {
            SaveLoadManager.Instance.Continue();
        }

        public void Quit()
        {
            Application.Quit();
        }

    }
}