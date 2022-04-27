using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GranGames.Hud
{
    public class ToolTipPlayer : MonoBehaviour
    {
        public TextMeshProUGUI _name;
        public TextMeshProUGUI _level;
        public TextMeshProUGUI _attackPower;
        public TextMeshProUGUI _xp;
        public Canvas canvas;

        //private
        private Vector3 auxLocalPosition;

        private void Start()
        {
            auxLocalPosition = gameObject.transform.localPosition;
            gameObject.SetActive(false);
        }


        public void OnDisable()
        {
            gameObject.transform.localPosition = auxLocalPosition;
        }

        public void Update()
        {
            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera,
                out movePos);

            transform.position = canvas.transform.TransformPoint(movePos);
        }

        public void LoadData(Character _player)
        {
            _name.text = _player.StrName;
            _level.text = _player.Level.ToString();
            _attackPower.text = _player.AttackPower.ToString();
            _xp.text = _player.Xp.ToString();
        }
    }
}