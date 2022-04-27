using UnityEngine;
using TMPro;
using GranGames.Static;
using UnityEngine.SceneManagement;
using GranGames.Pl;

namespace GranGames.Hud
{
    public class HudCanvasGamePlay : MonoBehaviour
    {
        public TextMeshProUGUI _textGuideMessage;
        public DataPlate _dataPlate;

        public void LoadDataPlate(Character _char, bool _enable)
        {
            _dataPlate.gameObject.SetActive(_enable);
            _dataPlate.LoadData(_char);
        }

        public void CloseSceneGAmePlay()
        {
            SceneManager.LoadScene(DataGame.MENUSTART_SCENE, LoadSceneMode.Single);
        }

        void Update()
        {
            _textGuideMessage.text = DataGame.CURRENT_TEXT_STATE;
        }
    }
}