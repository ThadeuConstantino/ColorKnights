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

        private int healthy;
        private Animator _anim;
        private SpriteRenderer _damageRenderer;

        private bool isDead;

        //Getters and Setters
        public int Healthy { get => healthy; set => healthy = value; }
        public Animator Anim { get => _anim; set => _anim = value; }
        public SpriteRenderer DamageRenderer { get => _damageRenderer; set => _damageRenderer = value; }

        private void Start()
        {
            isDead = false;
            Healthy = _enemy.Health;
            Anim = GetComponent<Animator>();
            DamageRenderer = GetComponent<SpriteRenderer>();
        }

        public void AnimDead()
        {
            isDead = true;
            Anim.SetTrigger("isDead");
        }

        public void Damage(int value)
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
            if (GamePlayManager.Instance.CurrentGameState != GamePlayManager.Instance._gsSelectEnemy)
                return;

            GamePlayManager.Instance.OnSelectEnemy.Invoke(this);
        }
    }
}