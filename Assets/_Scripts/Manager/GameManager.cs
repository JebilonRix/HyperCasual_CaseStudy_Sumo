using System.Collections.Generic;
using UnityEngine;

namespace SumoDemo
{
    public class GameManager : MonoBehaviour
    {
        static GameManager _instance;

        [SerializeField] uint _remainPlayer = 1;
        [SerializeField] uint _remainEnemy = 7;

        private GameState _gameState;
        private bool _isGameOver;

        public static GameManager Instance { get => _instance; set => _instance = value; }
        public uint RemainEnemy { get => _remainEnemy; set => _remainEnemy = value; }
        public uint RemainPlayer { get => _remainPlayer; set => _remainPlayer = value; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (RemainPlayer <= 0 || RemainEnemy <= 0)
            {
                if (!_isGameOver)
                {
                    StateMachine(GameState.GameOver);
                }
            }
        }
        private void StateMachine(GameState gameState)
        {
            _gameState = gameState;

            switch (_gameState)
            {
                case GameState.GamePlay:
                    Time.timeScale = 1;
                    break;

                case GameState.GameOver:

                    ScoreBoard();
                    _isGameOver = true;
                    Time.timeScale = 0;
                    Debug.Log("Game over");
                    break;
            }
        }

        private static void ScoreBoard()
        {
            List<uint> scoreBoard = new List<uint>();

            CharacterCollect[] scores = FindObjectsOfType<CharacterCollect>();

            foreach (CharacterCollect item in scores)
            {
                scoreBoard.Add(item.Money);
            }

            scoreBoard.Sort();

            Debug.Log(scoreBoard[scores.Length - 1]);
        }
    }
    public enum GameState
    {
        GamePlay, GameOver
    }
}