using GranGames.Hud;
using GranGames.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GranGames.Pl
{
    public class Player : MonoBehaviour
    {
        public Character _character;
        public GameObject _clamp;

        private int healthy;
        private bool isDead;
        private Animator _anim;
        private SpriteRenderer _damageRenderer;

        public LifeBarHud _lifeBarHud;

        //Getters and Setters
        public bool IsDead { get => isDead; set => isDead = value; }
        public int Healthy { get => healthy; set => healthy = value; }
        public Animator Anim { get => _anim; set => _anim = value; }
        public SpriteRenderer DamageRenderer { get => _damageRenderer; set => _damageRenderer = value; }

        private void Start()
        {
            Healthy = _character.Health;
            Anim = GetComponent<Animator>();
            DamageRenderer = GetComponent<SpriteRenderer>();
        }

        public void AnimDead()
        {
            Anim.SetTrigger("isDead");
        }

        public void AnimAttack()
        {
            Anim.SetTrigger("isAttack");
            StartCoroutine(DelayAttack());
        }
        IEnumerator DelayAttack()
        {
            yield return new WaitForSeconds(.3f);
            Anim.SetTrigger("isIdle");
        }

        public void Damage(int value)
        {
            Healthy -= value;
            _lifeBarHud.UpdateDataLife(value, Healthy, _character.Health);
            StartCoroutine(ColorDamage());

            if (Healthy <= 0)
                AnimDead();
        }

        IEnumerator ColorDamage()
        {
            Color _color = new Color();
            _color = DamageRenderer.color;
            DamageRenderer.color = Color.red;
            yield return new WaitForSeconds(.5f);
            DamageRenderer.color = _color;
        }


        private void OnMouseDown()
        {
            if (GamePlayManager.Instance.CurrentGameState != GamePlayManager.Instance._gsSelectPlayer
                && GamePlayManager.Instance.CurrentGameState != GamePlayManager.Instance._gsSelectEnemy
                && !IsDead)
                return;

            GamePlayManager.Instance.OnSelectPlayer.Invoke(this);
        }

        private void OnMouseEnter()
        {
            GamePlayManager.Instance.OnOverPlayer.Invoke(_character, true);
        }

        private void OnMouseExit()
        {
            GamePlayManager.Instance.OnOverPlayer.Invoke(_character, false);
        }
    }
}
