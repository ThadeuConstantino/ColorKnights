using GranGames.Hud;
using GranGames.Managers;
using GranGames.Scriptable;
using System.Collections;
using UnityEngine;

namespace GranGames.Boss
{
    public class Zombie : MonoBehaviour
    {
        public Enemy _enemy;
        public LifeBarHud _lifeBarHud;

        private float healthy;
        private Animator _anim;
        private SpriteRenderer _damageRenderer;

        private bool isDead;

        //Getters and Setters
        public float Healthy { get => healthy; set => healthy = value; }
        public Animator Anim { get => _anim; set => _anim = value; }
        public SpriteRenderer DamageRenderer { get => _damageRenderer; set => _damageRenderer = value; }
        public bool IsDead { get => isDead; set => isDead = value; }

        private void Start()
        {
            IsDead = false;
            Healthy = _enemy.Health;
            Anim = GetComponent<Animator>();
            DamageRenderer = GetComponent<SpriteRenderer>();
        }

        public void AnimDead()
        {
            IsDead = true;
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
            _lifeBarHud.UpdateDataLife(value, Healthy, _enemy.Health);
            StartCoroutine(ColorDamage());

            if (Healthy <= 0)
                AnimDead();
        }

        IEnumerator ColorDamage()
        {
            DamageRenderer.color = Color.red;
            yield return new WaitForSeconds(.5f);
            DamageRenderer.color = Color.white;
        }

        private void OnMouseDown()
        {
            if (GamePlayManager.Instance.CurrentGameState != GamePlayManager.Instance._gsSelectEnemy 
                || IsDead)
                return;

            GamePlayManager.Instance.OnSelectEnemy.Invoke(this);
        }
    }
}