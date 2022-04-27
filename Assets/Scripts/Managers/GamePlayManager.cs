using GranGames.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GranGames.Pl;
using UnityEngine.Events;
using GranGames.Scriptable;
using GranGames.Boss;
using GranGames.Hud;

namespace GranGames.Managers
{
    public class GamePlayManager : Singleton<GamePlayManager>
    {
        [Header("Player Data Points")]
        public List<Player> _listPlayerPrefabSelected;
        private List<Player> _auxListPlayerPrefabSelected;
        public List<GameObject> _listPlayerSpawnPoints;
        public List<Player> _listPlayerPrefab;
        public PlayerDatabase _playerDatabase;

        [Header("Enemy Data Points")]
        public GameObject _EnemySpawnPoints;
        public List<GameObject> _ListEnemyPrefab;
        public EnemyDatabase _enemyDatabase;

        [Header("Game State")]
        public GameState _gsSelectPlayer;
        public GameState _gsEnemyAttack;
        public GameState _gsSelectEnemy;
        private GameState _CurrentGameState;

        public GameObject _clamp;
        public GameObject _canvasHudLifeBar;
        public HudCanvasGamePlay _hudCanvasGamePlay;

        [Header("Unity Event")]
        public UnityEvent<Player> OnSelectPlayer;
        public UnityEvent<Character, bool> OnOverPlayer;
        public UnityEvent<Zombie> OnSelectEnemy;
        public UnityEvent OnUpdateStatus;

        //Private
        [SerializeField]
        private Player currentPlayer;
        [SerializeField]
        private Zombie currentEnemy;

        private IEnumerator _coroutine;

        //Getters and Setters
        public GameState CurrentGameState { get => _CurrentGameState; set => _CurrentGameState = value; }
        

        private void Start()
        {
            _listPlayerPrefabSelected.Clear();
            _auxListPlayerPrefabSelected = new List<Player>();
            _auxListPlayerPrefabSelected.Clear();

            foreach (var player in _listPlayerPrefab)
            {
                if(_playerDatabase._selectedPlayers.Contains(player.GetComponent<Player>()._character))
                    _listPlayerPrefabSelected.Add(player);
            }

            SpawnPlayers();
            SpawnEnemies();
            SetCurrentGameState(_gsSelectPlayer);
            SetCurrentGuideMessage(DataGame.SELECT_PLAYER);
        }

        private void OnEnable()
        {
            OnSelectPlayer.AddListener(SelectPlayer);
            OnSelectEnemy.AddListener(SelectEnemy);
            OnOverPlayer.AddListener(OverPlayer);
        }

        private void OnDisable()
        {
            OnSelectPlayer.RemoveListener(SelectPlayer);
            OnSelectEnemy.RemoveListener(SelectEnemy);
            OnOverPlayer.RemoveListener(OverPlayer);
        }

        private void SpawnPlayers()
        {
            int i = 0;
            GameObject clone;
            foreach (var player in _listPlayerPrefabSelected)
            {
                clone = Instantiate(player.gameObject, _listPlayerSpawnPoints[i].transform);
                _auxListPlayerPrefabSelected.Add(clone.GetComponent<Player>());
                i++;
            }
        }

        private void SpawnEnemies()
        {
            Instantiate(_ListEnemyPrefab[Random.Range(0, _ListEnemyPrefab.Count)], _EnemySpawnPoints.transform);
        }

        private void SetCurrentGameState(GameState gs)
        {
            _CurrentGameState = gs;
            OnUpdateStatus.Invoke();
        }

        private void SetCurrentGuideMessage(string value)
        {
            DataGame.CURRENT_TEXT_STATE = value;
        }

        private void SelectPlayer(Player arg0)
        {
            currentPlayer = arg0;

            SetCurrentGameState(_gsSelectEnemy);
            SetCurrentGuideMessage(DataGame.SELECT_ENEMY);

            _clamp.SetActive(true);
            Vector3 vec3 = arg0.transform.parent.localPosition; vec3.y = vec3.y + 1;
            _clamp.transform.localPosition = vec3;
        }

        private void SelectEnemy(Zombie arg0)
        {
            currentEnemy = arg0;

            SetCurrentGameState(null);
            SetCurrentGuideMessage(DataGame.PLAYER_ATTACK);

            OnUpdateStatus.Invoke();

            _clamp.SetActive(false);
            //Delay Attack Enemy
            _coroutine = DelayAttackEnemy();
            StartCoroutine(_coroutine);
        }

        private void OverPlayer(Character _char, bool _enable)
        {
            _hudCanvasGamePlay.LoadDataPlate(_char, _enable);
        }

        //Battle control
        IEnumerator DelayAttackEnemy()
        {
            currentPlayer.AnimAttack();
            currentEnemy.Damage(currentPlayer._character.AttackPower);
            CheckHealthEnemy();
            yield return new WaitForSeconds(2f);
            SetCurrentGameState(_gsEnemyAttack);
            SetCurrentGuideMessage(DataGame.ENEMY_ATTACK);
            yield return new WaitForSeconds(1f);
            SelectRandomPlayer().Damage(currentEnemy._enemy.AttackPower);
            SetCurrentGameState(_gsSelectPlayer);
            SetCurrentGuideMessage(DataGame.SELECT_PLAYER);
            CheckHealthyPlayers();
        }

        //Sort Not Dead Players
        private Player SelectRandomPlayer()
        {
            //Shuffle List
            _auxListPlayerPrefabSelected.Sort((a, b) => 1 - 2 * Random.Range(0, 1));
            foreach (var player in _auxListPlayerPrefabSelected)
            {
                if (!player.IsDead)
                    return player;
            }
            return currentPlayer;
        }

        private void ClearFunctions()
        {
            StopCoroutine(_coroutine);
            SetCurrentGameState(null);
            SetCurrentGuideMessage("");
            _CurrentGameState = null;
        }

        private void CheckHealthEnemy()
        {
            if(currentEnemy.Healthy <= 0)
            {
                GameOver(true);
                return;
            } 
        }

        private void CheckHealthyPlayers()
        {
            bool lost = true;
            foreach (var player in _auxListPlayerPrefabSelected)
            {
                if (player.Healthy > 0)
                {
                    lost = false;
                    break;
                }
            }

            if (lost)
                GameOver(false);
        }

        /*
         * PopUp loaded of the Resources folder
         * 
         */
        public void GameOver(bool value)
        {
            ClearFunctions();
            StartCoroutine(DelayClose(value));
        }

        IEnumerator DelayClose(bool value)
        {
            yield return new WaitForSeconds(2f);
            DataGame.win = value;
            GameObject.Instantiate(Resources.Load(DataGame.PREFAB_ENDGAME) as GameObject);
            Time.timeScale = 0f;
        }

        private void OnApplicationQuit()
        {
            
        }
    }
}