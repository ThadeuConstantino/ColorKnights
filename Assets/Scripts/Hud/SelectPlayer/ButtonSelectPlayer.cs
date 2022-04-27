using GranGames.Managers;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GranGames.Hud
{
    public class ButtonSelectPlayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Character _player;
        [SerializeField]
        private GameObject _not;
        [SerializeField]
        private GameObject _over;
        [SerializeField]
        private Image _img;
        [SerializeField]
        private GameObject _flag;

        [SerializeField]
        [Required]
        private ToolTipPlayer _tooltip;
        private bool isOpenTooltip;

        [SerializeField]
        [Required]
        private MenuSelectPlayer _menuSelectPlayer;

        private bool isEnable;

        private IEnumerator coroutine;

        //Getters and Setters
        public Character Player { get => _player; set => _player = value; }

        public void _Start()
        {
            isOpenTooltip = false;
            _over.SetActive(false);
            _not.SetActive(false);
            _img.gameObject.SetActive(true);
            _img.sprite = Player._imageMenu;
        }

        public void EnableBg(bool value)
        {
            isEnable = value;
            _not.SetActive(!isEnable);
        }

        public void DataPlayer(bool value)
        {
            _flag.SetActive(value);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _over.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _over.SetActive(false);

            if (!isEnable)
                return;

            _tooltip.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isEnable)
                return;

            coroutine = DelayOpenToolTip();
            StartCoroutine(coroutine);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopCoroutine(coroutine);

            if (isOpenTooltip || !isEnable)
            {
                isOpenTooltip = false;
                return;
            }

            if (_menuSelectPlayer.SelectedPlayer(_player, !_flag.activeSelf))
                _flag.SetActive(!_flag.activeSelf); 
        }

        IEnumerator DelayOpenToolTip()
        {
            isOpenTooltip = false;
            yield return new WaitForSeconds(3f);
            isOpenTooltip = true;
            _tooltip.gameObject.SetActive(true);
            _tooltip.LoadData(_player);
        }
    }
}
