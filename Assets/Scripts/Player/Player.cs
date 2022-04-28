using GranGames.Hud;
using GranGames.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GranGames.Pl
{
    public class Player : MonoBehaviour
    {
        public Character _character;
        public GameObject _clamp;
        private float healthy;

        private bool isDead;
        private Animator _anim;
        private SpriteRenderer _damageRenderer;

        public LifeBarHud _lifeBarHud;

        //Getters and Setters
        public bool IsDead { get => isDead; set => isDead = value; }
        public Animator Anim { get => _anim; set => _anim = value; }
        public SpriteRenderer DamageRenderer { get => _damageRenderer; set => _damageRenderer = value; }
        public float Healthy { get => healthy; set => healthy = value; }

        private void Start()
        {
            Anim = GetComponent<Animator>();
            DamageRenderer = GetComponent<SpriteRenderer>();
            Healthy = _character.Health;
            IsDead = false;
        }

        private void OnEnable()
        {
            _character.CalculateLogicalPlayer();
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

        public void Damage(float value)
        {
            Healthy -= value;
            _lifeBarHud.UpdateDataLife(value, Healthy, _character.Health);
            
            if(value>0)
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
            if (GamePlayManager.Instance.CurrentGameState != GamePlayManager.Instance._gsSelectPlayer
                && GamePlayManager.Instance.CurrentGameState != GamePlayManager.Instance._gsSelectEnemy
                && !IsDead)
                return;

            GamePlayManager.Instance.OnOverPlayer.Invoke(_character, true);
        }

        private void OnMouseExit()
        {
            if (GamePlayManager.Instance.CurrentGameState != GamePlayManager.Instance._gsSelectPlayer
                && GamePlayManager.Instance.CurrentGameState != GamePlayManager.Instance._gsSelectEnemy
                && !IsDead)
                return;

            GamePlayManager.Instance.OnOverPlayer.Invoke(_character, false);
        }
    }
}
