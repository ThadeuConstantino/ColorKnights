using GranGames.Static;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LSW.MenuGame
{
    public class PopUpEndGame : MonoBehaviour
    {
        public GameObject _bgGood;
        public GameObject _bgBad;

        private void OnEnable()
        {
            _bgGood.SetActive(DataGame.win);
            _bgBad.SetActive(!DataGame.win);  
        }

        public void CloseGame()
        {
            SceneManager.LoadScene(DataGame.SELECTPLAYER_SCENE, LoadSceneMode.Single);
        }
    }
}