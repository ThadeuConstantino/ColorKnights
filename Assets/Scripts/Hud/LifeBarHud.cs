using GranGames.Managers;
using GranGames.Scriptable;
using UnityEngine;

namespace GranGames.Hud
{
    public class LifeBarHud : MonoBehaviour
    {
        private GameObject _clone;
        [SerializeField]
        private GameObject _prefabLifeBar;
        private LifeBar auxLifeBar;

        private void OnEnable()
        {
            _clone = GameObject.Instantiate(_prefabLifeBar, GamePlayManager.Instance._canvasHudLifeBar.transform);
            auxLifeBar = _clone.GetComponent<LifeBar>();
            auxLifeBar._bar.fillAmount = 1f;
        }

        public void UpdateDataLife(float damage, float currentHealth, float totalHealth)
        {
            float result = (currentHealth * auxLifeBar._bar.fillAmount) / totalHealth;
            auxLifeBar._bar.fillAmount = result;
        }

        void Update()
        {
            if (_clone)
            {
                RectTransform rt = (RectTransform)_clone.transform;
                Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
                namePos.x -= rt.rect.width/2;

                _clone.transform.position = namePos;
            }
        }
    }
}