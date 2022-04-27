using GranGames.Managers;
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
            StartedData(false);
        }

        private void StartedData(bool value)
        {
            _buttonNewGame.SetActive(true);
            _buttonContinue.SetActive(value);
            _buttonContinueDisable.SetActive(!value);
        }

        public void NewGame()
        {
            LoadScene(DataGame.GAMEPLAY_SCENE);
        }

        public void Continue()
        {
            LoadScene(DataGame.GAMEPLAY_SCENE);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void LoadScene(string value)
        {
            SceneManager.LoadScene(value, LoadSceneMode.Single);
        }
    }
}