using GranGames.Static;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GranGames.Hud
{
    public class ButtonStartBattle : MonoBehaviour
    {
        public void StartGamePlayScene()
        {
            SceneManager.LoadScene(DataGame.GAMEPLAY_SCENE, LoadSceneMode.Single);
        }
    }
}